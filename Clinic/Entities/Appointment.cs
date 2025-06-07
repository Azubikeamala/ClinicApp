using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.Entities
{
    public enum AppointmentTypeOptions { InPerson, Phone, Video }

    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Please select an appointment date.")]
        [DataType(DataType.Date)]
        public DateTime? AppointmentDate { get; set; }

        [Required(ErrorMessage = "Patient name is required.")]
        [StringLength(100)]
        public string? PatientName { get; set; }

        [Display(Name = "Appointment Type")]
        public AppointmentTypeOptions AppointmentType { get; set; } = AppointmentTypeOptions.InPerson;

        // FK:
        [Required]
        public int ScheduleId { get; set; }

        // Nav:
        public Schedule? Schedule { get; set; }
    }
}
