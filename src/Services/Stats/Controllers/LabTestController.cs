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
    public class LabTestController : AbstractController<LabMedTestDto>
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
                .Select(async p => this.ConvertToDto(await this.GetEntities(p)))
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

            return Ok(this.ConvertToDto(await this.GetEntities(labMedicalTest)));
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
            var result = this.ConvertToDto(await this.GetEntities(labMedicalTest));

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

        private bool LabMedTestExists(int id)
        {
            return _context.LabTests.Any(e => e.Id == id);
        }

        //todo move to repository
        private async Task<IEnumerable<object>> GetEntities(LabMedTest entity) 
        {
            var branch = await _context.HealthFacilityBranches.FindAsync(entity.HealthFacilityBranchId);
            var test = await _context.Tests.FindAsync(entity.TestId);

            var entities = new List<object>{ entity, branch, test };
            
            return entities;
        }

        //todo place your refactor here
        private LabMedTestDto ConvertToDto(IEnumerable<object> entities)
        {
            var entity = entities.ToArray()[0] as LabMedTest;
            var branch = entities.ToArray()[1] as HealthFacilityBranch;
            var test = entities.ToArray()[2] as MedTest;

            return new LabMedTestDto
            {
                Id = entity.Id,
                HealthFacilityBranchId = entity.HealthFacilityBranchId,
                HealthFacilityBranchName = branch.Name,
                TestId = entity.TestId,
                TestName = test.Name,
                CustomTestName = entity.CustomName,
                Units = test.Units,
                Min = entity.Min,
                Max = entity.Max,
            };
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
