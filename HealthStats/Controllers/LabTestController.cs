using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthStats;
using HealthStats.Models;

namespace HealthStats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        private readonly HealthPanelDbContext _context;

        public LabTestController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/LabTest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabMedicalTest>>> GetLabTests()
        {
            return await _context.LabTests.ToListAsync();
        }

        // GET: api/LabTest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LabMedicalTest>> GetLabMedicalTest(int id)
        {
            var labMedicalTest = await _context.LabTests.FindAsync(id);

            if (labMedicalTest == null)
            {
                return NotFound();
            }

            return labMedicalTest;
        }

        // PUT: api/LabTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLabMedicalTest(int id, LabMedicalTest labMedicalTest)
        {
            if (id != labMedicalTest.Id)
            {
                return BadRequest();
            }

            _context.Entry(labMedicalTest).State = EntityState.Modified;

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
        public async Task<ActionResult<LabMedicalTest>> PostLabMedicalTest(LabMedicalTest labMedicalTest)
        {
            _context.LabTests.Add(labMedicalTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLabMedicalTest), new { id = labMedicalTest.Id }, labMedicalTest);
        }

        // DELETE: api/LabTest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabMedicalTest(int id)
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
    }
}
