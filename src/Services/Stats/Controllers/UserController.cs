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
    public class UserController
        : AbstractController<User, UserDto>
    {
        private readonly HealthPanelDbContext _context;

        public UserController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var entities = await _context.Users.ToListAsync();

            var dtos = entities
                .Select(async p => await this.EntityToDtoAsync(p))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList();

            return Ok(dtos);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(await this.EntityToDtoAsync(user));
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

            var user = await _context.Users.FindAsync(id);

            user.Name = dto.Name; //todo bad practice
            _context.Entry(user).State = EntityState.Modified;

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
            var user = ConvertToEntity(dto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        protected override bool Exists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        protected override async Task<UserDto> EntityToDtoAsync(User entity)
            => new UserDto(entity);

        private User ConvertToEntity(UserDto dto)
        {
            return new User
            {
                // Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
