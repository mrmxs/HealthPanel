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
    public class ConsultationController
        : AbstractController<Consultation, ConsultationDto>
    {
        public ConsultationController(HealthPanelDbContext context) : base(context) { }

        // GET: api/Consultation
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ConsultationDto>>> Get()
        {
            var entities = await _context.Consultations.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<Consultation, ConsultationDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/Consultation/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<ConsultationDto>> Get(int id)
        {
            var entity = await _context.Consultations.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<Consultation, ConsultationDto>(entity));
        }

        // PUT: api/Consultation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, ConsultationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.Consultations.FindAsync(id);

            modified.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            modified.TestId = dto.TestId;
            modified.CustomName = dto.CustomName;
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

        // POST: api/Consultation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<ConsultationDto>> Post(ConsultationDto dto)
        {
            var entity = this.ConvertToEntity(dto);

            _context.Consultations.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                 new { id = entity.Id },
                 await _mapper.Map<Consultation, ConsultationDto>(entity));
        }

        // DELETE: api/Consultation/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Consultations.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Consultations.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.Consultations.Any(e => e.Id == id);

        private Consultation ConvertToEntity(ConsultationDto dto)
            => new()
            {
                // Id = dto.Id,
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                TestId = dto.TestId,
                CustomName = dto.CustomName,
            };
    }
}
