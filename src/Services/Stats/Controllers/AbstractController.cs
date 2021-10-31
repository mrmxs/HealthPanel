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
    public abstract class AbstractController<T> : ControllerBase where T : IDto
    {
        // GET api/base
        [HttpGet]
        public abstract Task<ActionResult<IEnumerable<T>>> Get();

        // GET api/base/5
        [HttpGet("{id}")]
        public abstract Task<ActionResult<T>> Get(int id);

        // POST api/base
        [HttpPost]
        public abstract Task<ActionResult<T>> Post([FromBody] T value);

        // PUT api/base/5
        [HttpPut("{id}")]
        public abstract Task<IActionResult/*ActionResult<T>*/> Put(int id, [FromBody] T value);

        // DELETE api/base/5
        [HttpDelete("{id}")]
        public abstract Task<IActionResult/*ActionResult<T>*/> Delete(int id);

        protected BadRequestObjectResult CustomBadRequest(object error)
        {
            return BadRequest(new { Error = error });
        }
    }
}