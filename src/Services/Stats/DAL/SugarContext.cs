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
        ConsultationSugarLogic Cons { get; }
        TestPanelSugarLogic TPs { get; }
        LabTestPanelSugarLogic LTPs { get; }
        TestListSugarLogic TLs { get; }
        TestToTestListSugarLogic TstTL { get; }

        UserSugarLogic Usrs { get; }
        UserLabTestSugarLogic ULTests { get; }
        UserExaminationSugarLogic UExams { get; }
        UserConsultationSugarLogic UCons { get; }
        UserHospitalizationSugarLogic UHspzn { get; }


        HealthFacilitySugarLogic HFs { get; }
        HealthFacilityBranchSugarLogic HFBs { get; }
        DoctorSugarLogic Docs { get; }
        HospitalizationSugarLogic Hspzn { get; }
    }

    /// <summary>
    /// Syntactic sugar cover for the standart DBContext
    /// </summary>
    public class SugarContext : ISugarContext
    {
        public TestsSugarLogic Tests { get; }
        public LabTestSugarLogic LTests { get; }
        public ExaminationSugarLogic Exams { get; }
        public ConsultationSugarLogic Cons { get; }

        public TestPanelSugarLogic TPs { get; }
        public LabTestPanelSugarLogic LTPs { get; }
        public TestListSugarLogic TLs { get; }
        public TestToTestListSugarLogic TstTL { get; }

        public UserSugarLogic Usrs { get; }
        public UserLabTestSugarLogic ULTests { get; }
        public UserExaminationSugarLogic UExams { get; }
        public UserConsultationSugarLogic UCons { get; }
        public UserHospitalizationSugarLogic UHspzn { get; }

        public HealthFacilitySugarLogic HFs { get; }
        public HealthFacilityBranchSugarLogic HFBs { get; }
        public DoctorSugarLogic Docs { get; }
        public HospitalizationSugarLogic Hspzn { get; }

        public SugarContext(HealthPanelDbContext context)
        {
            Tests = new TestsSugarLogic(context);
            LTests = new LabTestSugarLogic(context);
            Exams = new ExaminationSugarLogic(context);
            Cons = new ConsultationSugarLogic(context);
            TPs = new TestPanelSugarLogic(context);
            LTPs = new LabTestPanelSugarLogic(context);
            TLs = new TestListSugarLogic(context);
            TstTL = new TestToTestListSugarLogic(context);

            Usrs = new UserSugarLogic(context);
            ULTests = new UserLabTestSugarLogic(context);
            UExams = new UserExaminationSugarLogic(context);
            UCons = new UserConsultationSugarLogic(context);
            UHspzn = new UserHospitalizationSugarLogic(context);

            HFs = new HealthFacilitySugarLogic(context);
            HFBs = new HealthFacilityBranchSugarLogic(context);
            Docs = new DoctorSugarLogic(context);
            Hspzn = new HospitalizationSugarLogic(context);
        }
    }

}