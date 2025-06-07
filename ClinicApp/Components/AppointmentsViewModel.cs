using Clinic.Entities;

namespace ClinicApp.Components
{
    public class AppointmentsViewModel
    {
        public List<Appointment>? Appointments { get; set; }

        public int NumberOfAppointmentsToDisplay { get; set; }
    }
}
