using System.Threading.Tasks;
using HealthPanel.Core.Entities;
using HealthPanel.Infrastructure.Data;

namespace HealthPanel.Services.Stats.DAL
{
    interface ISugarLogic<T>
    {
        Task<T> Id(int id);
    }

    public abstract class AbstractSugarLogic<T> : ISugarLogic<T>
    {
        protected readonly HealthPanelDbContext _context;

        public AbstractSugarLogic(HealthPanelDbContext context)
        {
            _context = context;
        }

        public abstract Task<T> Id(int id);
    }

    #region Basic Types

    public class TestsSugarLogic : AbstractSugarLogic<MedTest>
    {
        public TestsSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<MedTest> Id(int id) => await _context.Tests.FindAsync(id);
    }

    public class LabTestSugarLogic : AbstractSugarLogic<LabTest>
    {
        public LabTestSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<LabTest> Id(int id) => await _context.LabTests.FindAsync(id);
    }

    public class ExaminationSugarLogic : AbstractSugarLogic<Examination>
    {
        public ExaminationSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<Examination> Id(int id) => await _context.Examinations.FindAsync(id);
    }

    public class ConsultationSugarLogic : AbstractSugarLogic<Consultation>
    {
        public ConsultationSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<Consultation> Id(int id) => await _context.Consultations.FindAsync(id);
    }

    public class TestPanelSugarLogic : AbstractSugarLogic<TestPanel>
    {
        public TestPanelSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<TestPanel> Id(int id) => await _context.TestPanels.FindAsync(id);
    }

    public class LabTestPanelSugarLogic : AbstractSugarLogic<LabTestPanel>
    {
        public LabTestPanelSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<LabTestPanel> Id(int id) => await _context.LabTestPanels.FindAsync(id);
    }

    public class TestListSugarLogic : AbstractSugarLogic<TestList>
    {
        public TestListSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<TestList> Id(int id) => await _context.TestLists.FindAsync(id);
    }

    public class TestToTestListSugarLogic : AbstractSugarLogic<TestToTestList>
    {
        public TestToTestListSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<TestToTestList> Id(int id)
            => await _context.TestsToTestList.FindAsync(id);
    }

    #endregion

    #region User Types

    public class UserSugarLogic : AbstractSugarLogic<User>
    {
        public UserSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<User> Id(int id) => await _context.Users.FindAsync(id);
    }

    public class UserLabTestSugarLogic : AbstractSugarLogic<UserLabTest>
    {
        public UserLabTestSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<UserLabTest> Id(int id) => await _context.UserLabTests.FindAsync(id);
    }

    public class UserExaminationSugarLogic : AbstractSugarLogic<UserExamination>
    {
        public UserExaminationSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<UserExamination> Id(int id) => await _context.UserExaminations.FindAsync(id);
    }

    public class UserConsultationSugarLogic : AbstractSugarLogic<UserConsultation>
    {
        public UserConsultationSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<UserConsultation> Id(int id) => await _context.UserConsultations.FindAsync(id);
    }

    public class UserHospitalizationSugarLogic : AbstractSugarLogic<UserHospitalization>
    {
        public UserHospitalizationSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<UserHospitalization> Id(int id) => await _context.UserHospitalizations.FindAsync(id);
    }

    #endregion

    #region Hospital Treatment

    public class HealthFacilitySugarLogic : AbstractSugarLogic<HealthFacility>
    {
        public HealthFacilitySugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<HealthFacility> Id(int id) => await _context.HealthFacilities.FindAsync(id);
    }

    public class HealthFacilityBranchSugarLogic : AbstractSugarLogic<HealthFacilityBranch>
    {
        public HealthFacilityBranchSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<HealthFacilityBranch> Id(int id) => await _context.HealthFacilityBranches.FindAsync(id);
    }

    public class DoctorSugarLogic : AbstractSugarLogic<Doctor>
    {
        public DoctorSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<Doctor> Id(int id) => await _context.Doctors.FindAsync(id);
    }

    public class HospitalizationSugarLogic : AbstractSugarLogic<Hospitalization>
    {
        public HospitalizationSugarLogic(HealthPanelDbContext context) : base(context) { }
        public override async Task<Hospitalization> Id(int id) => await _context.Hospitalizations.FindAsync(id);
    }

    #endregion
}