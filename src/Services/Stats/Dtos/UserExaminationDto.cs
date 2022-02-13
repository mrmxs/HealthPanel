using System;
using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class UserExaminationDto : IDto
    {
        public int Id { get; set; }        
        public int ExaminationId { get; set; } 
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Status { get; set; } 

        public ExaminationDto Examination {get; set;}
        public DoctorDto Doctor {get; set;}
        public UserDto User {get; set;}


        public UserExaminationDto() {}
        
        public UserExaminationDto(
            UserExamination userExaminationEntity, 
            Examination examinationEntity,
            HealthFacilityBranch branchEntity,
            MedTest medTestEntity,
            Doctor doctorEntity,
            User userEntity)
        {            
            this.Id = userExaminationEntity.Id;
            this.ExaminationId = userExaminationEntity.ExaminationId;
            this.UserId = userExaminationEntity.UserId;
            this.DoctorId = userExaminationEntity.DoctorId;
            this.Date = userExaminationEntity.Date;
                // TimeZoneInfo.ConvertTime(
                //     userExaminationEntity.Date, 
                //     TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
            this.Value = userExaminationEntity.Value;
            this.Status = userExaminationEntity.Status.ToString();
        
            this.Examination = new ExaminationDto(
                examinationEntity, branchEntity, medTestEntity);
            this.Doctor = new DoctorDto(doctorEntity);
            this.User = new UserDto(userEntity);
        }
    }
}