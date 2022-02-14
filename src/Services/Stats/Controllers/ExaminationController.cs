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
    public class ExaminationController
        : AbstractController<Examination, ExaminationDto>
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
            
            var dtos = entities
                .Select(async p => await this.EntityToDtoAsync(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();
            
            return Ok(dtos);
        }

        // GET: api/Examination/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<ExaminationDto>> Get(int id)
        {
            var entity = await _context.Examinations.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await this.EntityToDtoAsync(entity));
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
            
            var modified = await _context.Examinations.FindAsync(id);

            modified.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            modified.TestId = dto.TestId; //todo bad practice
            modified.CustomName = dto.CustomTestName;
            
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

        // POST: api/Examination
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<ExaminationDto>> Post(
            ExaminationDto dto)
        {
            //todo: equal LabTestController: healthFacilityBranchId & testId validation -> repos

            _context.Examinations.Add(this.ConvertToEntity(dto));

            var newEntityId = await _context.SaveChangesAsync();
            var newEntity = await _context.Examinations.FindAsync(newEntityId);

            return CreatedAtAction(nameof(Post),
                 new { id = newEntity.Id },
                 await this.EntityToDtoAsync(newEntity));
        }

        // DELETE: api/Examination/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Examinations.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Examinations.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.Examinations.Any(e => e.Id == id);

        protected override async Task<ExaminationDto> EntityToDtoAsync(
            Examination entity)
            => new ExaminationDto( //todo move to repository
                examinationEntity:  entity,
                branchEntity:   await _context.HealthFacilityBranches
                    .FindAsync(entity.HealthFacilityBranchId),
                medTestEntity:  await _context.Tests.FindAsync(entity.TestId)
            );

        private Examination ConvertToEntity(ExaminationDto dto)
            => new()
            {
                // Id = dto.Id,
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                TestId = dto.TestId,
                CustomName = dto.CustomTestName,
            };
    }
}
