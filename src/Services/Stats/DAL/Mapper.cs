using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;
using HealthPanel.Services.Stats.Dtos;

namespace HealthPanel.Services.Stats.DAL
{
    public interface IMapper
    {
        Task<IDto> Map(IEntity entity);
        Task<MedTestDto> Map(MedTest entity);
        Task<LabTestDto> Map(LabTest entity);
        Task<ExaminationDto> Map(Examination entity);
        Task<TestPanelDto> Map(TestPanel entity);
        Task<LabTestPanelDto> Map(LabTestPanel entity);

    }

    public class Mapper : IMapper
    {
        private readonly HealthPanelDbContext _context;
        private readonly ISugarContext _sugar;

        public Mapper(HealthPanelDbContext context, ISugarContext sugar)
        {
            _context = context;
            _sugar = sugar;
        }

        public async Task<IDto> Map(IEntity entity)
        {
            return entity.GetType().FullName switch
            {
                "HealthPanel.Core.Entities.MedTest" => await this.Map(entity as MedTest),
                "HealthPanel.Core.Entities.LabTest" => await this.Map(entity as LabTest),
                "HealthPanel.Core.Entities.Examination" => await this.Map(entity as Examination),
                "HealthPanel.Core.Entities.TestPanel" => await this.Map(entity as TestPanel),
                "HealthPanel.Core.Entities.LabTestPanel" => await this.Map(entity as LabTestPanel),

                _ => throw new System.NotImplementedException(),
            };
        }

        public async Task<MedTestDto> Map(MedTest entity) => new(entity);
        public async Task<LabTestDto> Map(LabTest entity)
        {
            var branchEntity = await _sugar.HFBs.Id(entity.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(entity.TestId);

            return new LabTestDto(
                labTestEntity: entity,
                branchEntity: branchEntity,
                medTestEntity: medTestEntity
            );
        }
        public async Task<ExaminationDto> Map(Examination entity)
        {
            var branchEntity = await _sugar.HFBs.Id(entity.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(entity.TestId);

            return new ExaminationDto(
                examinationEntity: entity,
                branchEntity: branchEntity,
                medTestEntity: medTestEntity
            );
        }
        public async Task<TestPanelDto> Map(TestPanel entity) => new(entity);
        public async Task<LabTestPanelDto> Map(LabTestPanel entity)
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
    }
}