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
    public class LabTestController : AbstractController<LabMedicalTestDto>
    {
        private readonly HealthPanelDbContext _context;

        public LabTestController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/LabTest
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<LabMedicalTestDto>>> Get()
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
        public override async Task<ActionResult<LabMedicalTestDto>> Get(int id)
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
        public override async Task<IActionResult> Put(int id, LabMedicalTestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var labTest = await _context.LabTests.FindAsync(id);
                       
            labTest.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            labTest.TestId = dto.TestId;
            labTest.Min = dto.Min;
            labTest.Max = dto.Max;

            _context.Entry(labTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabMedicalTestExists(id))
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
        public override async Task<ActionResult<LabMedicalTestDto>> Post(LabMedicalTestDto dto)
        {
            //todo: healthFacilityBranchId & testId validation -> repos

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

        private bool LabMedicalTestExists(int id)
        {
            return _context.LabTests.Any(e => e.Id == id);
        }

        //todo move to repository
        private async Task<IEnumerable<object>> GetEntities(LabMedTest entity) 
        {
            var lab = await _context.HealthFacilityBranches.FindAsync(entity.HealthFacilityBranchId);
            var test = await _context.Tests.FindAsync(entity.TestId);

            var entities = new List<object>{ entity, lab, test };
            
            return entities;
        }

        //todo place your refactor here
        private LabMedicalTestDto ConvertToDto(IEnumerable<object> entities)
        {
            var labTest = entities.ToArray()[0] as LabMedTest;
            var lab = entities.ToArray()[1] as HealthFacilityBranch;
            var test = entities.ToArray()[2] as MedTest;

            return new LabMedicalTestDto
            {
                Id = labTest.Id,
                HealthFacilityBranchId = labTest.HealthFacilityBranchId,
                LabName = lab.Name,
                TestId = labTest.TestId,
                TestTitle = test.Name,
                Min = labTest.Min,
                Max = labTest.Max,
            };
        }

        private LabMedTest ConvertToEntity(LabMedicalTestDto dto)
        {
            return new LabMedTest
            {
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                TestId = dto.TestId,
                Min = dto.Min,
                Max = dto.Max,
            };
        }
    }
}
