using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public TestListExtendedController(HealthPanelDbContext context)
        {
            _context = context;
        }

        // GET: api/TestListExtended/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestListExtendedDto>> Get(int id)
        {
            var entity = await _context.TestLists.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(this.EntityToDtoAsync(entity));
        }

        // protected bool Exists(int id)
        //     => _context.TestLists.Any(e => e.Id == id);

        protected async Task<TestListExtendedDto> EntityToDtoAsync(TestList entity)
        {
            // Get lists of all added types from TestsToTLists
            var tttls = new List<TestToTestList>(
                await _context.TestsToTestList.ToListAsync()
            ).Where(p => p.TestListId == entity.Id);

            // TestListItem.Item<object> stores itemID<int>
            var medTestIds = tttls.Where(p => p.MedTestId != 0)
                .Select(p => new TestListItemDto() { Index = p.Index, Item = p.MedTestId });
            var labTestIds = tttls.Where(p => p.LabTestId != 0)
                .Select(p => new TestListItemDto() { Index = p.Index, Item = p.LabTestId });
            var examinationIds = tttls.Where(p => p.ExaminationId != 0)
                .Select(p => new TestListItemDto() { Index = p.Index, Item = p.ExaminationId });
            var testPanelIds = tttls.Where(p => p.TestPanelId != 0)
                .Select(p => new TestListItemDto() { Index = p.Index, Item = p.TestPanelId });
            var labTestPanelIds = tttls.Where(p => p.LabTestPanelId != 0)
                .Select(p => new TestListItemDto() { Index = p.Index, Item = p.LabTestPanelId });

            // Convert all IDs to DTOs, get all additional data 
            // TestListItem.Item<object> stores itemDto<IDto>
            var medTests = medTestIds.Select(async id =>
                {
                    var ent = await _context.Tests.FindAsync(id.Item);

                    return new TestListItemDto()
                    {
                        Index = id.Index,
                        Type = TestListType.MedTest,
                        Item = new MedTestDto(testEntity: ent),
                    };
                })
                .Select(t => t.Result);

            var labTests = labTestIds.Select(async id =>
                {
                    var ent = await _context.LabTests.FindAsync(id.Item);
                    var branchEntity =
                        await _context.HealthFacilityBranches.FindAsync(ent.HealthFacilityBranchId);
                    var medTestEntity = await _context.Tests.FindAsync(ent.TestId);

                    return new TestListItemDto()
                    {
                        Index = id.Index,
                        Type = TestListType.LabTest,
                        Item = new LabTestDto(
                            labTestEntity: ent,
                            branchEntity: branchEntity,
                            medTestEntity: medTestEntity
                        )
                    };
                }).Select(t => t.Result);

            var examinations = examinationIds.Select(async id =>
                {
                    var ent = await _context.Examinations.FindAsync(id.Item);
                    var medTestEntity = await _context.Tests.FindAsync(ent.TestId);
                    var branchEntity =
                        await _context.HealthFacilityBranches.FindAsync(ent.HealthFacilityBranchId);

                    return new TestListItemDto()
                    {
                        Index = id.Index,
                        Type = TestListType.Examination,
                        Item = new ExaminationDto(
                            examinationEntity: ent,
                            branchEntity: branchEntity,
                            medTestEntity: medTestEntity
                        )
                    };
                }).Select(t => t.Result);

            var testPanels = testPanelIds.Select(async id =>
                {
                    var ent = await _context.TestPanels.FindAsync(id.Item);
                    id.Item = new TestPanelDto(ent);
                    id.Type = TestListType.TestPanel;

                    return new TestListItemDto()
                    {
                        Index = id.Index,
                        Type = TestListType.TestPanel,
                        Item = new TestPanelDto(entity: ent),
                    };
                })
                .Select(t => t.Result);

            var labTestPanels = labTestPanelIds.Select(async id =>
                {
                    var ent = await _context.LabTestPanels.FindAsync(id.Item);

                    HealthFacilityBranch branchEntity =
                        await _context.HealthFacilityBranches
                            .FindAsync(ent.HealthFacilityBranchId);

                    TestPanel testPanelEntity =
                        await _context.TestPanels
                            .FindAsync(ent.TestPanelId);

                    List<LabTest> labTestEntities = ent.LabTestIds.ToList()
                        .Select(async p => await _context.LabTests.FindAsync(p))
                        .Select(t => t.Result)
                        .Where(i => i != null)
                        .ToList<LabTest>();

                    List<MedTest> medTestEntities = labTestEntities
                        .Select(p => p.TestId).ToList()
                        .Select(async p => await _context.Tests.FindAsync(p))
                        .Select(t => t.Result)
                        .Where(i => i != null)
                        .ToList<MedTest>();

                    return new TestListItemDto()
                    {
                        Index = id.Index,
                        Type = TestListType.LabTestPanel,
                        Item = new LabTestPanelDto(
                            labTestPanelEntity: ent,
                            branchEntity: branchEntity,
                            testPanelEntity: testPanelEntity,
                            labTestEntities: labTestEntities,
                            medTestEntities: medTestEntities),
                    };
                })
                .Select(t => t.Result);

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
