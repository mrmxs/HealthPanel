using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Core.Enums;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthFacilityBranchController
        : AbstractController<HealthFacilityBranch, HealthFacilityBranchDto>
    {
        public HealthFacilityBranchController(HealthPanelDbContext context) : base(context) { }

        // GET: api/HealthFacilityBranch
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<HealthFacilityBranchDto>>> Get()
        {
            var entities = await _context.HealthFacilityBranches.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<HealthFacilityBranch, HealthFacilityBranchDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/HealthFacilityBranch/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<HealthFacilityBranchDto>> Get(int id)
        {
            var entity = await _context.HealthFacilityBranches.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<HealthFacilityBranch, HealthFacilityBranchDto>(entity));
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

            var modified = await _context.HealthFacilityBranches.FindAsync(id);

            modified.HealthFacilityId = dto.HealthFacilityId;
            modified.Name = dto.Name;
            modified.Address = dto.Address;
            modified.Type = (HealthFacilityBranchType)
                Enum.Parse(typeof(HealthFacilityBranchType), dto.Type);
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

        // POST: api/HealthFacilityBranch
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<HealthFacilityBranchDto>> Post(
            HealthFacilityBranchDto dto)
        {
            var entity = this.ConvertToEntity(dto);
            
            _context.HealthFacilityBranches.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = entity.Id },
                await _mapper.Map<HealthFacilityBranch, HealthFacilityBranchDto>(entity));
        }

        // DELETE: api/HealthFacilityBranch/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.HealthFacilityBranches.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.HealthFacilityBranches.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.HealthFacilityBranches.Any(e => e.Id == id);

        private HealthFacilityBranch ConvertToEntity(HealthFacilityBranchDto branch)
            => new()
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
