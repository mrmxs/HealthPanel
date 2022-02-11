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

namespace HealthPanel.Services.Stats.Controllers //Stats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AbstractController<T,D> : ControllerBase 
            where T : IEntity
            where D : IDto
    {
        // GET api/base
        [HttpGet]
        public abstract Task<ActionResult<IEnumerable<D>>> Get();

        // GET api/base/5
        [HttpGet("{id}")]
        public abstract Task<ActionResult<D>> Get(int id);

        // POST api/base
        [HttpPost]
        public abstract Task<ActionResult<D>> Post([FromBody] D value);

        // PUT api/base/5
        [HttpPut("{id}")]
        public abstract Task<IActionResult> Put(int id, [FromBody] D value);

        // DELETE api/base/5
        [HttpDelete("{id}")]
        public abstract Task<IActionResult> Delete(int id);


        protected abstract Task<D> EntityToDtoAsync(T entity);

        protected BadRequestObjectResult CustomBadRequest(object error)
        {
            return BadRequest(new { Error = error });
        }
    }
}