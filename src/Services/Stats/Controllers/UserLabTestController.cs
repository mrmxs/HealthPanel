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
    public class UserLabTestController
        : AbstractController<UserLabTest, UserLabTestDto>
    {
        public UserLabTestController(HealthPanelDbContext context) : base(context) { }

        // GET: api/UserLabTest
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserLabTestDto>>> Get()
        {
            var entities = await _context.UserLabTests.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<UserLabTest, UserLabTestDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();
            
            return Ok(dtos);
        }

        // GET: api/UserLabTest/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<UserLabTestDto>> Get(int id)
        {
            var entity = await _context.UserLabTests.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<UserLabTest, UserLabTestDto>(entity));
        }

        // PUT: api/UserLabTest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, UserLabTestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.UserLabTests.FindAsync(id);

            modified.LabTestId = dto.LabTestId;
            modified.UserId = dto.UserId;
            modified.Date = dto.Date;
            modified.Value = dto.Value;
            modified.Status = (TestStatus)
                    Enum.Parse(typeof(TestStatus), dto.Status);

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

        // POST: api/UserLabTest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<UserLabTestDto>> Post(
            UserLabTestDto dto)
        {
            var entity = this.ConvertToEntity(dto);

            _context.UserLabTests.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = entity.Id },
                await _mapper.Map<UserLabTest, UserLabTestDto>(entity));
        }

        // DELETE: api/UserLabTest/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.UserLabTests.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.UserLabTests.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.UserLabTests.Any(e => e.Id == id);

        private UserLabTest ConvertToEntity(UserLabTestDto dto)
        {
            return new UserLabTest
            {
                LabTestId = dto.LabTestId,
                UserId = dto.UserId,
                Date = dto.Date,
                Value = dto.Value,
                Status = (TestStatus)
                    Enum.Parse(typeof(TestStatus), dto.Status),
            };
        }
    }
}
