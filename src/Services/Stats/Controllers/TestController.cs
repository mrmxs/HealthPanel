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
    public class TestController : ControllerBase
    {
        private readonly HealthPanelDbContext _context;

        public TestController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalTest>>> GetTests()
        {
            return await _context.Tests.ToListAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalTest>> GetMedicalTest(int id)
        {
            var medicalTest = await _context.Tests.FindAsync(id);

            if (medicalTest == null)
            {
                return NotFound();
            }

            return medicalTest;
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalTest(int id, MedicalTest medicalTest)
        {
            if (id != medicalTest.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicalTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalTestExists(id))
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

        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedicalTest>> PostMedicalTest(MedicalTest medicalTest)
        {
            _context.Tests.Add(medicalTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedicalTest), new { id = medicalTest.Id }, medicalTest);
            // The C# nameof keyword is used to avoid hard-coding 
            // the action name in the CreatedAtAction call.
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalTest(int id)
        {
            var medicalTest = await _context.Tests.FindAsync(id);
            if (medicalTest == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(medicalTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalTestExists(int id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }
    }
}
