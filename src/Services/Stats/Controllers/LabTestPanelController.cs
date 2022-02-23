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
    public class LabTestPanelController
        : AbstractController<LabTestPanel, LabTestPanelDto>
    {
        public LabTestPanelController(HealthPanelDbContext context) : base(context) { }

        // GET: api/LabTestPanel
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<LabTestPanelDto>>> Get()
        {
            var entities = await _context.LabTestPanels.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<LabTestPanel, LabTestPanelDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/LabTestPanel/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<LabTestPanelDto>> Get(int id)
        {
            var entity = await _context.LabTestPanels.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<LabTestPanel, LabTestPanelDto>(entity));
        }

        // PUT: api/LabTestPanel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, LabTestPanelDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            foreach (var testId in dto.LabTestIds)
            {
                // todo refactoring to repos
                if (!_context.LabTests.Any(e => e.Id == testId))
                {
                    return BadRequest();
                }
            }

            var modified = await _context.LabTestPanels.FindAsync(id);
            // todo nullable data in dto
            modified.HealthFacilityBranchId = dto.HealthFacilityBranchId;
            modified.TestPanelId = dto.TestPanelId;
            modified.CustomName = dto.CustomTestPanelName;
            modified.LabTestIds = dto.LabTestIds;
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

        // POST: api/LabTestPanel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<LabTestPanelDto>> Post(LabTestPanelDto dto)
        {
            foreach (var testId in dto.LabTestIds)
            {
                if (!_context.LabTests.Any(e => e.Id == testId))
                {
                    return BadRequest();
                }
            }

            var entity = ConvertToEntity(dto);

            _context.LabTestPanels.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = entity.Id },
                await _mapper.Map<LabTestPanel, LabTestPanelDto>(entity));
        }

        // DELETE: api/LabTestPanel/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.LabTestPanels.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.LabTestPanels.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.LabTestPanels.Any(e => e.Id == id);

        private LabTestPanel ConvertToEntity(LabTestPanelDto dto)
            => new()
            {
                // Id = lab.Id,
                HealthFacilityBranchId = dto.HealthFacilityBranchId,
                TestPanelId = dto.TestPanelId,
                CustomName = dto.CustomTestPanelName,
                LabTestIds = dto.LabTestIds,
            };

    }
}
