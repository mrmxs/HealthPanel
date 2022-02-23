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
    public class TestToTestListController
        : AbstractController<TestToTestList, TestToTestListDto>
    {
        public TestToTestListController(HealthPanelDbContext context) : base(context) { }

        // GET: api/TestToTestList
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<TestToTestListDto>>> Get()
        {
            var entities = await _context.TestsToTestList.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<TestToTestList, TestToTestListDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/TestToTestList/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<TestToTestListDto>> Get(int id)
        {
            var entity = await _context.TestsToTestList.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<TestToTestList, TestToTestListDto>(entity));
        }

        // PUT: api/TestToTestList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(
            int id, TestToTestListDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.TestsToTestList.FindAsync(id);

            modified.TestListId = dto.TestListId;
            modified.MedTestId = dto.MedTestId;
            modified.LabTestId = dto.LabTestId;
            modified.ExaminationId = dto.ExaminationId;
            modified.TestPanelId = dto.TestPanelId;
            modified.LabTestPanelId = dto.LabTestPanelId;
            modified.Index = dto.Index;

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

        // POST: api/TestToTestList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<TestToTestListDto>> Post(
            TestToTestListDto dto)
        {
            var newEntity = ConvertToEntity(dto);

            _context.TestsToTestList.Add(newEntity);
            // on SaveChangesAsync() after calling DetectChanges()
            // newEntity is rewritted and got actual Id,
            // which can be used later
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = newEntity.Id },
                await _mapper.Map<TestToTestList, TestToTestListDto>(newEntity));
        }

        // DELETE: api/TestToTestList/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.TestsToTestList.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TestsToTestList.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.TestsToTestList.Any(e => e.Id == id);

        private TestToTestList ConvertToEntity(TestToTestListDto dto)
            => new()
            {
                // Id = lab.Id,
                TestListId = dto.TestListId,
                MedTestId = dto.MedTestId,
                LabTestId = dto.LabTestId,
                ExaminationId = dto.ExaminationId,
                TestPanelId = dto.TestPanelId,
                LabTestPanelId = dto.LabTestPanelId,
                Index = dto.Index,
            };
    }
}
