using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : AbstractController<DoctorDto>
    {
        private readonly HealthPanelDbContext _context;

        public DoctorController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/Doctor
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<DoctorDto>>> Get()
        {
            var entities = await _context.Doctors.ToListAsync();

            return Ok(entities.Select(t => this.ConvertToDto(t)));
        }

        // GET: api/Doctor/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<DoctorDto>> Get(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(this.ConvertToDto(doctor));
        }

        // PUT: api/Doctor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, DoctorDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }


            var doctor = await _context.Doctors.FindAsync(id);

            doctor.Name = dto.Name; //todo bad practice
            doctor.HealthFacilityBranchId = dto.HealthFacilityBranchId; //todo bad practice
            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/Doctor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<DoctorDto>> Post(DoctorDto dto)
        {
            var doctor = ConvertToEntity(dto);
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = doctor.Id }, doctor);
        }

        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }

        private object ConvertToDto(object raw)
        {
            var entity = raw as Doctor;

            return new DoctorDto
            {
                Id = entity.Id,
                Name = entity.Name,
                HealthFacilityBranchId = entity.HealthFacilityBranchId,
            };
        }

        private Doctor ConvertToEntity(DoctorDto dto)
        {
            return new Doctor
            {
                // Id = dto.Id,
                Name = dto.Name,
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
            };
        }
    }
}
