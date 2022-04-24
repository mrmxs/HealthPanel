using System;
using HealthPanel.Core.Entities;

namespace HealthPanel.Services.Stats.Dtos
{
    public class UserHospitalizationDto : IDto
    {
        public int Id { get; set; }
        public int HospitalizationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DoctorId { get; set; }
        public string DischargeSummary { get; set; }
        public string Notes { get; set; }

        public UserHospitalizationDto() { }

        public UserHospitalizationDto(UserHospitalization entity)
        {
            this.Id = entity.Id;
            this.HospitalizationId = entity.HospitalizationId;
            this.StartDate = entity.StartDate;
            this.EndDate = entity.EndDate;
            this.DoctorId = entity.DoctorId;
            this.DischargeSummary = entity.DischargeSummary;
            this.Notes = entity.Notes;
        }
    }
}