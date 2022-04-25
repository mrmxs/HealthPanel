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

        public ConsultationDto Consultation { get; set; }
        public DoctorDto Doctor { get; set; }
        public UserDto User { get; set; }


        public UserConsultationDto() { }

        public UserConsultationDto(
            UserConsultation userConsultationEntity,
            Consultation consultationEntity,
            HealthFacilityBranch branchEntity,
            MedTest medTestEntity,
            Doctor doctorEntity,
            User userEntity)
        {
            this.Id = userConsultationEntity.Id;
            this.ConsultationId = userConsultationEntity.ConsultationId;
            this.DoctorId = userConsultationEntity.DoctorId;
            this.Date = userConsultationEntity.Date;
            this.UserId = userConsultationEntity.UserId;
            this.Value = userConsultationEntity.Value;
            this.Status = userConsultationEntity.Status;

            this.Consultation = new ConsultationDto(
               consultationEntity, branchEntity, medTestEntity);
            this.Doctor = new DoctorDto(doctorEntity);
            this.User = new UserDto(userEntity);
        }
    }
}