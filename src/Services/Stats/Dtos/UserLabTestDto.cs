using System;
using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class UserLabTestDto : IDto
    {
        public int Id { get; set; } 
        public int LabTestId { get; set; } 
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
        public string Status { get; set; } 
        
        public LabTestDto Test {get; set;}
        public UserDto User {get; set;}


        public UserLabTestDto() {}
        
        public UserLabTestDto(
            UserLabTest userTestEntity, 
            LabTest labTestEntity,
            HealthFacilityBranch branchEntity,
            MedTest medTestEntity,
            User userEntity)
        {            
            this.Id = userTestEntity.Id;
            this.LabTestId = userTestEntity.LabTestId;
            this.UserId = userTestEntity.UserId;
            this.Date = userTestEntity.Date;
                // TimeZoneInfo.ConvertTime(
                //     userTestEntity.Date, 
                //     TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time"));
            this.Value = userTestEntity.Value;
            this.Status = userTestEntity.Status.ToString();
        
            this.Test = new LabTestDto(labTestEntity, branchEntity, medTestEntity);
            this.User = new UserDto(userEntity);
        }
    }
}