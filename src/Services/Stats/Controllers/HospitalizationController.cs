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
    public class HospitalizationController
        : AbstractController<Hospitalization, HospitalizationDto>
    {
        public HospitalizationController(HealthPanelDbContext context) : base(context) { }

        // GET: api/Hospitalization
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<HospitalizationDto>>> Get()
        {
            var entities = await _context.Hospitalization.ToListAsync();

            var dtos = entities
                .Select(async p =>
                    await _mapper.Map<Hospitalization, HospitalizationDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/Hospitalization/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<HospitalizationDto>> Get(int id)
        {
            var entity = await _context.Hospitalization.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<Hospitalization, HospitalizationDto>(entity));
        }

        // PUT: api/Hospitalization/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, HospitalizationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.Hospitalization.FindAsync(id);

            modified.Name = dto.Name; //todo bad practice
            modified.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            modified.Department = dto.Department;
            modified.PreHplzTListId = dto.PreHplzTListId;
            modified.DefaultDoctorId = dto.DefaultDoctorId;
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

        // POST: api/Hospitalization
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<HospitalizationDto>> Post(
            HospitalizationDto dto)
        {
            var entity = this.ConvertToEntity(dto);

            _context.Hospitalization.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                 new { id = entity.Id },
                 await _mapper.Map<Hospitalization, HospitalizationDto>(entity));
        }

        // DELETE: api/Hospitalization/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Hospitalization.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Hospitalization.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.Hospitalization.Any(e => e.Id == id);

        private Hospitalization ConvertToEntity(HospitalizationDto dto)
            => new()
            {
                // Id = dto.Id,
                Name = dto.Name,
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                Department = dto.Department,
                PreHplzTListId = dto.PreHplzTListId,
                DefaultDoctorId = dto.DefaultDoctorId,
            };
    }
}
