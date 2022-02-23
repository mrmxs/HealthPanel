using System.Threading.Tasks;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.DAL;
using HealthPanel.Services.Stats.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestListExtendedController : ControllerBase
    // : AbstractController<TestList, TestListExtendedDto>
    // TODO : AbstractROController<TestList, TestListExtendedDto>
    {
        private readonly HealthPanelDbContext _context;
        private readonly ISugarContext _sugar;
        private readonly IMapper _mapper;

        public TestListExtendedController(HealthPanelDbContext context)
        {
            _context = context;
            _sugar = new SugarContext(context);
            _mapper = new Mapper(context, _sugar);
        }

        // GET: api/TestListExtended/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestListExtendedDto>> Get(int id)
        {
            var entity = await _sugar.TLs.Id(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(await _mapper.Map<TestListExtendedDto>(entity));
        }
    }
}
