using HealthPanel.Infrastructure.Data;

namespace HealthPanel.Services.Stats.DAL
{
    /// <summary>
    /// Interface for SugarContext
    /// </summary>
    public interface ISugarContext
    {
        TestsSugarLogic Tests { get; }
        LabTestSugarLogic LTests { get; }
        ExaminationSugarLogic Exams { get; }
        TestPanelSugarLogic TPs { get; }
        LabTestPanelSugarLogic LTPs { get; }
        TestListSugarLogic TLs { get; }
        TestToTestListSugarLogic TstTL { get; }

        UserLabTestSugarLogic ULTests { get; }
        UserExaminationSugarLogic UExams { get; }
        HealthFacilitySugarLogic HFs { get; }
        HealthFacilityBranchSugarLogic HFBs { get; }
        DoctorSugarLogic Docs { get; }
        UserSugarLogic Usrs { get; }
    }

    /// <summary>
    /// Syntactic sugar cover for the standart DBContext
    /// </summary>
    public class SugarContext : ISugarContext
    {
        public TestsSugarLogic Tests { get; }
        public LabTestSugarLogic LTests { get; }
        public ExaminationSugarLogic Exams { get; }
        public TestPanelSugarLogic TPs { get; }
        public LabTestPanelSugarLogic LTPs { get; }
        public TestListSugarLogic TLs { get; }
        public TestToTestListSugarLogic TstTL { get; }

        public UserLabTestSugarLogic ULTests { get; }
        public UserExaminationSugarLogic UExams { get; }
        public HealthFacilitySugarLogic HFs { get; }
        public HealthFacilityBranchSugarLogic HFBs { get; }
        public DoctorSugarLogic Docs { get; }
        public UserSugarLogic Usrs { get; }

        public SugarContext(HealthPanelDbContext context)
        {
            Tests = new TestsSugarLogic(context);
            LTests = new LabTestSugarLogic(context);
            Exams = new ExaminationSugarLogic(context);
            TPs = new TestPanelSugarLogic(context);
            LTPs = new LabTestPanelSugarLogic(context);
            TLs = new TestListSugarLogic(context);
            TstTL = new TestToTestListSugarLogic(context);

            Usrs = new UserSugarLogic(context);
            ULTests = new UserLabTestSugarLogic(context);
            UExams = new UserExaminationSugarLogic(context);

            HFs = new HealthFacilitySugarLogic(context);
            HFBs = new HealthFacilityBranchSugarLogic(context);
            Docs = new DoctorSugarLogic(context);
        }
    }

}