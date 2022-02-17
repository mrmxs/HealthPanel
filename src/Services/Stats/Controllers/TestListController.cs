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
    public class TestListController
        : AbstractController<TestList, TestListDto>
    {
        private readonly HealthPanelDbContext _context;

        public TestListController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/TestList
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<TestListDto>>> Get()
        {
            var entities = await _context.TestLists.ToListAsync();

            var dtos = entities
                .Select(async p => await this.EntityToDtoAsync(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/TestList/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<TestListDto>> Get(int id)
        {
            var entity = await _context.TestLists.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await this.EntityToDtoAsync(entity));
        }

        // PUT: api/TestList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, TestListDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.TestLists.FindAsync(id);

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

        // POST: api/TestList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<TestListDto>> Post(TestListDto dto)
        {
            var newEntity = ConvertToEntity(dto);

            _context.TestLists.Add(newEntity);
            // on SaveChangesAsync() after calling DetectChanges()
            // newEntity is rewritted and got actual Id,
            // which can be used later
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = newEntity.Id }, await this.EntityToDtoAsync(newEntity));
        }

        // DELETE: api/TestList/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.TestLists.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TestLists.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.TestLists.Any(e => e.Id == id);

        protected override async Task<TestListDto> EntityToDtoAsync(TestList entity)
            => new TestListDto(entity);

        private TestList ConvertToEntity(TestListDto dto)
            => new()
            {
                // Id = lab.Id,
                Name = dto.Name,
            };

    }
}
