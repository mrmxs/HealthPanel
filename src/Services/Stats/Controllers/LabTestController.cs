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
    public class LabTestController
        : AbstractController<LabMedTest, LabMedTestDto>
    {
        private readonly HealthPanelDbContext _context;

        public LabTestController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/LabTest
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<LabMedTestDto>>> Get()
        {            
            var entities = await _context.LabTests.ToListAsync();
            
            var dtos = entities
                .Select(async p => await this.EntityToDto(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();
            
            return Ok(dtos);
        }

        // GET: api/LabTest/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<LabMedTestDto>> Get(int id)
        {
            var labMedicalTest = await _context.LabTests.FindAsync(id);

            if (labMedicalTest == null)
            {
                return NotFound();
            }

            return Ok(await this.EntityToDto(labMedicalTest));
        }

        // PUT: api/LabTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, LabMedTestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var labTest = await _context.LabTests.FindAsync(id);
                       
            labTest.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            labTest.TestId = dto.TestId;
            labTest.CustomName = dto.CustomTestName;
            labTest.Min = dto.Min;
            labTest.Max = dto.Max;

            _context.Entry(labTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabMedTestExists(id))
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
        public override async Task<ActionResult<LabMedTestDto>> Post(LabMedTestDto dto)
        {
            //todo: equal ExaminationController: healthFacilityBranchId & testId validation -> repos

            _context.LabTests.Add(this.ConvertToEntity(dto));
            var resultId = await _context.SaveChangesAsync();

            var labMedicalTest = await _context.LabTests.FindAsync(resultId);
            var result = await this.EntityToDto(labMedicalTest);

            return CreatedAtAction(nameof(Post), new { id = resultId }, result);
        }

        // DELETE: api/LabTest/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var labMedicalTest = await _context.LabTests.FindAsync(id);
            if (labMedicalTest == null)
            {
                return NotFound();
            }

            _context.LabTests.Remove(labMedicalTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //todo place your refactor here
        private bool LabMedTestExists(int id)
        {
            return _context.LabTests.Any(e => e.Id == id);
        }

        protected override async Task<LabMedTestDto> EntityToDto(LabMedTest entity)
        {
            //todo move to repository

            return new LabMedTestDto(
                labTestEntity:  entity,
                branchEntity:   await _context.HealthFacilityBranches
                    .FindAsync(entity.HealthFacilityBranchId),
                medTestEntity:  await _context.Tests.FindAsync(entity.TestId)
            );
        }

        private LabMedTest ConvertToEntity(LabMedTestDto dto)
        {
            return new LabMedTest
            {
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                TestId = dto.TestId,
                CustomName = dto.CustomTestName,
                Min = dto.Min,
                Max = dto.Max,
            };
        }
    }
}
