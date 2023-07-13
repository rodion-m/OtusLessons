using System.Timers;
using Timer = System.Timers.Timer;

namespace _17_Delegates;

public class TimerExample : IDisposable
{
    private Timer _timer;

    public TimerExample()
    {
        _timer = new Timer(TimeSpan.FromSeconds(1));
    }

    private void WriteCurrentTime(object? _, ElapsedEventArgs args)
    {
        Console.WriteLine($"Текущее время {args.SignalTime}");
    }

    public void Start()
    {
        _timer.Elapsed += WriteCurrentTime;
        _timer.Start();
    }

    public void Stop()
    {
        _timer.Stop();
        _timer.Elapsed -= WriteCurrentTime;
    }

    public void Dispose()
    {
        Stop();
        _timer.Dispose();
    }
}