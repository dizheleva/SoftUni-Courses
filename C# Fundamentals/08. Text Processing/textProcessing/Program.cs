using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace textProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string result = "";

            foreach (char value in input)
            {
                // See if character is in the result already.
                int ind = input.IndexOf(value);
                if (result.IndexOf(value) == -1)
                {
                    // Append to the result.
                    result += value;
                }
                else if (input[input.IndexOf(value)-1]!= result[result.IndexOf(value)])
                {
                    result += value;
                }
            }
            Console.WriteLine(result);
        }
    }
}
