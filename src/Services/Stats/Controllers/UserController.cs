using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
        : AbstractController<User, UserDto>
    {
        public UserController(HealthPanelDbContext context) : base(context) { }

        // GET: api/User
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var entities = await _context.Users.ToListAsync();

            var dtos = entities
                .Select(async p => await _mapper.Map<User, UserDto>(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<UserDto>> Get(int id)
        {
            var entity = await _context.Users.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<User, UserDto>(entity));
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(int id, UserDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var modified = await _context.Users.FindAsync(id);

            modified.Name = dto.Name; //todo bad practice
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

        // POST: api/User
        // todo: To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public override async Task<ActionResult<UserDto>> Post(UserDto dto)
        {
            var entity = ConvertToEntity(dto);

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post),
                new { id = entity.Id },
                await _mapper.Map<User, UserDto>(entity));
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
            => _context.Users.Any(e => e.Id == id);

        private User ConvertToEntity(UserDto dto)
        => new()
        {
            // Id = dto.Id,
            Name = dto.Name,
        };
    }
}
