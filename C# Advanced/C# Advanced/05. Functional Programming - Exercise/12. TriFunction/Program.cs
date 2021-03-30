using System;
using System.Linq;

namespace _12._TriFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, int, bool> equalOrLangerFunc =
                (word, sum) => word.Sum(x => x) >= sum;

            Func<string[], Func<string, int, bool>, int, string> findTargetFunc =
                (n, isLargerFunc, s) => n.FirstOrDefault(x => isLargerFunc(x, s));

            var sum = int.Parse(Console.ReadLine());

            var names = Console.ReadLine().Split();

            var target = findTargetFunc(names, equalOrLangerFunc, sum);

            Console.WriteLine(target);
        }
    }
}
