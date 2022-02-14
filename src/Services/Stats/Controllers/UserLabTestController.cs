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
        private readonly HealthPanelDbContext _context;

        public UserLabTestController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/UserLabTest
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserLabTestDto>>> Get()
        {
            var entities = await _context.UserLabTests.ToListAsync();

            var dtos = entities
                .Select(async p => await this.EntityToDtoAsync(p))
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

            return Ok(await this.EntityToDtoAsync(entity));
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
            _context.UserLabTests.Add(this.ConvertToEntity(dto));
            
            var newEntityId = await _context.SaveChangesAsync();
            var newEntity = await _context.UserLabTests.FindAsync(newEntityId);

            return CreatedAtAction(nameof(Post),
                new { id = newEntity.Id },
                await this.EntityToDtoAsync(newEntity));
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

        protected override async Task<UserLabTestDto> EntityToDtoAsync(UserLabTest entity)  
        {
            var labTest =
                await _context.LabTests.FindAsync(entity.LabTestId);

            //todo move to repository 
            return new UserLabTestDto(
                userTestEntity: entity, 
                labTestEntity:  labTest,
                branchEntity:   await _context.HealthFacilityBranches
                    .FindAsync(labTest.HealthFacilityBranchId),
                medTestEntity:  await _context.Tests.FindAsync(labTest.TestId),
                userEntity:           await _context.Users.FindAsync(entity.UserId) 
            );
        }

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
