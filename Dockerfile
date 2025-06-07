# Use the official .NET 8 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and build
COPY . .
RUN dotnet publish ClinicApp/ClinicApp.csproj -c Release -o out

# Use the .NET 8 ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "ClinicApp.dll"]
