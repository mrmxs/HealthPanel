using System;
using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class TestToTestListDto : IDto
    {
        public int Id { get; set; }
        public int TestListId { get; set; }
        public int MedTestId { get; set; }
        public int LabTestId { get; set; }
        public int ExaminationId { get; set; }
        public int TestPanelId { get; set; }
        public int LabTestPanelId { get; set; }
        public TimeSpan TTL { get; set; }
        public int Index { get; set; }

        public TestToTestListDto() { }

        public TestToTestListDto(TestToTestList entity)
        {
            this.Id = entity.Id;
            this.TestListId = entity.TestListId;
            this.MedTestId = entity.MedTestId;
            this.LabTestId = entity.LabTestId;
            this.ExaminationId = entity.ExaminationId;
            this.TestPanelId = entity.TestPanelId;
            this.LabTestPanelId = entity.LabTestPanelId;
            this.TTL = entity.TTL;
            this.Index = entity.Index;
        }
    }
}