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
    public class TestPanelController
        : AbstractController<TestPanel, TestPanelDto>
    {
        public TestPanelController(HealthPanelDbContext context) : base(context) { }

        // GET: api/TestPanel
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<TestPanelDto>>> Get()
        {
            var entities = await _context.TestPanels.ToListAsync();

            var dtos = entities
                .Select(async p => await this.EntityToDtoAsync(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/TestPanel/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<TestPanelDto>> Get(int id)
        {
            var entity = await _context.TestPanels.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await this.EntityToDtoAsync(entity));
        }

        // PUT: api/TestPanel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, TestPanelDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.TestPanels.FindAsync(id);

            modified.Name = dto.Name;
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

        // POST: api/TestPanel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<TestPanelDto>> Post(TestPanelDto dto)
        {
            var newEntity = ConvertToEntity(dto);

            _context.TestPanels.Add(newEntity);
            // on SaveChangesAsync() after calling DetectChanges()
            // newEntity is rewritted and got actual Id,
            // which can be used later
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = newEntity.Id }, await this.EntityToDtoAsync(newEntity));
        }

        // DELETE: api/TestPanel/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.TestPanels.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TestPanels.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.TestPanels.Any(e => e.Id == id);

        protected override async Task<TestPanelDto> EntityToDtoAsync(TestPanel entity)
            => new TestPanelDto(entity);

        private TestPanel ConvertToEntity(TestPanelDto dto)
            => new()
            {
                // Id = lab.Id,
                Name = dto.Name,
            };
    }
}
