using System.Collections.Generic;
using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Services.Stats.Dtos;

namespace HealthPanel.Services.Stats.DAL
{
    public interface IMapper
    {
        #region Basic

        Task<IDto> Map(IEntity entity);

        Task<MedTestDto> Map<T, D>(MedTest entity)
            where T : MedTest
            where D : MedTestDto;

        Task<LabTestDto> Map<T, D>(LabTest entity)
            where T : LabTest
            where D : LabTestDto;

        Task<ExaminationDto> Map<T, D>(Examination entity)
            where T : Examination
            where D : ExaminationDto;

        Task<TestPanelDto> Map<T, D>(TestPanel entity)
            where T : TestPanel
            where D : TestPanelDto;

        Task<LabTestPanelDto> Map<T, D>(LabTestPanel entity)
            where T : LabTestPanel
            where D : LabTestPanelDto;

        Task<TestListDto> Map<T, D>(TestList entity)
            where T : TestList
            where D : TestListDto;

        #endregion

        #region Extended

        IEnumerable<TestListItemDto> Map<T, D>(
        TestListType type, IEnumerable<TestToTestList> tttls)
            where T : IEnumerable<TestToTestList>
            where D : IEnumerable<TestListItemDto>;

        Task<TestListExtendedDto> Map<T>(TestList entity)
            where T : TestListExtendedDto;

        #endregion
    }
}