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

            var medTests = tttls.Where(p => p.MedTestId != 0)
                .Select(async p => new TestListItemDto()
                {
                    Index = p.Index,
                    Type = TestListType.MedTest,
                    Item = await _mapper.Map(await _sugar.Tests.Id(p.MedTestId)),
                }).Select(t => t.Result);

            var labTests = tttls.Where(p => p.LabTestId != 0)
                .Select(async p => new TestListItemDto()
                {
                    Index = p.Index,
                    Type = TestListType.LabTest,
                    Item = await _mapper.Map(await _sugar.LTests.Id(p.LabTestId))
                }).Select(t => t.Result);

            var examinations = tttls.Where(p => p.ExaminationId != 0)
                .Select(async p => new TestListItemDto()
                {
                    Index = p.Index,
                    Type = TestListType.Examination,
                    Item = await _mapper.Map(await _sugar.Exams.Id(p.ExaminationId))
                }).Select(t => t.Result);

            var testPanels = tttls.Where(p => p.TestPanelId != 0)
                .Select(async p => new TestListItemDto()
                {
                    Index = p.Index,
                    Type = TestListType.TestPanel,
                    Item = await _mapper.Map(await _sugar.TPs.Id(p.TestPanelId))
                }).Select(t => t.Result);

            var labTestPanels = tttls.Where(p => p.LabTestPanelId != 0)
                .Select(async p => new TestListItemDto()
                {
                    Index = p.Index,
                    Type = TestListType.LabTestPanel,
                    Item = await _mapper.Map(await _sugar.LTPs.Id(p.LabTestPanelId))
                }).Select(t => t.Result);

            // Add DTOs to the tList Index
            var tList = new TestListExtendedDto(entity)
                .Add(medTests)
                .Add(labTests)
                .Add(examinations)
                .Add(testPanels)
                .Add(labTestPanels);

            // Return beautiful TestListExtended
            return tList;
        }
    }
}
