using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthPanel.Core.Entities;
using HealthPanel.Core.Enums;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthFacilityBranchController : AbstractController<HealthFacilityBranchDto>
    {
        private readonly HealthPanelDbContext _context;

        public HealthFacilityBranchController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/HealthFacilityBranch
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<HealthFacilityBranchDto>>> Get()
        {
            var entities = await _context.HealthFacilityBranches.ToListAsync();

            return Ok(entities.Select(p => this.ConvertToDto(p)));
        }

        // GET: api/HealthFacilityBranch/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<HealthFacilityBranchDto>> Get(int id)
        {
            var branch = await _context.HealthFacilityBranches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return Ok(this.ConvertToDto(branch));
        }

        // PUT: api/HealthFacilityBranch/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, HealthFacilityBranchDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var branch = await _context.HealthFacilityBranches.FindAsync(id);
                       
            branch.Name = dto.Name;
            _context.Entry(branch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthFacilityBranchExists(id))
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

        // POST: api/HealthFacilityBranch
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<HealthFacilityBranchDto>> Post(HealthFacilityBranchDto dto)
        {
            var branch = ConvertToEntity(dto);
            _context.HealthFacilityBranches.Add(branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = branch.Id }, branch);
        }

        // DELETE: api/HealthFacilityBranch/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var branch = await _context.HealthFacilityBranches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _context.HealthFacilityBranches.Remove(branch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HealthFacilityBranchExists(int id)
        {
            return _context.HealthFacilityBranches.Any(e => e.Id == id);
        }

        private HealthFacilityBranchDto ConvertToDto(object raw) {
            var entity = raw as HealthFacilityBranch;
            
            return new HealthFacilityBranchDto
            {
                Id = entity.Id,
                HealthFacilityId = entity.HealthFacilityId,
                Name = entity.Name,
                Address = entity.Address,
                Type = entity.Type.ToString(),
            };
        }

        private HealthFacilityBranch ConvertToEntity(HealthFacilityBranchDto branch) {
            return new HealthFacilityBranch
            {
                // Id = branch.Id,
                HealthFacilityId = branch.HealthFacilityId,
                Name = branch.Name,
                Address = branch.Address,
                Type = (HealthFacilityBranchType)
                    Enum.Parse(typeof(HealthFacilityBranchType), branch.Type),
            };
        }
    }
}
