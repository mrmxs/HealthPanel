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
        Task<TestToTestListDto> Map<T, D>(TestToTestList entity)
            where T : TestToTestList
            where D : TestToTestListDto;


        Task<UserDto> Map<T, D>(User entity)
            where T : User
            where D : UserDto;
        Task<UserLabTestDto> Map<T, D>(UserLabTest entity)
            where T : UserLabTest
            where D : UserLabTestDto;
        Task<UserExaminationDto> Map<T, D>(UserExamination entity)
            where T : UserExamination
            where D : UserExaminationDto;

        Task<HealthFacilityDto> Map<T, D>(HealthFacility entity)
            where T : HealthFacility
            where D : HealthFacilityDto;
        Task<HealthFacilityBranchDto> Map<T, D>(HealthFacilityBranch entity)
            where T : HealthFacilityBranch
            where D : HealthFacilityBranchDto;
        Task<DepartmentDto> Map<T, D>(Department entity)
            where T : Department
            where D : DepartmentDto;
        Task<DoctorDto> Map<T, D>(Doctor entity)
            where T : Doctor
            where D : DoctorDto;
        Task<HospitalizationDto> Map<T, D>(Hospitalization entity)
        where T : Hospitalization
        where D : HospitalizationDto;

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