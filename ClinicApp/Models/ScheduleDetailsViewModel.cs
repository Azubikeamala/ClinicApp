using Clinic.Entities;
using ClinicApp.Entities; 

namespace ClinicApp.Models
{
    public class ScheduleDetailsViewModel
    {
        public ScheduleDetailsViewModel()
        {
            Schedule = new Schedule();
            NewClinician = new Clinician();
            NewAppointment = new Appointment();
            Clinicians = new List<Clinician>();
            Appointments = new List<Appointment>();
        }

        public Schedule Schedule { get; set; }
        public Clinician NewClinician { get; set; }
        public Appointment NewAppointment { get; set; }
        public List<Clinician> Clinicians { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
