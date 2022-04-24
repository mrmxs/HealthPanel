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
    public class DepartmentController
        : AbstractController<Department, DepartmentDto>
    {
        public DepartmentController(HealthPanelDbContext context) : base(context) { }

        // GET: api/Department
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<DepartmentDto>>> Get()
        {
            var entities = await _context.Departments.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<Department, DepartmentDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<DepartmentDto>> Get(int id)
        {
            var entity = await _context.Departments.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<Department, DepartmentDto>(entity));
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, DepartmentDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.Departments.FindAsync(id);

            modified.Name = dto.Name; //todo bad practice
            modified.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            modified.HeadId = dto.HeadId;
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

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<DepartmentDto>> Post(DepartmentDto dto)
        {
            var entity = this.ConvertToEntity(dto);

            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = entity.Id },
                _mapper.Map<Department, DepartmentDto>(entity));
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.Departments.Any(e => e.Id == id);

        private Department ConvertToEntity(DepartmentDto dto)
            => new()
            {
                // Id = dto.Id,
                Name = dto.Name,
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                HeadId = dto.HeadId,
            };
    }
}
