namespace _04.Chronometer
{
    public interface IChronometer
    {
        string GetTime { get; }

        void Start();

        void Stop();

        void Reset();

        List<string> Laps { get; }

        string Lap();
    }
}
