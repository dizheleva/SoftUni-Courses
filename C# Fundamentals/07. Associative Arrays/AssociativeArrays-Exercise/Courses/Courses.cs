using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses
{
    class Courses
    {
        static void Main(string[] args)
        {
            var courses = new Dictionary<string, List<string>>();

            string input;

            while ((input = Console.ReadLine()) != "end")
            {
                var splitedInput = input.Split(" : ");

                var courseName = splitedInput[0];
                var studentName = splitedInput[1];

                if (!courses.ContainsKey(courseName))
                {
                    courses[courseName] = new List<string>();
                }
                courses[courseName].Add(studentName);
            }
            
            courses = courses.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Key}: {course.Value.Count}");
                course.Value.Sort();
                
                foreach (var student in course.Value)
                {
                    Console.WriteLine($"-- {student}");
                }
            }
        }
    }
}
