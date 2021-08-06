using System;
using System.Numerics;

namespace _11.Snowballs
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int snowballSnowMax = 0;
            int snowballTimeMax = 0;
            int snowballQualityMax = 0;
            BigInteger snowballValueMax = 0;
            BigInteger max = 0;

            while (n > 0)
            {
                int snowballSnow = int.Parse(Console.ReadLine());
                int snowballTime = int.Parse(Console.ReadLine());
                int snowballQuality = int.Parse(Console.ReadLine());
                BigInteger snowballValue = 0;

                if (snowballTime > 0)
                {
                    snowballValue = BigInteger.Pow(snowballSnow / snowballTime, snowballQuality);
                }

                if (max <= snowballValue)
                {
                    max = snowballValue;
                    snowballSnowMax = snowballSnow;
                    snowballTimeMax = snowballTime;
                    snowballQualityMax = snowballQuality;
                    snowballValueMax = snowballValue;
                }

                n--;
            }

            Console.WriteLine($"{snowballSnowMax} : {snowballTimeMax} = {snowballValueMax} ({snowballQualityMax})");
        }
    }
}
