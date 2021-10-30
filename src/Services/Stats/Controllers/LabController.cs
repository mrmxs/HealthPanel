using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private readonly HealthPanelDbContext _context;

        public LabController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/Lab
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lab>>> GetLabs()
        {
            return await _context.Labs.ToListAsync();
        }

        // GET: api/Lab/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lab>> GetLab(int id)
        {
            var lab = await _context.Labs.FindAsync(id);

            if (lab == null)
            {
                return NotFound();
            }

            return lab;
        }

        // PUT: api/Lab/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLab(int id, Lab lab)
        {
            if (id != lab.Id)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<Lab>> PostLab(Lab lab)
        {
            _context.Labs.Add(lab);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLab), new { id = lab.Id }, lab);
        }

        // DELETE: api/Lab/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLab(int id)
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
    }
}
