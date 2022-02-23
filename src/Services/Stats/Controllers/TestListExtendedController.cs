using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.DAL;
using HealthPanel.Services.Stats.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthPanel.Services.Stats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestListExtendedController : ControllerBase
    // : AbstractController<TestList, TestListExtendedDto>
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

            return Ok(await this.EntityToDtoAsync(entity));
        }

        protected async Task<TestListExtendedDto> EntityToDtoAsync(TestList entity)
        {
            // Get lists of all added types from TestsToTLists
            var tttls = new List<TestToTestList>(
                await _context.TestsToTestList.ToListAsync()
            ).Where(p => p.TestListId == entity.Id);

            // Get DTOs to all items and add them to the TestList Index
            return new TestListExtendedDto(entity)
                .Add(this.TestListItemDtos(TestListType.MedTest, tttls))
                .Add(this.TestListItemDtos(TestListType.LabTest, tttls))
                .Add(this.TestListItemDtos(TestListType.Examination, tttls))
                .Add(this.TestListItemDtos(TestListType.TestPanel, tttls))
                .Add(this.TestListItemDtos(TestListType.LabTestPanel, tttls));
        }

        private IEnumerable<TestListItemDto> TestListItemDtos(
               TestListType type,
               IEnumerable<TestToTestList> tttls)
        {
            int id = 0;

            return tttls.Where(p =>
            {
                // Find all ids of given type
                id = type switch
                {
                    TestListType.MedTest => p.MedTestId,
                    TestListType.LabTest => p.LabTestId,
                    TestListType.Examination => p.ExaminationId,
                    TestListType.TestPanel => p.TestPanelId,
                    TestListType.LabTestPanel => p.LabTestPanelId,
                    _ => throw new NotImplementedException(),
                };

                return id != 0;
            })
            .Select(async p =>
            {
                // Get entity of given type by id
                IEntity entity = type switch
                {
                    TestListType.MedTest => await _sugar.Tests.Id(id),
                    TestListType.LabTest => await _sugar.LTests.Id(id),
                    TestListType.Examination => await _sugar.Exams.Id(id),
                    TestListType.TestPanel => await _sugar.TPs.Id(id),
                    TestListType.LabTestPanel => await _sugar.LTPs.Id(id),
                    _ => throw new NotImplementedException(),
                };

                // Map Entity to TestListItemDto
                return new TestListItemDto()
                {
                    Index = p.Index,
                    Type = type,
                    Item = await _mapper.Map(entity)
                };
            }).Select(t => t.Result);
        }
    }
}
