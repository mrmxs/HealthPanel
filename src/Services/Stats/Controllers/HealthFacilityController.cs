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
    public class HealthFacilityController
        : AbstractController<HealthFacility, HealthFacilityDto>
    {
        public HealthFacilityController(HealthPanelDbContext context) : base(context) { }

        // GET: api/HealthFacility
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<HealthFacilityDto>>> Get()
        {
            var entities = await _context.HealthFacilities.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<HealthFacility, HealthFacilityDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/HealthFacility/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<HealthFacilityDto>> Get(int id)
        {
            var entity = await _context.HealthFacilities.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<HealthFacility, HealthFacilityDto>(entity));
        }

        // PUT: api/HealthFacility/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, HealthFacilityDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.HealthFacilities.FindAsync(id);

            modified.Name = dto.Name; //todo bad practice
            modified.Address = dto.Address;

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

        // POST: api/HealthFacility
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<HealthFacilityDto>> Post(
            HealthFacilityDto dto)
        {
            var entity = this.ConvertToEntity(dto);

            _context.HealthFacilities.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = entity.Id },
                await _mapper.Map<HealthFacility, HealthFacilityDto>(entity));
        }

        // DELETE: api/HealthFacility/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.HealthFacilities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.HealthFacilities.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.HealthFacilities.Any(e => e.Id == id);

        private HealthFacility ConvertToEntity(HealthFacilityDto dto)
            => new()
            {
                // Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
            };
    }
}
