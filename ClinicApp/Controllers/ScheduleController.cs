using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicApp.DataAccess;
using Clinic.Entities;
using ClinicApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClinicApp.Controllers
{
    [Authorize] 
    public class ScheduleController : Controller
    {
        private readonly ClinicScheduleDbContext _clinicScheduleDbContext;

        public ScheduleController(ClinicScheduleDbContext clinicScheduleDbContext)
        {
            _clinicScheduleDbContext = clinicScheduleDbContext;
        }

        [HttpGet("/schedules")]
        public IActionResult GetAllSchedules()
        {
            var schedules = _clinicScheduleDbContext.Schedules
                .Include(s => s.Clinicians)
                .Include(s => s.Appointments)
                .OrderByDescending(s => s.DateCreated)
                .ToList();

            return View("Items", schedules);
        }

        [HttpGet("/schedules/{id}")]
        public IActionResult GetScheduleById(int id)
        {
            var schedule = _clinicScheduleDbContext.Schedules
                .Include(s => s.Clinicians)
                .Include(s => s.Appointments)
                .FirstOrDefault(s => s.ScheduleId == id);

            if (schedule == null)
                return NotFound();

            var vm = new ScheduleDetailsViewModel
            {
                Schedule = schedule,
                NewClinician = new Clinician(),
                NewAppointment = new Appointment(),
                Clinicians = schedule.Clinicians?.ToList() ?? new List<Clinician>(),
                Appointments = schedule.Appointments?.ToList() ?? new List<Appointment>()
            };

            return View("Details", vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddClinician(int scheduleId, Clinician newClinician)
        {
            if (!ModelState.IsValid)
            {
                var schedule = _clinicScheduleDbContext.Schedules
                    .Include(s => s.Clinicians)
                    .Include(s => s.Appointments)
                    .FirstOrDefault(s => s.ScheduleId == scheduleId);

                var vm = new ScheduleDetailsViewModel
                {
                    Schedule = schedule ?? new Schedule(),
                    NewClinician = newClinician,
                    NewAppointment = new Appointment(),
                    Clinicians = schedule?.Clinicians?.ToList() ?? new List<Clinician>(),
                    Appointments = schedule?.Appointments?.ToList() ?? new List<Appointment>()
                };

                return View("Details", vm);
            }

            newClinician.ScheduleId = scheduleId;
            _clinicScheduleDbContext.Clinicians.Add(newClinician);
            _clinicScheduleDbContext.SaveChanges();

            TempData["LastActionMessage"] = "Clinician successfully added.";
            return RedirectToAction("GetScheduleById", new { id = scheduleId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAppointment(int scheduleId, Appointment newAppointment)
        {
            if (!ModelState.IsValid)
            {
                var schedule = _clinicScheduleDbContext.Schedules
                    .Include(s => s.Clinicians)
                    .Include(s => s.Appointments)
                    .FirstOrDefault(s => s.ScheduleId == scheduleId);

                var vm = new ScheduleDetailsViewModel
                {
                    Schedule = schedule ?? new Schedule(),
                    NewAppointment = newAppointment,
                    NewClinician = new Clinician(),
                    Clinicians = schedule?.Clinicians?.ToList() ?? new List<Clinician>(),
                    Appointments = schedule?.Appointments?.ToList() ?? new List<Appointment>()
                };

                return View("Details", vm);
            }

            newAppointment.ScheduleId = scheduleId;
            _clinicScheduleDbContext.Appointments.Add(newAppointment);
            _clinicScheduleDbContext.SaveChanges();

            TempData["LastActionMessage"] = "Appointment successfully added.";
            return RedirectToAction("GetScheduleById", new { id = scheduleId });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/schedules/add-request")]
        public IActionResult GetAddScheduleRequest()
        {
            return View("AddSchedule", new Schedule());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/schedules")]
        public IActionResult AddNewSchedule(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _clinicScheduleDbContext.Schedules.Add(schedule);
                _clinicScheduleDbContext.SaveChanges();

                TempData["LastActionMessage"] = $"The schedule \"{schedule.Name}\" was added.";
                return RedirectToAction("GetAllSchedules", "Schedule");
            }

            return View("AddSchedule", schedule);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/schedules/{id}/edit-request")]
        public IActionResult GetEditRequestById(int id)
        {
            var schedule = _clinicScheduleDbContext.Schedules.Find(id);
            return View("EditSchedule", schedule);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/schedules/edit-requests")]
        public IActionResult ProcessEditRequest(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _clinicScheduleDbContext.Schedules.Update(schedule);
                _clinicScheduleDbContext.SaveChanges();

                TempData["LastActionMessage"] = $"The schedule \"{schedule.Name}\" was updated.";
                return RedirectToAction("GetAllSchedules", "Schedule");
            }

            return View("EditSchedule", schedule);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/schedules/{id}/delete-request")]
        public IActionResult GetDeleteRequestById(int id)
        {
            var schedule = _clinicScheduleDbContext.Schedules.Find(id);
            return View("DeleteConfirmation", schedule);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ProcessDeleteRequestById(int id)
        {
            var schedule = _clinicScheduleDbContext.Schedules
                            .Include(s => s.Appointments)
                            .Include(s => s.Clinicians)
                            .FirstOrDefault(s => s.ScheduleId == id);

            if (schedule == null) return NotFound();

            _clinicScheduleDbContext.Appointments.RemoveRange(schedule.Appointments);
            _clinicScheduleDbContext.Clinicians.RemoveRange(schedule.Clinicians);
            _clinicScheduleDbContext.Schedules.Remove(schedule);

            _clinicScheduleDbContext.SaveChanges();

            TempData["LastActionMessage"] = $"The schedule \"{schedule.Name}\" was deleted.";
            return RedirectToAction("GetAllSchedules");
        }



    }
}
