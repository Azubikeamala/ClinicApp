using Microsoft.AspNetCore.Mvc;
using ClinicApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using ClinicApp.Components;

namespace ClinicApp.Components
{
    public class RecentPastAppointments : ViewComponent
    {
        private readonly ClinicScheduleDbContext _clinicScheduleDbContext;

        public RecentPastAppointments(ClinicScheduleDbContext clinicScheduleDbContext)
        {
            _clinicScheduleDbContext = clinicScheduleDbContext;
        }

        public IViewComponentResult Invoke(int numberOfAppointmentsToDisplay)
        {
            DateTime now = DateTime.Now;
            var appointments = _clinicScheduleDbContext.Appointments
                .Include(a => a.Schedule)
                .Where(a => a.AppointmentDate < now)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            var vm = new AppointmentsViewModel
            {
                Appointments = appointments,
                NumberOfAppointmentsToDisplay = numberOfAppointmentsToDisplay
            };

            return View(vm);
        }
    }
}
