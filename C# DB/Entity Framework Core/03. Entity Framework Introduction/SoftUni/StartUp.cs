using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using SoftUni.Models;
using Microsoft.EntityFrameworkCore;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var softUniContext = new SoftUniContext();
            var task3 = GetEmployeesFullInformation(softUniContext);
            //Console.WriteLine(task3);

            var task4 = GetEmployeesWithSalaryOver50000(softUniContext);
            //Console.WriteLine(task4);

            var task5 = GetEmployeesFromResearchAndDevelopment(softUniContext);
            //Console.WriteLine(task5);

            var task6 = AddNewAddressToEmployee(softUniContext);
            //Console.WriteLine(task6);

            var task7 = GetEmployeesInPeriod(softUniContext);
            //Console.WriteLine(task7);

            var task8 = GetAddressesByTown(softUniContext);
            //Console.WriteLine(task8);

            var task9 = GetEmployee147(softUniContext);
            //Console.WriteLine(task9);

            var task10 = GetDepartmentsWithMoreThan5Employees(softUniContext);
            //Console.WriteLine(task10);

            var task11 = GetLatestProjects(softUniContext);
            //Console.WriteLine(task11);

            var task12 = IncreaseSalaries(softUniContext);
            //Console.WriteLine(task12);

            var task13 = GetEmployeesByFirstNameStartingWithSa(softUniContext);
            //Console.WriteLine(task13);

            //var task14 = DeleteProjectById(softUniContext);
            //Console.WriteLine(task14);

            var task15 = RemoveTown(softUniContext);
            Console.WriteLine(task15);
        }

        public static string RemoveTown(SoftUniContext context)
        {
            var townToDelete = context.Towns.First(t => t.Name == "Seattle");

            var addressesToDelete = context.Addresses
                .Where(a => a.TownId == townToDelete.TownId);

            var addressesCount = addressesToDelete.Count();

            var employeesOnDeletedAddresses = context.Employees
                .Where(e => addressesToDelete.Any(a => a.AddressId == e.AddressId));

            foreach (var employee in employeesOnDeletedAddresses)
            {
                employee.AddressId = null;
            }

            foreach (var address in addressesToDelete)
            {
                context.Addresses.Remove(address);
            }

            context.Remove(townToDelete);

            context.SaveChanges();

            return $"{addressesCount} addresses in Seattle were deleted";
        }
        public static string DeleteProjectById(SoftUniContext context)
        {
            var employeeProjects = context.EmployeesProjects.Where(x => x.ProjectId == 2).ToList();

            foreach (var employeeProject in employeeProjects)
            {
                context.EmployeesProjects.Remove(employeeProject);
            }
            
            context.Projects.Remove(context.Projects.Find(2));

            context.SaveChanges();

            var sb = new StringBuilder();

            foreach (var project in context.Projects.Take(10).ToList())
            {
                sb.AppendLine($"{project.Name}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(x => x.FirstName.StartsWith("Sa"))
                // .Where(x => EF.Functions.Like(x.FirstName, "sa%")) --> will take it case insensitive
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departments = new[]
            {
                "Engineering",
                "Tool Design",
                "Marketing",
                "Information Services"
            };

            var employees = context.Employees
                .Where(x => departments.Contains(x.Department.Name))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                employee.Salary *= 1.12m;
            }

            context.SaveChanges();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            var projects = context.Projects
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .Select(x => new
                {
                    ProjectName = x.Name,
                    x.StartDate,
                    x.Description
                })
                .OrderBy(x => x.ProjectName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var project in projects)
            {
                var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                sb.AppendLine($"{project.ProjectName}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{startDate}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Where(x => x.Employees != null && x.Employees.Count > 5)
                // mind the order! First we order the departments, then the employees
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    x.Name,
                    x.Manager.FirstName,
                    x.Manager.LastName,
                    Employees = x.Employees.Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList()
                })
                .ToList();

            var sb = new StringBuilder();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.Name} - {department.FirstName}  {department.LastName}");

                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee147 = context.Employees
                .Select(x => new Employee
                {
                    // taking anonymous object cant work with Order by, so we make make object Employee
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    JobTitle = x.JobTitle,
                    EmployeesProjects = x.EmployeesProjects.Select(p => new EmployeeProject
                    {
                        Project = p.Project
                    })
                        .OrderBy(x => x.Project.Name)
                        .ToList()
                })
                .FirstOrDefault(x => x.EmployeeId == 147);

            var sb = new StringBuilder();

            sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");

            foreach (var project in employee147.EmployeesProjects)
            {
                sb.AppendLine($"{project.Project.Name}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Include(x => x.Employees)
                .Select(x => new
                {
                    x.AddressText,
                    x.Town.Name,
                    x.Employees.Count
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.AddressText)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}, {address.Name} - {address.Count} employees");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Include(x => x.EmployeesProjects)
                .ThenInclude(x => x.Project)
                .Where(x => x.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(x => new
                {
                    EmployeeFirstName = x.FirstName,
                    EmployeeLastName = x.LastName,
                    ManagerFirstName = x.Manager.FirstName,
                    ManagerLastName = x.Manager.LastName,
                    Projects = x.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        p.Project.StartDate,
                        p.Project.EndDate
                    })
                })
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    var startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    var endDate = project.EndDate == null
                        ? "not finished"
                        : project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    sb.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");
                }
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var employeeNakov = context.Employees.FirstOrDefault(x => x.LastName == "Nakov");

            employeeNakov.Address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4,
            };

            context.SaveChanges();

            var addresses = context.Employees
                .Select(x => new
                {
                    x.Address.AddressText,
                    x.Address.AddressId,
                })
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .ToList();

            var sb = new StringBuilder();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.AddressText}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(x => x.Department.Name == "Research and Development")
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Salary,
                    DepartmentName = x.Department.Name,
                })
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }


        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(x => x.Salary > 50_000)
                .Select(x => new
                {
                    x.FirstName,
                    x.Salary
                })
                .OrderBy(x => x.FirstName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.LastName,
                    x.MiddleName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x => x.EmployeeId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            var result = sb.ToString().TrimEnd();

            return result;
        }
    }
}
