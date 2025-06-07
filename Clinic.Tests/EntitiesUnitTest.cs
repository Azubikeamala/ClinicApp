using Clinic.Entities;

namespace Clinic.Tests
{
    public class EntitiesUnitTest
    {
        [Fact]
        public void TestNumberOfPastAppointments()
        {
            // Arrange
            Schedule sch1 = new Schedule()
            {
                Name = "Test schedule #1",
                Appointments = new List<Appointment>()
            };

            sch1.Appointments.Add(new Appointment() { AppointmentDate = new DateTime(2020, 8, 5), PatientName = "Homer Simpson" });
            sch1.Appointments.Add(new Appointment() { AppointmentDate = new DateTime(2021, 11, 24), PatientName = "Marge Simpson" });
            sch1.Appointments.Add(new Appointment() { AppointmentDate = new DateTime(2023, 2, 9), PatientName = "Bart Simpson" });
            sch1.Appointments.Add(new Appointment() { AppointmentDate = new DateTime(2023, 5, 8), PatientName = "Lisa Simpson" });
            sch1.Appointments.Add(new Appointment() { AppointmentDate = new DateTime(2023, 5, 31), PatientName = "Maggie Simpson" });
            sch1.Appointments.Add(new Appointment() { AppointmentDate = DateTime.Today.AddDays(1), PatientName = "Future Patient" });

            // Act
            var pastAppointments = sch1.Appointments
                .Where(a => a.AppointmentDate < DateTime.Today)
                .ToList();

            // Assert
            Assert.Equal(5, pastAppointments.Count); 
        }
    }
}
