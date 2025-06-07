using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicApp.DataAccess;
using Clinic.Entities;

namespace ClinicApp.Components
{
    public class UpcomingInPersonAppointments : ViewComponent
    {
        private readonly ClinicScheduleDbContext _context;

        public UpcomingInPersonAppointments(ClinicScheduleDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int count = 3)
        {
            var upcomingAppointments = _context.Appointments
                .Include(a => a.Schedule)
                .Where(a => a.AppointmentDate >= DateTime.Now
                            && a.AppointmentType == AppointmentTypeOptions.InPerson)
                .OrderBy(a => a.AppointmentDate)
                .Take(count)
                .ToList();

            return View(upcomingAppointments);
        }
    }
}
