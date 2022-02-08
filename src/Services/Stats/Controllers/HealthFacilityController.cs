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
    public class HealthFacilityController : AbstractController<HealthFacilityDto>
    {
        private readonly HealthPanelDbContext _context;

        public HealthFacilityController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/HealthFacility
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<HealthFacilityDto>>> Get()
        {
            var entities = await _context.HealthFacilities.ToListAsync();

            return Ok(entities.Select(t => this.ConvertToDto(t)));
        }

        // GET: api/HealthFacility/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<HealthFacilityDto>> Get(int id)
        {
            var healthFacility = await _context.HealthFacilities.FindAsync(id);

            if (healthFacility == null)
            {
                return NotFound();
            }

            return Ok(this.ConvertToDto(healthFacility));
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

            var healthFacility = await _context.HealthFacilities.FindAsync(id);

            healthFacility.Name = dto.Name; //todo bad practice
            healthFacility.Address = dto.Address; 

            _context.Entry(healthFacility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthFacilityExists(id))
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
        public override async Task<ActionResult<HealthFacilityDto>> Post(HealthFacilityDto dto)
        {
            var healthFacility = ConvertToEntity(dto);
            _context.HealthFacilities.Add(healthFacility);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = healthFacility.Id }, healthFacility);
        }

        // DELETE: api/HealthFacility/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var healthFacility = await _context.HealthFacilities.FindAsync(id);
            if (healthFacility == null)
            {
                return NotFound();
            }

            _context.HealthFacilities.Remove(healthFacility);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HealthFacilityExists(int id)
        {
            return _context.HealthFacilities.Any(e => e.Id == id);
        }

        private object ConvertToDto(object raw)
        {
            var entity = raw as HealthFacility;

            return new HealthFacilityDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,

            };
        }

        private HealthFacility ConvertToEntity(HealthFacilityDto dto)
        {
            return new HealthFacility
            {
                // Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
            };
        }
    }
}
