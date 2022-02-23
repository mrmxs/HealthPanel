using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HealthPanel.Services.Stats.DAL
{
    public class Mapper : IMapper
    {
        private readonly HealthPanelDbContext _context;
        private readonly ISugarContext _sugar;

        public Mapper(HealthPanelDbContext context, ISugarContext sugar)
        {
            _context = context;
            _sugar = sugar;
        }

        #region Basic

        public async Task<IDto> Map(IEntity entity)
        {
            return entity.GetType().Name switch
            {
                "MedTest" =>
                    await this.Map<MedTest, MedTestDto>(entity as MedTest),
                "LabTest" =>
                    await this.Map<LabTest, LabTestDto>(entity as LabTest),
                "Examination" =>
                    await this.Map<Examination, ExaminationDto>(entity as Examination),
                "TestPanel" =>
                    await this.Map<TestPanel, TestPanelDto>(entity as TestPanel),
                "LabTestPanel" =>
                    await this.Map<LabTestPanel, LabTestPanelDto>(entity as LabTestPanel),
                "TestList" =>
                    await this.Map<TestList, TestListDto>(entity as TestList),

                _ => throw new System.NotImplementedException(),
            };
        }

        public async Task<MedTestDto> Map<T, D>(MedTest entity)
            where T : MedTest where D : MedTestDto
            => new(entity);

        public async Task<LabTestDto> Map<T, D>(LabTest entity)
            where T : LabTest where D : LabTestDto
        {
            var branchEntity = await _sugar.HFBs.Id(entity.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(entity.TestId);

            return new LabTestDto(
                labTestEntity: entity,
                branchEntity: branchEntity,
                medTestEntity: medTestEntity
            );
        }

        public async Task<ExaminationDto> Map<T, D>(Examination entity)
            where T : Examination where D : ExaminationDto
        {
            var branchEntity = await _sugar.HFBs.Id(entity.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(entity.TestId);

            return new ExaminationDto(
                examinationEntity: entity,
                branchEntity: branchEntity,
                medTestEntity: medTestEntity
            );
        }

        public async Task<TestPanelDto> Map<T, D>(TestPanel entity)
            where T : TestPanel where D : TestPanelDto
            => new(entity);

        public async Task<LabTestPanelDto> Map<T, D>(LabTestPanel entity)
            where T : LabTestPanel where D : LabTestPanelDto
        {
            var branchEntity = await _sugar.HFBs.Id(entity.HealthFacilityBranchId);
            var testPanelEntity = await _sugar.TPs.Id(entity.TestPanelId);

            var labTestEntities = entity.LabTestIds.ToList()
                .Select(async p => await _sugar.LTests.Id(p))
                .Select(t => t.Result).Where(i => i != null)
                .ToList<LabTest>();

            var medTestEntities = labTestEntities
                .Select(p => p.TestId).ToList()
                .Select(async p => await _sugar.Tests.Id(p))
                .Select(t => t.Result).Where(i => i != null)
                .ToList<MedTest>();

            return new LabTestPanelDto(
                labTestPanelEntity: entity,
                branchEntity: branchEntity,
                testPanelEntity: testPanelEntity,
                labTestEntities: labTestEntities,
                medTestEntities: medTestEntities
            );
        }

        #endregion

        #region Extended

        public async Task<TestListDto> Map<T, D>(TestList entity)
            where T : TestList where D : TestListDto
            => new(entity);
        public IEnumerable<TestListItemDto> Map<T, D>(
            TestListType type, IEnumerable<TestToTestList> tttls)
            where T : IEnumerable<TestToTestList>
            where D : IEnumerable<TestListItemDto>
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
                return new TestListItemDto(
                    index: p.Index,
                    type: type,
                    item: await this.Map(entity)
                );
            }).Select(t => t.Result);
        }

        public async Task<TestListExtendedDto> Map<T>(TestList entity)
            where T : TestListExtendedDto
        {
            var tttls = new List<TestToTestList>(
               await _context.TestsToTestList.ToListAsync()
           ).Where(p => p.TestListId == entity.Id);

            // Get DTOs to all items and add them to the TestList Index
            return new TestListExtendedDto(entity)
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.MedTest, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.LabTest, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.Examination, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.TestPanel, tttls))
                .Add(this.Map<IEnumerable<TestToTestList>, IEnumerable<TestListItemDto>>(
                    TestListType.LabTestPanel, tttls));
        }

        #endregion
    }
}