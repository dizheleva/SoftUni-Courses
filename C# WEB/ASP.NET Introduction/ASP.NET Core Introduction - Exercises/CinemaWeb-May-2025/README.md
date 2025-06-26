# CinemaWeb (ASP.NET Core Introduction - Exercises)

A simple cinema web application built with ASP.NET Core as part of SoftUni C# Web courese. This project demonstrates the basics of building an MVC application, handling data, and creating user interfaces with .NET technologies.

---

## Table of Contents

- [About](#about)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Technologies Used](#technologies-used)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## About

CinemaWeb is a basic web application for managing movies in a cinema context. It was developed as an exercise during the ASP.NET Core Fundamentals module at SoftUni. The app implements core ASP.NET concepts such as MVC architecture, routing, and simple data handling.

---

## Features

- List available movies
- Add new movies to the database
- View detailed information about each movie
- Edit and delete movie records
- Responsive user interface with Razor views

---

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/dizheleva/SoftUni-Courses.git
    cd SoftUni-Courses/C# WEB/ASP.NET Introduction/ASP.NET Core Introduction - Exercises/CinemaWeb-May-2025
    ```
2. Restore dependencies:
    ```bash
    dotnet restore
    ```
3. Update the database:
    ```bash
    dotnet ef database update
    ```
4. Run the application:
    ```bash
    dotnet run
    ```

---

## Usage

- Open your browser and navigate to the local URL shown in the terminal (typically `https://localhost:5001` or `http://localhost:5000`).
- Use the navigation menu to browse, add, edit, or delete movies.

---

## Technologies Used

- **C#**
- **ASP.NET Core MVC**
- **Razor Views**
- **Entity Framework Core** 
- **Bootstrap** (for responsive layout)

---

## Project Structure

```
CinemaWeb-May-2025/
├── Controllers/
├── Models/
├── Views/
├── wwwroot/
├── Data/
├── Program.cs
├── Startup.cs
└── README.md
```

- `Controllers/` – Application controllers (logic)
- `Models/` – Data models/entities
- `Views/` – Razor page views
- `wwwroot/` – Static files (CSS, JS, images)
- `Data/` – Database context/migrations 

---

## Contributing

Contributions, suggestions, and improvements are welcome!  
Please open an issue or submit a pull request.

---

## License

This project is for educational purposes as part of the SoftUni ASP.NET Core Introduction course.

---

## Contact

Created by [dizheleva](https://github.com/dizheleva)  
For questions or feedback, open an issue or reach out via GitHub.
