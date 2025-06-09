#  ClinicApp

ClinicApp is a fully functional ASP.NET Core MVC web application designed to manage clinicians, schedules, and appointments. It includes full CRUD functionality, role-based access control, form validation, and a responsive user interface.

##  Features

- ASP.NET Core MVC architecture with a clean, layered structure
- Entity Framework Core integration using Code-First migration
- User authentication and role-based authorization using ASP.NET Identity
- Clinician registration with input validation and formatting
- Schedule and appointment creation and management
- View components to display upcoming and completed appointments
- Tag Helpers and Partial Views for clean, reusable UI code
- Responsive UI with jQuery UI for interactive date picking
- Unit testing with xUnit to ensure functionality and reliability

##  Technologies Used

- ASP.NET Core MVC  
- Entity Framework Core  
- SQL Server LocalDB  
- ASP.NET Identity  
- jQuery and jQuery UI  
- xUnit for unit testing  

##  How to Run Locally

To get this project running on your local machine:

1. Clone the repository  
   git clone https://github.com/Azubikeamala/ClinicApp.git  
   cd ClinicApp

2. Open the solution in Visual Studio

3. Restore NuGet packages  
   dotnet restore

4. Update the connection string in appsettings.json if needed

5. Apply database migrations  
   dotnet ef database update

6. Run the app  
   dotnet run

7. Visit the application in your browser using the URL shown in the terminal, typically https://localhost:5001

##  Future Enhancements

- Admin dashboard for managing users and roles  
- Appointment reminders via email  
- Export schedules to PDF  
- Mobile-friendly views  
- Integration with external calendar APIs (Google, Outlook)

##  License

This project is licensed under the MIT License.

Thanks for checking it out!
