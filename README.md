# Todo API

A C# ASP.NET Core Web API for managing todo items, following clean architecture principles. The project supports CRUD operations, uses MongoDB for persistence, and is containerized with Docker for easy development and deployment.

## Features
- Create, read, update, and delete todo items
- Clean separation of Application, Domain, and Infrastructure layers
- MongoDB as the data store
- Docker support for local development
- OpenAPI (Swagger) documentation

## Getting Started

### Prerequisites
- [.NET 10.0+](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)

### Running with Docker Compose
1. Start MongoDB:
   ```sh
   docker-compose up -d
   ```
2. Run the API:
   ```sh
   dotnet run
   ```

The API will be available at `https://localhost:5001` (or the port specified in your launch settings).

### Configuration
MongoDB connection string is set in `appsettings.json`:
```json
"MongoDb": {
  "ConnectionString": "mongodb://localhost:27017"
}
```

## Project Structure
- `Api/Controllers/` - API endpoints
- `Application/` - Application logic, contracts, services, mapping
- `Domain/` - Core business entities
- `Infrastructure/` - Data access implementations

## License

This project is licensed under the [Apache License 2.0](LICENSE).
