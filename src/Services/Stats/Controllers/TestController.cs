using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController
        : AbstractController<MedTest, MedTestDto>
    {
        private readonly HealthPanelDbContext _context;

        public TestController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/Test
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MedTestDto>>> Get()
        {
            var entities = await _context.Tests.ToListAsync();
            
            var dtos = entities
                .Select(async p => await this.EntityToDtoAsync(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<MedTestDto>> Get(int id)
        {
            var entity = await _context.Tests.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await this.EntityToDtoAsync(entity));
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, MedTestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.Tests.FindAsync(id);
                       
            modified.Name = dto.Name; //todo bad practice
            modified.Units = dto.Units;
            _context.Entry(modified).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<MedTestDto>> Post(MedTestDto dto)
        {
            _context.Tests.Add(ConvertToEntity(dto));

            var newEntityId = await _context.SaveChangesAsync();
            var newEntity = await _context.Tests.FindAsync(newEntityId);

            return CreatedAtAction(nameof(Post),
                new { id = newEntity.Id },
                await this.EntityToDtoAsync(newEntity));
            // The C# nameof keyword is used to avoid hard-coding 
            // the action name in the CreatedAtAction call.
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Tests.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.Tests.Any(e => e.Id == id);

        protected override async Task<MedTestDto> EntityToDtoAsync(MedTest entity)
            => new MedTestDto(entity);

        private MedTest ConvertToEntity(MedTestDto dto)
            => new()
            {
                // Id = lab.Id,
                Name = dto.Name,
                Units = dto.Units,
            };
    }
}
