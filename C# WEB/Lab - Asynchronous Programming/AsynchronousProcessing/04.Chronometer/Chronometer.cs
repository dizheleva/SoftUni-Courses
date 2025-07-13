namespace _04.Chronometer
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Chronometer : IChronometer
    {
        private Stopwatch stopwatch;
        private List<string> laps;

        public Chronometer()
        {
            this.stopwatch = new Stopwatch();
            this.laps = new List<string>();
        }

        public string GetTime => stopwatch.Elapsed.ToString(@"mm\:ss\.ffff");
        public void Start()
        {
            stopwatch.Start();
        }
        public void Stop()
        {
            stopwatch.Stop();
        }
        public void Reset()
        {
            stopwatch.Reset();
            laps.Clear();
        }
        public List<string> Laps => laps;
        public string Lap()
        {
            string result = GetTime;
            laps.Add(result);
            return result;
        }
    }
}
