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
    public class ExaminationController : AbstractController<ExaminationDto>
    {
        private readonly HealthPanelDbContext _context;

        public ExaminationController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/Examination
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<ExaminationDto>>> Get()
        {
            var entities = await _context.Examinations.ToListAsync();
            
            return Ok(entities.Select(t => this.ConvertToDto(t)));
        }

        // GET: api/Examination/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<ExaminationDto>> Get(int id)
        {
            var examination = await _context.Examinations.FindAsync(id);

            if (examination == null)
            {
                return NotFound();
            }

            return Ok(this.ConvertToDto(examination));;
        }

        // PUT: api/Examination/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, ExaminationDto dto )
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            
            var examination = await _context.Examinations.FindAsync(id);

            examination.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            examination.TestId = dto.TestId; //todo bad practice
            _context.Entry(examination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExaminationExists(id))
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

        // POST: api/Examination
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<ExaminationDto>> Post(ExaminationDto dto)
        {
            var examination = ConvertToEntity(dto);
            _context.Examinations.Add(examination);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = examination.Id }, examination);
        }

        // DELETE: api/Examination/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var examination = await _context.Examinations.FindAsync(id);
            if (examination == null)
            {
                return NotFound();
            }

            _context.Examinations.Remove(examination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExaminationExists(int id)
        {
            return _context.Examinations.Any(e => e.Id == id);
        }

        private object ConvertToDto(object raw)
        {
            var entity = raw as Examination;

            return new ExaminationDto
            {
                Id = entity.Id,
                HealthFacilityBranchId = entity.HealthFacilityBranchId,
                TestId = entity.TestId,
            };
        }

        private Examination ConvertToEntity(ExaminationDto dto)
        {
            return new Examination
            {
                // Id = dto.Id,
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                TestId = dto.TestId,
            };
        }
    }
}
