# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the entire solution
COPY . .

# Restore dependencies
RUN dotnet restore AiTutor.API/AiTutor.API.csproj

# Build and publish the API
WORKDIR /src/AiTutor.API
RUN dotnet publish AiTutor.API.csproj -c Release -o /app/publish

# Generate the final image with only the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory in the container
WORKDIR /app

# Expose ports
EXPOSE 5000

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Explicitly set the listening port for ASP.NET Core application
ENV ASPNETCORE_URLS=http://+:5000

# Set the entrypoint to run the application
ENTRYPOINT ["dotnet", "AiTutor.API.dll"]
