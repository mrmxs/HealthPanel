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
    public class LabTestController
        : AbstractController<LabTest, LabTestDto>
    {
        public LabTestController(HealthPanelDbContext context) : base(context) { }

        // GET: api/LabTest
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<LabTestDto>>> Get()
        {
            var entities = await _context.LabTests.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<LabTest, LabTestDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/LabTest/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<LabTestDto>> Get(int id)
        {
            var entity = await _context.LabTests.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<LabTest, LabTestDto>(entity));
        }

        // PUT: api/LabTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, LabTestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.LabTests.FindAsync(id);

            modified.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            modified.TestId = dto.TestId;
            modified.CustomName = dto.CustomTestName;
            modified.Min = dto.Min;
            modified.Max = dto.Max;

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

        // POST: api/LabTest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<LabTestDto>> Post(LabTestDto dto)
        {
            //todo: equal ExaminationController: healthFacilityBranchId & testId validation -> repos

            _context.LabTests.Add(this.ConvertToEntity(dto));

            var newEntityId = await _context.SaveChangesAsync();
            var newEntity = await _context.LabTests.FindAsync(newEntityId);

            return CreatedAtAction(nameof(Post),
               new { id = newEntity.Id },
               await _mapper.Map<LabTest, LabTestDto>(newEntity));
        }

        // DELETE: api/LabTest/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.LabTests.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.LabTests.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.LabTests.Any(e => e.Id == id);

        private LabTest ConvertToEntity(LabTestDto dto)
            => new()
            {
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                TestId = dto.TestId,
                CustomName = dto.CustomTestName,
                Min = dto.Min,
                Max = dto.Max,
            };
    }
}
