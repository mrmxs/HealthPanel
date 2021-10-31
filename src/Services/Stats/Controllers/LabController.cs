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
    public class LabController : AbstractController<LabDto>
    {
        private readonly HealthPanelDbContext _context;

        public LabController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/Lab
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<LabDto>>> Get()
        {
            var entities = await _context.Labs.ToListAsync();

            return Ok(entities.Select(p => this.ConvertToDto(p)));
        }

        // GET: api/Lab/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<LabDto>> Get(int id)
        {
            var lab = await _context.Labs.FindAsync(id);

            if (lab == null)
            {
                return NotFound();
            }

            return Ok(this.ConvertToDto(lab));
        }

        // PUT: api/Lab/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, LabDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var lab = await _context.Labs.FindAsync(id);
                       
            lab.Name = dto.Name;
            _context.Entry(lab).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabExists(id))
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

        // POST: api/Lab
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<LabDto>> Post(LabDto dto)
        {
            var lab = ConvertToEntity(dto);
            _context.Labs.Add(lab);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = lab.Id }, lab);
        }

        // DELETE: api/Lab/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var lab = await _context.Labs.FindAsync(id);
            if (lab == null)
            {
                return NotFound();
            }

            _context.Labs.Remove(lab);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LabExists(int id)
        {
            return _context.Labs.Any(e => e.Id == id);
        }

        private LabDto ConvertToDto(object raw) {
            var entity = raw as Lab;
            
            return new LabDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        private Lab ConvertToEntity(LabDto lab) {
            return new Lab
            {
                // Id = lab.Id,
                Name = lab.Name,
            };
        }
    }
}
