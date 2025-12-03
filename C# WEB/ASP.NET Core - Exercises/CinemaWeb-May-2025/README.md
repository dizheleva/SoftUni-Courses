# 🎬 CinemaWeb Application

A comprehensive cinema management web application built with **ASP.NET Core 8.0** and **Entity Framework Core**. This project demonstrates advanced MVC patterns, repository pattern, service layer architecture, and role-based authentication.

---

## 📋 Table of Contents

- [About](#about)
- [Features](#features)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Database Schema](#database-schema)
- [Contributing](#contributing)
- [License](#license)

---

## 🎯 About

CinemaWeb is a full-featured cinema management system developed as part of the **SoftUni C# Web Developer** course. The application allows users to browse cinemas and movies, manage watchlists, and provides role-based access for managers to control movie and cinema data.

The application follows **Clean Architecture** principles with separation of concerns across multiple layers:
- **Web Layer** - MVC Controllers and Views
- **Service Layer** - Business logic
- **Data Layer** - Repository pattern and Entity Framework
- **Infrastructure Layer** - Extensions and Middleware

---

## ✨ Features

### Public Features
- 🏢 **Browse Cinemas** - View all available cinemas with their locations
- 🎬 **Browse Movies** - Explore the complete movie catalog
- 🔍 **Movie Details** - View detailed information about each movie
- 📋 **Cinema Programs** - See movies showing at specific cinemas

### User Features (Authenticated)
- ⭐ **Watchlist Management** - Add/remove movies to your personal watchlist
- 👤 **User Account** - Register and login functionality
- 🎫 **Ticket Management** - Purchase tickets for movies (coming soon)

### Manager Features
- ➕ **Movie Management** - Create, edit, and delete movies
- 🏗️ **Cinema Management** - Manage cinema information and programs
- 🔐 **Role-Based Access** - Secure manager-only access via middleware

---

## 🏗️ Architecture

The application follows a **layered architecture** with clear separation of concerns:

```
┌─────────────────────────────────────┐
│         Web Layer (MVC)             │
│  Controllers, Views, ViewModels     │
├─────────────────────────────────────┤
│      Infrastructure Layer           │
│   Extensions, Middleware, Helpers   │
├─────────────────────────────────────┤
│        Service Layer                │
│    Business Logic & Interfaces      │
├─────────────────────────────────────┤
│         Data Layer                  │
│  Repositories, DbContext, Models    │
└─────────────────────────────────────┘
```

### Design Patterns Used
- **Repository Pattern** - Abstraction of data access
- **Service Layer Pattern** - Business logic encapsulation
- **Dependency Injection** - Loose coupling
- **Repository-Service Pattern** - Separation of data access and business logic

---

## 📁 Project Structure

```
CinemaWeb-May-2025/
│
├── CinemaApp/                          # Main Web Application
│   ├── Controllers/                    # MVC Controllers
│   │   ├── BaseController.cs          # Base controller with common functionality
│   │   ├── HomeController.cs          # Home page controller
│   │   ├── MovieController.cs         # Movie management
│   │   ├── CinemaController.cs        # Cinema browsing
│   │   ├── WatchlistController.cs     # User watchlist
│   │   └── ManagerController.cs       # Manager dashboard
│   │
│   ├── Views/                         # Razor Views
│   │   ├── Home/                      # Home page views
│   │   ├── Movie/                     # Movie views (Index, Details, Create, Edit)
│   │   ├── Cinema/                    # Cinema views
│   │   ├── Watchlist/                 # Watchlist views
│   │   └── Shared/                    # Layout and partial views
│   │
│   ├── Areas/                         # Identity area for authentication
│   │   └── Identity/
│   │
│   ├── wwwroot/                       # Static files
│   │   ├── css/                       # Stylesheets
│   │   ├── js/                        # JavaScript files
│   │   ├── lib/                       # Third-party libraries (Bootstrap, jQuery)
│   │   └── images/                    # Images
│   │
│   ├── Program.cs                     # Application entry point
│   └── appsettings.json               # Configuration
│
├── CinemaApp.Data/                     # Data Access Layer
│   ├── ApplicationDbContext.cs        # DbContext
│   ├── Repository/                    # Repository implementations
│   │   ├── Interfaces/                # Repository interfaces
│   │   ├── BaseRepository.cs          # Generic repository base
│   │   ├── MovieRepository.cs
│   │   ├── CinemaRepository.cs
│   │   └── WatchlistRepository.cs
│   │
│   ├── Configuration/                 # EF Core configurations
│   │   ├── MovieConfiguration.cs
│   │   ├── CinemaConfiguration.cs
│   │   └── ...
│   │
│   ├── Migrations/                    # Database migrations
│   └── Seeding/                       # Data seeding
│       └── RoleSeeder.cs              # Roles and initial user seeding
│
├── CinemaApp.Data.Models/              # Entity Models
│   ├── Movie.cs                       # Movie entity
│   ├── Cinema.cs                      # Cinema entity
│   ├── CinemaMovie.cs                 # Junction entity
│   ├── UserMovie.cs                   # User watchlist entity
│   ├── Ticket.cs                      # Ticket entity
│   └── Manager.cs                     # Manager entity
│
├── CinemaApp.Data.Common/              # Common Data Constants
│   └── EntityConstants.cs             # Entity validation constants
│
├── CinemaApp.Services.Core/            # Business Logic Layer
│   ├── Interfaces/                    # Service interfaces
│   │   ├── IMovieService.cs
│   │   ├── ICinemaService.cs
│   │   └── IWatchlistService.cs
│   │
│   └── Services/                      # Service implementations
│       ├── MovieService.cs
│       ├── CinemaService.cs
│       └── WatchlistService.cs
│
├── CinemaApp.Web.ViewModels/           # View Models (DTOs)
│   ├── Movie/                         # Movie view models
│   ├── Cinema/                        # Cinema view models
│   └── Watchlist/                     # Watchlist view models
│
├── CinemaApp.Web.Infrastructure/       # Infrastructure Layer
│   ├── Extensions/                    # Extension methods
│   │   ├── ServiceCollectionExtensions.cs
│   │   └── WebApplicationExtensions.cs
│   │
│   └── Middlewares/                   # Custom middlewares
│       └── ManagerAccessMiddleware.cs # Manager role middleware
│
├── CinemaApp.GCommon/                  # Global Constants
│   ├── ApplicationConstants.cs        # Application-wide constants
│   └── ExceptionMessages.cs           # Exception messages
│
└── CinemaApp.*.Tests/                  # Test Projects
    ├── CinemaApp.Services.Tests/
    ├── CinemaApp.Web.Tests/
    └── CinemaApp.IntegrationTests/
```

---

## 🛠️ Technologies Used

### Backend
- **.NET 8.0** - Latest .NET framework
- **ASP.NET Core MVC** - Web framework
- **Entity Framework Core 8.0** - ORM
- **SQL Server** - Database
- **ASP.NET Core Identity** - Authentication & Authorization
- **Razor Pages** - View engine

### Frontend
- **Bootstrap 5** - CSS framework
- **jQuery** - JavaScript library
- **Bootstrap Icons** - Icon library
- **Font Awesome** - Additional icons

### Tools & Libraries
- **Entity Framework Tools** - Database migrations
- **Visual Studio / Rider** - IDE

---

## 📦 Prerequisites

Before running the application, ensure you have:

- **.NET 8.0 SDK** or later
- **SQL Server** (Express, Developer, or full version)
- **Visual Studio 2022** or **JetBrains Rider** (recommended)
- **Git** (for cloning the repository)

---

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/dizheleva/SoftUni-Courses.git
cd "SoftUni-Courses/C# WEB/ASP.NET Core - Exercises/CinemaWeb-May-2025"
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure Database Connection

Update the connection string in `CinemaApp/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=CinemaApp25;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;"
  }
}
```

Replace `YOUR_SERVER` with your SQL Server instance name (e.g., `localhost`, `(localdb)\\mssqllocaldb`, or your server name).

### 4. Apply Database Migrations

Navigate to the `CinemaApp.Data` project directory:

```bash
cd CinemaApp.Data
dotnet ef database update --startup-project ../CinemaApp
```

Or from the solution root:

```bash
dotnet ef database update --project CinemaApp.Data --startup-project CinemaApp
```

### 5. Run the Application

From the solution root or `CinemaApp` directory:

```bash
dotnet run --project CinemaApp
```

Or use Visual Studio/Rider:
- Set `CinemaApp` as startup project
- Press `F5` to run with debugging or `Ctrl+F5` to run without debugging

The application will start on:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`

---

## ⚙️ Configuration

### Default User Accounts

The application seeds a default manager account on first run:
- **Email**: `manager@mail.com`
- **Password**: `123456`

### Password Requirements

For development purposes, password requirements are relaxed:
- Minimum length: 3 characters
- No special requirements (no digits, uppercase, etc.)

> ⚠️ **Important**: Change these settings in production environments!

### Roles

The application uses two roles:
- **Manager** - Can manage movies and cinemas
- **User** - Regular users who can browse and manage watchlists

---

## 📊 Database Schema

### Main Entities

- **Movie** - Movie information (title, genre, director, description, duration, etc.)
- **Cinema** - Cinema locations and details
- **CinemaMovie** - Junction table linking cinemas to movies (program)
- **UserMovie** - User watchlist (many-to-many between Users and Movies)
- **Ticket** - Ticket purchases
- **Manager** - Manager accounts linked to cinemas

### Relationships

- Cinema ↔ CinemaMovie ↔ Movie (many-to-many)
- User ↔ UserMovie ↔ Movie (many-to-many for watchlist)
- Cinema → Manager (one-to-one)
- CinemaMovie → Ticket (one-to-many)

---

## 🎮 Usage

### As a Guest
1. Browse available movies and cinemas
2. View movie details
3. Register a new account

### As a Registered User
1. Login to your account
2. Browse movies and add them to your watchlist
3. Manage your watchlist (add/remove movies)

### As a Manager
1. Login with manager credentials
2. Access manager dashboard
3. Create, edit, and delete movies
4. Manage cinema programs

---

## 🔧 Development

### Adding a New Feature

1. **Create Entity** (if needed) in `CinemaApp.Data.Models`
2. **Add Configuration** in `CinemaApp.Data/Configuration`
3. **Create Repository** in `CinemaApp.Data/Repository`
4. **Create Service** in `CinemaApp.Services.Core/Services`
5. **Create ViewModel** in `CinemaApp.Web.ViewModels`
6. **Create Controller** in `CinemaApp/Controllers`
7. **Create Views** in `CinemaApp/Views`
8. **Add Migration**: `dotnet ef migrations add FeatureName --project CinemaApp.Data --startup-project CinemaApp`
9. **Update Database**: `dotnet ef database update --project CinemaApp.Data --startup-project CinemaApp`

### Running Tests

```bash
dotnet test
```

---

## 🤝 Contributing

Contributions, suggestions, and improvements are welcome!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is created for educational purposes as part of the **SoftUni C# Web Developer** course.

---

## 👤 Author

Created by **[dizheleva](https://github.com/dizheleva)**

---

## 📝 Notes

- The application uses **soft delete** for movies and cinemas (sets `IsDeleted` flag instead of removing records)
- Manager access is protected by custom middleware (`ManagerAccessMiddleware`)
- The repository pattern uses a generic `BaseRepository<T, TId>` for common CRUD operations
- All service methods follow async/await pattern for better performance

---

**Happy Coding! 🚀**
