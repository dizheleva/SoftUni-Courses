using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyUsers
{
    class CompanyUsers
    {
        static void Main(string[] args)
        {
            var data = new Dictionary<string, List<string>>();
            
            while (true)
            {
                var input = Console.ReadLine().Split(" -> ");
                
                if (input[0] == "End")
                {
                    break;
                }
                
                var company = input[0];
                var employee = input[1];
                
                if (!data.ContainsKey(company))
                {
                    data.Add(company, new List<string>());
                    data[company].Add(employee);
                }
                else if (!data[company].Contains(employee))
                {
                    data[company].Add(employee);
                }
            }
            
            foreach (var company in data.OrderByDescending(x => x.Key).Reverse())
            {
                Console.WriteLine($"{company.Key}");
                
                foreach (var employee in company.Value)
                {
                    Console.WriteLine($"-- {employee}");
                }
            }
        }
    }
}
