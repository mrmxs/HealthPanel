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
    public class UserExaminationController
        : AbstractController<UserExamination, UserExaminationDto>
    {
        public UserExaminationController(HealthPanelDbContext context) : base(context) { }

        // GET: api/UserExamination
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserExaminationDto>>> Get()
        {
            var entities = await _context.UserExaminations.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<UserExamination, UserExaminationDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/UserExamination/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<UserExaminationDto>> Get(int id)
        {
            var entity = await _context.UserExaminations.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<UserExamination, UserExaminationDto>(entity));
        }

        // PUT: api/UserExamination/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, UserExaminationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.UserExaminations.FindAsync(id);

            modified.ExaminationId = dto.ExaminationId;
            modified.UserId = dto.UserId;
            modified.DoctorId = dto.DoctorId;
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

        // POST: api/UserExamination
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<UserExaminationDto>> Post(
            UserExaminationDto dto)
        {
            _context.UserExaminations.Add(this.ConvertToEntity(dto));

            var newEntityId = await _context.SaveChangesAsync();
            var newEntity = await _context.UserExaminations.FindAsync(newEntityId);

            return CreatedAtAction(nameof(Post),
                new { id = newEntity.Id },
                await _mapper.Map<UserExamination, UserExaminationDto>(newEntity));
        }

        // DELETE: api/UserExamination/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.UserExaminations.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.UserExaminations.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id) 
            => _context.UserExaminations.Any(e => e.Id == id);

        private UserExamination ConvertToEntity(UserExaminationDto dto)
            => new()
            {
                ExaminationId = dto.ExaminationId,
                UserId = dto.UserId,
                DoctorId = dto.DoctorId,
                Date = dto.Date,
                Value = dto.Value,
                Status = (TestStatus)
                        Enum.Parse(typeof(TestStatus), dto.Status),
            };

    }
}
