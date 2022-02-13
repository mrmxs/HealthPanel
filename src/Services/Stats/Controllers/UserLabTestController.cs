using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthPanel.Core.Entities;
using HealthPanel.Core.Enums;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;

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
            var userLabTest = await _context.UserLabTests.FindAsync(id);

            if (userLabTest == null)
            {
                return NotFound();
            }

             return Ok(await this.EntityToDtoAsync(userLabTest));
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

            var userLabTest = await _context.UserLabTests.FindAsync(id);

            userLabTest.LabMedTestId = dto.LabTestId; //todo !!!!
            userLabTest.UserId = dto.UserId;
            userLabTest.Date = dto.Date;
            userLabTest.Value = dto.Value;
            userLabTest.Status = (TestStatus)
                    Enum.Parse(typeof(TestStatus), dto.Status);

            _context.Entry(userLabTest).State = EntityState.Modified;

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
        public override async Task<ActionResult<UserLabTestDto>> Post(UserLabTestDto dto)
        {
            _context.UserLabTests.Add(this.ConvertToEntity(dto));
            var resultId = await _context.SaveChangesAsync();

            var userLabTest = await _context.UserLabTests.FindAsync(resultId);
            var result = await this.EntityToDtoAsync(userLabTest);

            return CreatedAtAction(nameof(Post), new { id = resultId }, result);
        }

        // DELETE: api/UserLabTest/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var userLabTest = await _context.UserLabTests.FindAsync(id);
            if (userLabTest == null)
            {
                return NotFound();
            }

            _context.UserLabTests.Remove(userLabTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
        {
            return _context.UserLabTests.Any(e => e.Id == id);
        }

        // //todo move to repository
        // private async Task<IEnumerable<object>> GetEntities(UserLabTest entity) 
        // {
        //     var labTest = await _context.LabTests.FindAsync(entity.LabTestId); 
            
        //     var branch = await _context.HealthFacilityBranches
        //         .FindAsync(labTest.HealthFacilityBranchId);
        //     var test = await _context.Tests.FindAsync(labTest.TestId);
            
        //     var user = await _context.Users.FindAsync(entity.UserId);
            
        //     return new List<object>{ entity, labTest, branch, test, user };
        // }

        protected override async Task<UserLabTestDto> EntityToDtoAsync(UserLabTest entity)  
        {
             var labTest = await _context.LabTests.FindAsync(entity.LabMedTestId); //todo !!!!
            
            // var branch = await _context.HealthFacilityBranches
            //     .FindAsync(labTest.HealthFacilityBranchId);
            // var test = await _context.Tests.FindAsync(labTest.TestId);
            
            // var user = await _context.Users.FindAsync(entity.UserId);
           
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

        // private UserLabTestDto ConvertToDto(IEnumerable<object> entities)
        // {
        //     return new UserLabTestDto(entities);
        // }

        private UserLabTest ConvertToEntity(UserLabTestDto dto)
        {
            return new UserLabTest
            {
                LabMedTestId = dto.LabTestId, //todo !!!!!
                UserId = dto.UserId,
                Date = dto.Date,
                Value = dto.Value,
                Status = (TestStatus)
                    Enum.Parse(typeof(TestStatus), dto.Status),
            };
        }
    }
}
