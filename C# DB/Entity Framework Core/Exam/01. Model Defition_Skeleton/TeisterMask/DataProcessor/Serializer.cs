using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TeisterMask.DataProcessor.ExportDto;

namespace TeisterMask.DataProcessor
{
    using System;

    using Data;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        //public static void  Main() { }
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportProjectsDto[]), new XmlRootAttribute("Projects"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var projects = context
                .Projects
                .ToArray()
                .Where(p => p.Tasks.Any())
                .Select(p => new ExportProjectsDto()
                {
                    ProjectName = p.Name,
                    TasksCount = p.Tasks.Count,
                    HasEndDate = p.DueDate != null ? "Yes" : "No",
                    Tasks = p.Tasks
                        .Select(m => new TaskDto()
                        {
                            Name = m.Name,
                            Label = m.LabelType.ToString()
                        })
                        .ToArray()
                        .OrderBy(t => t.Name)
                        .ToArray()
                })
                .ToArray()
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.ProjectName)
                .ToArray();

            using var stringWriter = new StringWriter(sb);
            xmlSerializer.Serialize(stringWriter, projects, namespaces);

            //string utf8;
            //using (StringWriter writer = new Utf8StringWriter())
            //{
            //    xmlSerializer.Serialize(writer, projects, namespaces);
            //    utf8 = writer.ToString();
            //}
            //return utf8;

            return sb.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            // openDate is after or equal to date
            var employees = context
                .Employees
                .ToArray()
                .Where(p => p.EmployeesTasks.Any(t => t.Task.OpenDate >= date))
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .Where(t => t.Task.OpenDate >= date)
                        .OrderByDescending(o => o.Task.DueDate)
                        .ThenBy(o => o.Task.Name)
                        .ToArray()
                        .Select(ta => new
                        {
                            TaskName = ta.Task.Name,
                            OpenDate = ta.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = ta.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = ta.Task.LabelType.ToString(),
                            ExecutionType = ta.Task.ExecutionType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(p => p.Tasks.Length)
                .ThenBy(p => p.Username)
                .Take(10)
                .ToArray();

            var json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return json;
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}