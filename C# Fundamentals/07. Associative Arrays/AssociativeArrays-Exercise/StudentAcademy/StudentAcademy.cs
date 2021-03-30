using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentAcademy
{
    class StudentAcademy
    {
        static void Main(string[] args)
        {
            var students = new Dictionary<string, List<double>>();
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var student = Console.ReadLine();
                var grade = double.Parse(Console.ReadLine());

                if (!students.ContainsKey(student))
                {
                    students.Add(student, new List<double>());
                    students[student].Add(grade);
                }
                else
                {
                    students[student].Add(grade);
                }
            }

            foreach (var student in students.Where(x => x.Value.Average() >= 4.50).OrderByDescending(x => x.Value.Average()))
            {
                Console.WriteLine($"{student.Key} -> {student.Value.Average():f2}");
            }
        }
    }
}
