namespace _04.Chronometer
{
    public class StartUp
    {
        static void Main()
        {
            var chronometer = new Chronometer();

            string command;

            while ((command = Console.ReadLine()) != "exit")
            {
                switch (command)
                {
                    case "start":
                        Task.Run(() => chronometer.Start());
                        break;
                    case "stop":
                        chronometer.Stop();
                        break;
                    case "lap":
                        Console.WriteLine(chronometer.Lap());
                        break;
                    case "laps":
                        if (chronometer.Laps.Count == 0)
                        {
                            Console.WriteLine("Laps: no laps");
                        }
                        else
                        {
                            Console.WriteLine($"Laps: ");
                            for (int i = 0; i < chronometer.Laps.Count; i++)
                            {
                                Console.WriteLine($"{i}. {chronometer.Laps[i]}");
                            }
                        }
                        break;
                    case "time":
                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "reset":
                        chronometer.Reset();
                        break;
                }
            }
            chronometer.Stop();
        }
    }
}
