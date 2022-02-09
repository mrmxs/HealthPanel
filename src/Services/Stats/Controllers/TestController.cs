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
    public class TestController : AbstractController<MedTestDto>
    {
        private readonly HealthPanelDbContext _context;

        public TestController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/Test
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<MedTestDto>>> Get()
        {
            var entities = await _context.Tests.ToListAsync();
            
            return Ok(entities.Select(t => this.ConvertToDto(t)));
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<MedTestDto>> Get(int id)
        {
            var medicalTest = await _context.Tests.FindAsync(id);

            if (medicalTest == null)
            {
                return NotFound();
            }

            return Ok(this.ConvertToDto(medicalTest));
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, MedTestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var medicalTest = await _context.Tests.FindAsync(id);
                       
            medicalTest.Name = dto.Name; //todo bad practice
            medicalTest.Units = dto.Units;
            _context.Entry(medicalTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedTestExists(id))
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
        public override async Task<ActionResult<MedTestDto>> Post(MedTestDto dto)
        {
            var medicalTest = ConvertToEntity(dto);
            _context.Tests.Add(medicalTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = medicalTest.Id }, medicalTest);
            // The C# nameof keyword is used to avoid hard-coding 
            // the action name in the CreatedAtAction call.
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
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

        private bool MedTestExists(int id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }
        
        //todo place your refactor here
        private MedTestDto ConvertToDto(object raw)
        {
            var entity = raw as MedTest; 
            
            return new MedTestDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Units = entity.Units,
            };
        }

        private MedTest ConvertToEntity(MedTestDto dto)
        {
            return new MedTest
            {
                // Id = lab.Id,
                Name = dto.Name,
                Units = dto.Units,
            };
        }
    }
}
