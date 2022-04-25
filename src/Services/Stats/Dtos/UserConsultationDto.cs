using System;
using HealthPanel.Core.Entities;
using HealthPanel.Core.Enums;

namespace HealthPanel.Services.Stats.Dtos
{
    public class UserConsultationDto : IDto
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Value { get; set; }
        public TestStatus Status { get; set; }

        public UserConsultationDto() { }

        public UserConsultationDto(UserConsultation entity)
        {
            this.Id = entity.Id;
            this.ConsultationId = entity.ConsultationId;
            this.DoctorId = entity.DoctorId;
            this.Date = entity.Date;
            this.UserId = entity.UserId;
            this.Value = entity.Value;
            this.Status = entity.Status;
        }
    }
}