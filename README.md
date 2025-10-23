# TickerQ Demo

A comprehensive demonstration of background job processing using TickerQ in .NET with Clean Architecture principles.

## 🚀 Features

- **Background Job Processing** - Scheduled and one-time job execution
- **Request Management System** - CRUD operations with status tracking
- **Dashboard Interface** - Web-based job monitoring and management
- **Exception Handling** - Custom error handling for failed jobs
- **Entity Framework Integration** - Persistent job storage
- **RESTful API** - Complete API for job scheduling and management

## 🏗️ Architecture

This project follows Clean Architecture with the following structure:

```
TickerQ_Demo/
├── Domain/           # Entities and Enums
├── Application/      # Services, Jobs, and DTOs
├── Infrastructure/   # Data Access and Persistence
└── TickerQ_Demo/     # Web API and Controllers
```

## 🛠️ Prerequisites

- **.NET 8.0** or later
- **SQL Server** (LocalDB, Express, or full version)
- **Visual Studio** or **VS Code** with C# extension

## 📦 Key Components

### Background Jobs
- **AutoReject**: Automatically rejects approved requests after 10 seconds
- **CreateReport**: Generates scheduled reports every 5 minutes
- **ExceptionJob**: Handles job failures with custom logic
- **JobWithData**: Demonstrates data passing to jobs

### API Endpoints
- `GET /api/requests` - List all requests
- `POST /api/requests` - Create new request
- `PUT /api/requests/{id}/approve` - Approve request
- `PUT /api/requests/{id}/complete` - Complete request
- `PUT /api/requests/{id}/reject` - Reject request

### Dashboard
- Access at `/dashboard` (admin/admin)
- Monitor job execution status
- View job history and logs
- Manage scheduled jobs

## 🚀 Quick Start

### 1. Clone and Setup
```bash
git clone <your-repo-url>
cd TickerQ_Demo
```

### 2. Database Setup
```bash
# Update connection string in appsettings.json
# Run migrations
dotnet ef database update --project Infrastructure --startup-project TickerQ_Demo
```

### 3. Run the Application
```bash
dotnet run --project TickerQ_Demo
```

### 4. Access the Application
- **API**: `https://localhost:7000/swagger`
- **Dashboard**: `https://localhost:7000/dashboard`

## 🔧 Configuration

### Connection String
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TickerQ;TrustServerCertificate=true;Integrated Security=SSPI"
  }
}
```

### TickerQ Settings
```json
{
  "TickerQBasicAuth": {
    "username": "admin",
    "password": "admin"
  }
}
```

## 🧪 Testing

The project includes comprehensive examples of:
- Scheduled jobs with cron expressions
- One-time job scheduling
- Exception handling
- Data persistence
- API integration

## 📖 Documentation Site

The documentation site includes:
- **Installation Guide** - Step-by-step setup
- **API Reference** - Complete API documentation
- **Configuration** - Detailed configuration options
- **Dashboard Guide** - Using the web interface
- **Exception Handling** - Error management strategies
- **Cron Jobs** - Scheduling patterns
- **Time Jobs** - One-time job scheduling

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 🔗 Related Links

- [TickerQ Documentation](https://tickerq.net/)
- [HemaDocs](https://ebrahimdawoud.github.io/TickerQUserManual/)
---

**Built with ❤️ using TickerQ, .NET, and Docusaurus By AboKhalil**
