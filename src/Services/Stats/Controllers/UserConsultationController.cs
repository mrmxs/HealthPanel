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
    public class UserConsultationController
        : AbstractController<UserConsultation, UserConsultationDto>
    {
        public UserConsultationController(HealthPanelDbContext context) : base(context) { }

        // GET: api/UserConsultation
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserConsultationDto>>> Get()
        {
            var entities = await _context.UserConsultations.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<UserConsultation, UserConsultationDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/UserConsultation/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<UserConsultationDto>> Get(int id)
        {
            var entity = await _context.UserConsultations.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<UserConsultation, UserConsultationDto>(entity));
        }

        // PUT: api/UserConsultation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, UserConsultationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.UserConsultations.FindAsync(id);

            modified.ConsultationId = dto.ConsultationId;
            modified.DoctorId = dto.DoctorId;
            modified.Date = dto.Date;
            modified.UserId = dto.UserId;
            modified.Value = dto.Value;
            modified.Status = dto.Status;
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

        // POST: api/UserConsultation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<UserConsultationDto>> Post(UserConsultationDto dto)
        {
            var entity = this.ConvertToEntity(dto);

            _context.UserConsultations.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                 new { id = entity.Id },
                 await _mapper.Map<UserConsultation, UserConsultationDto>(entity));
        }

        // DELETE: api/UserConsultation/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.UserConsultations.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.UserConsultations.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.UserConsultations.Any(e => e.Id == id);

        private UserConsultation ConvertToEntity(UserConsultationDto dto)
           => new()
           {
               // Id = dto.Id,
               ConsultationId = dto.ConsultationId,
               DoctorId = dto.DoctorId,
               Date = dto.Date,
               UserId = dto.UserId,
               Value = dto.Value,
               Status = dto.Status,
           };
    }
}
