using System.ComponentModel.DataAnnotations;
namespace Clinic.Entities
{
    public class Clinician
    {
        public int ClinicianId { get; set; }


        [Required(ErrorMessage = "Professional Registration Number is required.")]
        [RegularExpression(@"^\d{2}-\d{4}-[a-zA-Z]{2}$", ErrorMessage = "Format must be: 12-3456-AB")]
        public string? ProfessionalRegistrationNumber { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string? LastName { get; set; }

        public string? FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }


        // FK:
        public int? ScheduleId { get; set; }

        // And nav prop:
        public Schedule? Schedule { get; set; }
    }
}
