# Description:
An Simple app to manage Ships, calculate distance between ships and port, estimate arrive time of ship to port...

# Solution structure:
- `Ship.Api`: API project
- `Ship.Bussiness`: Host all bussiness logic
- `Ship.UnitTest`: Unit test project
- `Ship.Common`: Host all common logic

# Technologies:
- .NET 6.0
- Entity Framework Core 6.0
- Docker
- Swagger
- AutoMapper
- Serilog

- xUnit
- Moq
- FluentAssertions

# Endpoints and Swagger UI:

Root: `https://localhost:5000`
- **Swagger UI:** {Root}/swagger/index.html
- **Ship:** {Root}/api/ship
  - Method: GET
    > Get all ships
  - Method: Post
    > Create new ship
- **Estimated Arrival:** {Root}/api/Ship/estimated_arrival 
  - Method: Post
    > Calculate estimated arrival time of ship to port
    > Note: Please use one of these portid for testing: `117d495d-e3e3-4cd7-a543-814eae32a08e`, `27823f1a-85d0-4260-9e97-3ebba3d9e90b`, `68e7d233-a727-43f9-87fb-5383f6390037`, the test data can be found within `20230522072726_init_db.cs` in folder `Migration` within `Ship.Bussiness`
- **Change Velocity:** {Root}/api/Ship/{id}/velocity
  - Method: Put
    > Change velocity of ship

# How to run:

## Docker:

Navigate to the root folder of the project and run the following command:

```bash
docker-compose up -d

```

# Run from Visual Studio:
- Open Visual Studio
- Open TestShipApi.sln Solution
- Build and Run Solution
> Note: Startup project should be `Ship.Api`

You can also change the Database by modifying `connectionstring` in `appsetting.json`
If you are pointing to a blank DB, please 
- Open `Package Management Console` 
- Change default project to `Ship.Bussiness` 
- And run the bellow command:
```bash
update-database
```