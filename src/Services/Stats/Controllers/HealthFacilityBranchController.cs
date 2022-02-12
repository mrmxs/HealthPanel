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
    public class HealthFacilityBranchController 
        : AbstractController<HealthFacilityBranch, HealthFacilityBranchDto>
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

            var dtos = entities
                .Select(async p => await this.EntityToDtoAsync(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
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

            return Ok(await this.EntityToDtoAsync(branch));
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
                       
            branch.HealthFacilityId = dto.HealthFacilityId;
            branch.Name = dto.Name;
            branch.Address = dto.Address;
            branch.Type = (HealthFacilityBranchType)
                Enum.Parse(typeof(HealthFacilityBranchType), dto.Type);
            _context.Entry(branch).State = EntityState.Modified;

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

        protected override bool Exists(int id)
        {
            return _context.HealthFacilityBranches.Any(e => e.Id == id);
        }

        protected override async Task<HealthFacilityBranchDto>
            EntityToDtoAsync(HealthFacilityBranch entity)
                => new HealthFacilityBranchDto(entity);

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
