# Taxi24App

Taxi24App is a web application for managing taxi services. This document provides instructions on how to set up and run the application.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Applying Migrations](#applying-migrations)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

Before you begin, ensure you have met the following requirements:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or use a local or cloud-based SQL Server instance)

## Installation

1. **Clone the repository**:

    ```bash
    git clone https://github.com/Brajerrie/Taxi24App.git
    cd Taxi24App
    ```

2. **Set up the database connection string**:

    Update the `appsettings.json` file in the `Taxi24App` project with your SQL Server connection string:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=Taxi24Db;User Id=your_username;Password=your_password;"
      },
      "Serilog": {
        "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.File" ],
        "MinimumLevel": {
          "Default": "Information",
          "Override": {
            "Microsoft": "Warning",
            "System": "Warning"
          }
        },
        "WriteTo": [
          { "Name": "Console" },
          {
            "Name": "File",
            "Args": {
              "path": "Logs/log-.txt",
              "rollingInterval": "Day",
              "restrictedToMinimumLevel": "Information"
            }
          }
        ],
        "Enrich": [ "FromLogContext" ]
      },
      "AllowedHosts": "*"
    }
    ```

3. **Install the required packages**:

    ```bash
    dotnet restore
    ```

## Running the Application

1. **Build the project**:

    ```bash
    dotnet build
    ```

2. **Run the application**:

    ```bash
    dotnet run
    ```

    By default, the application will run on `http://localhost:5015` and `https://localhost:7265`.

## Applying Migrations

1. **Add the necessary Entity Framework Core tools**:

    Ensure that you have the EF Core CLI tools installed:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

2. **Add a migration**:

    ```bash
    dotnet ef migrations add InitialCreate
    ```

3. **Update the database**:

    ```bash
    dotnet ef database update
    ```

## Usage

Once the application is running, you can use a tool like Postman or your browser to interact with the API.

## API Endpoints

Here are some of the main API endpoints you can use:

- **Riders**
  - `GET /api/v1/riders` - Get all riders
  - `POST /api/v1/riders` - Create a new rider
  - `GET /api/v1/riders/{id}` - Get a rider by ID
  - `PUT /api/v1/riders/{id}` - Update a rider by ID
  - `DELETE /api/v1/riders/{id}` - Delete a rider by ID
  - `POST /api/v1/riders/search` - Search for riders

- **Drivers**
  - `GET /api/v1/drivers` - Get all drivers
  - `POST /api/v1/drivers` - Create a new driver
  - `GET /api/v1/drivers/{id}` - Get a driver by ID
  - `PUT /api/v1/drivers/{id}` - Update a driver by ID
  - `DELETE /api/v1/drivers/{id}` - Delete a driver by ID
  - `POST /api/v1/drivers/search` - Search for drivers

- **Trips**
  - `GET /api/v1/trips` - Get all trips
  - `POST /api/v1/trips` - Create a new trip
  - `GET /api/v1/trips/{id}` - Get a trip by ID
  - `PUT /api/v1/trips/{id}` - Update a trip by ID
  - `DELETE /api/v1/trips/{id}` - Delete a trip by ID
  - `POST /api/v1/trips/search` - Search for trips

## Contributing

To contribute to this project, follow these steps:

1. Fork the repository.
2. Create a branch: `git checkout -b feature-branch`.
3. Make your changes and commit them: `git commit -m 'Add new feature'`.
4. Push to the original branch: `git push origin feature-branch`.
5. Create a pull request.

Alternatively, see the GitHub documentation on [creating a pull request](https://help.github.com/articles/creating-a-pull-request).

## License

none
