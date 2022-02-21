using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

            return Ok(await this.EntityToDtoAsync(entity));
        }

        protected bool Exists(int id)
            => _context.TestLists.Any(e => e.Id == id);

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
                    var entt = await _context.Tests.FindAsync(id.Item);

                    id.Type = TestListType.MedTest;
                    id.Item = new MedTestDto(entt);

                    return id;
                }).Select(t => t.Result);

            var labTests = labTestIds.Select(async id =>
                {
                    var entt = await _context.LabTests.FindAsync(id.Item);

                    id.Type = TestListType.LabTest;
                    id.Item = new LabTestDto(
                        labTestEntity: entt,
                        branchEntity: await _context.HealthFacilityBranches
                            .FindAsync(entt.HealthFacilityBranchId),
                        medTestEntity: await _context.Tests.FindAsync(entt.TestId)
                    );

                    return id;
                }).Select(t => t.Result);

            var examinations = examinationIds.Select(async id =>
                {
                    var entt = await _context.Examinations.FindAsync(id.Item);

                    id.Type = TestListType.Examination;
                    id.Item = new ExaminationDto(
                        examinationEntity: entt,
                        branchEntity: await _context.HealthFacilityBranches
                            .FindAsync(entt.HealthFacilityBranchId),
                        medTestEntity: await _context.Tests.FindAsync(entt.TestId)
                    );

                    return id;
                }).Select(t => t.Result);

            var testPanels = testPanelIds.Select(async id =>
                {
                    var entt = await _context.TestPanels.FindAsync(id.Item);
                    id.Item = new TestPanelDto(entt);
                    id.Type = TestListType.TestPanel;

                    return id;
                }).Select(t => t.Result);

            var labTestPanels = labTestPanelIds.Select(async id =>
                {
                    var entt = await _context.LabTestPanels.FindAsync(id.Item);

                    HealthFacilityBranch branchEntity =
                        await _context.HealthFacilityBranches
                            .FindAsync(entt.HealthFacilityBranchId);

                    TestPanel testPanelEntity =
                        await _context.TestPanels
                            .FindAsync(entt.TestPanelId);

                    List<LabTest> labTestEntities = entt.LabTestIds.ToList()
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

                    id.Type = TestListType.LabTestPanel;
                    id.Item = new LabTestPanelDto(
                        labTestPanelEntity: entt,
                        branchEntity: branchEntity,
                        testPanelEntity: testPanelEntity,
                        labTestEntities: labTestEntities,
                        medTestEntities: medTestEntities);

                    return id;
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
