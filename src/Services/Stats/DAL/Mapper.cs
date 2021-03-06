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
    public partial class Mapper : IMapper
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
            Dictionary<Type, int> types = new()
            {
                { typeof(MedTest), 0 },
                { typeof(LabTest), 1 },
                { typeof(Examination), 2 },
                { typeof(TestPanel), 3 },
                { typeof(LabTestPanel), 4 },
                { typeof(TestList), 5 },
                { typeof(TestToTestList), 6 },
                { typeof(User), 7 },
                { typeof(UserLabTest), 8 },
                { typeof(UserExamination), 9 },
                { typeof(HealthFacility), 10 },
                { typeof(HealthFacilityBranch), 11 },
                { typeof(Doctor), 12 },
                { typeof(Department), 13 },
                { typeof(Hospitalization), 14 },
                { typeof(UserHospitalization), 15 },
                { typeof(Consultation), 16 },
                { typeof(UserConsultation), 17 },
            };

            return types[entity.GetType()] switch
            {
                0 => await this.Map<MedTest, MedTestDto>(entity as MedTest),
                1 => await this.Map<LabTest, LabTestDto>(entity as LabTest),
                2 => await this.Map<Examination, ExaminationDto>(entity as Examination),
                16 => await this.Map<Consultation, ConsultationDto>(entity as Consultation),
                3 => await this.Map<TestPanel, TestPanelDto>(entity as TestPanel),
                4 => await this.Map<LabTestPanel, LabTestPanelDto>(entity as LabTestPanel),

                5 => await this.Map<TestList, TestListDto>(entity as TestList),
                6 => await this.Map<TestToTestList, TestToTestListDto>(entity as TestToTestList),

                7 => await this.Map<User, UserDto>(entity as User),
                8 => await this.Map<UserLabTest, UserLabTestDto>(entity as UserLabTest),
                9 => await this.Map<UserExamination, UserExaminationDto>(entity as UserExamination),
                17 => await this.Map<UserConsultation, UserConsultationDto>(entity as UserConsultation),
                15 => await this.Map<UserHospitalization, UserHospitalizationDto>(entity as UserHospitalization),

                10 => await this.Map<HealthFacility, HealthFacilityDto>(entity as HealthFacility),
                11 => await this.Map<HealthFacilityBranch, HealthFacilityBranchDto>(entity as HealthFacilityBranch),
                13 => await this.Map<Department, DepartmentDto>(entity as Department),
                12 => await this.Map<Doctor, DoctorDto>(entity as Doctor),
                14 => await this.Map<Hospitalization, HospitalizationDto>(entity as Hospitalization),

                _ => throw new Exception(),
            };
        }

        #region Basic Test Types

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

        public async Task<ConsultationDto> Map<T, D>(Consultation entity)
            where T : Consultation where D : ConsultationDto
        {
            var branchEntity = await _sugar.HFBs.Id(entity.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(entity.TestId);

            return new ConsultationDto(
                consultatioEntity: entity,
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

        public async Task<TestListDto> Map<T, D>(TestList entity)
            where T : TestList
            where D : TestListDto
            => new(entity);

        public async Task<TestToTestListDto> Map<T, D>(TestToTestList entity)
            where T : TestToTestList
            where D : TestToTestListDto
            => new(entity);

        #endregion

        #region User

        public async Task<UserDto> Map<T, D>(User entity)
            where T : User
            where D : UserDto
            => new(entity);

        public async Task<UserLabTestDto> Map<T, D>(UserLabTest entity)
            where T : UserLabTest
            where D : UserLabTestDto
        {
            var labTest = await _sugar.LTests.Id(entity.LabTestId);
            var branchEntity = await _sugar.HFBs.Id(labTest.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(labTest.TestId);
            var userEntity = await _sugar.Usrs.Id(entity.UserId);

            return new UserLabTestDto(
                userTestEntity: entity,
                labTestEntity: labTest,
                branchEntity: branchEntity,
                medTestEntity: medTestEntity,
                userEntity: userEntity
            );
        }

        public async Task<UserExaminationDto> Map<T, D>(UserExamination entity)
            where T : UserExamination
            where D : UserExaminationDto
        {
            var examination = await _sugar.Exams.Id(entity.ExaminationId);
            var branchEntity = await _sugar.HFBs.Id(examination.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(examination.TestId);
            var doctorEntity = await _sugar.Docs.Id(entity.DoctorId);
            var userEntity = await _sugar.Usrs.Id(entity.UserId);

            return new UserExaminationDto(
                 userExaminationEntity: entity,
                 examinationEntity: examination,
                 branchEntity: branchEntity,
                 medTestEntity: medTestEntity,
                 doctorEntity: doctorEntity,
                 userEntity: userEntity
             );
        }

        public async Task<UserConsultationDto> Map<T, D>(UserConsultation entity)
            where T : UserConsultation
            where D : UserConsultationDto
        {
            var consultation = await _sugar.Cons.Id(entity.ConsultationId);
            var branchEntity = await _sugar.HFBs.Id(consultation.HealthFacilityBranchId);
            var medTestEntity = await _sugar.Tests.Id(consultation.TestId);
            var doctorEntity = await _sugar.Docs.Id(entity.DoctorId);
            var userEntity = await _sugar.Usrs.Id(entity.UserId);

            return new UserConsultationDto(
                 userConsultationEntity: entity,
                 consultationEntity: consultation,
                 branchEntity: branchEntity,
                 medTestEntity: medTestEntity,
                 doctorEntity: doctorEntity,
                 userEntity: userEntity
             );
        }

        public async Task<UserHospitalizationDto> Map<T, D>(UserHospitalization entity)
            where T : UserHospitalization
            where D : UserHospitalizationDto
            => new(entity);

        #endregion

        #region Health Facility
        public async Task<HealthFacilityDto> Map<T, D>(HealthFacility entity)
            where T : HealthFacility
            where D : HealthFacilityDto
            => new(entity);

        public async Task<HealthFacilityBranchDto> Map<T, D>(HealthFacilityBranch entity)
            where T : HealthFacilityBranch
            where D : HealthFacilityBranchDto
            => new(entity);

        public async Task<DepartmentDto> Map<T, D>(Department entity)
            where T : Department
            where D : DepartmentDto
            => new(entity);

        public async Task<DoctorDto> Map<T, D>(Doctor entity)
            where T : Doctor
            where D : DoctorDto
            => new(entity);

        public async Task<HospitalizationDto> Map<T, D>(Hospitalization entity)
            where T : Hospitalization
            where D : HospitalizationDto
            => new(entity);

        #endregion

        // for Extended Maps go to ExtendedMaps.cs
    }
}