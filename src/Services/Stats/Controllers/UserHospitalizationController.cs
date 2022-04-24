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
    public class UserHospitalizationController
        : AbstractController<UserHospitalization, UserHospitalizationDto>
    {
        public UserHospitalizationController(HealthPanelDbContext context) : base(context) { }

        // GET: api/UserHospitalization
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserHospitalizationDto>>> Get()
        {
            var entities = await _context.UserHospitalizations.ToListAsync();

            var dtos = entities
                .Select(async p =>
                    await _mapper.Map<UserHospitalization, UserHospitalizationDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/UserHospitalization/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<UserHospitalizationDto>> Get(int id)
        {
            var entity = await _context.UserHospitalizations.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<UserHospitalization, UserHospitalizationDto>(entity));
        }

        // PUT: api/UserHospitalization/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, UserHospitalizationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.UserHospitalizations.FindAsync(id);

            modified.HospitalizationId = dto.HospitalizationId;
            modified.StartDate = dto.StartDate;
            modified.EndDate = dto.EndDate;
            modified.DoctorId = dto.DoctorId;
            modified.DischargeSummary = dto.DischargeSummary;
            modified.Notes = dto.Notes;
            
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

        // POST: api/UserHospitalization
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<UserHospitalizationDto>> Post(UserHospitalizationDto dto)
        {
            var entity = this.ConvertToEntity(dto);

            _context.UserHospitalizations.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                 new { id = entity.Id },
                 await _mapper.Map<UserHospitalization, UserHospitalizationDto>(entity));
        }

        // DELETE: api/UserHospitalization/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.UserHospitalizations.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.UserHospitalizations.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.UserHospitalizations.Any(e => e.Id == id);

        private UserHospitalization ConvertToEntity(UserHospitalizationDto dto)
            => new()
            {
                // Id = dto.Id,
                HospitalizationId = dto.HospitalizationId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DoctorId = dto.DoctorId,
                DischargeSummary = dto.DischargeSummary,
                Notes = dto.Notes,
            };
    }
}
