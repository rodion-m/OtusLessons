namespace _28_InProcessCommunication;

using System.Threading;

// ReSharper disable once InconsistentNaming
public class CAS
{
    public static void Example()
    {
        ThreadSafeCounter counter = new ThreadSafeCounter();

        Parallel.For(0, 100_000, _ =>
        {
            counter.Increment();
        });

        Console.WriteLine("Counter Value: " + counter.GetValue()); // Output: Counter Value: 100000
    }
    
    public class ThreadSafeCounter
    {
        private int _value;

        public void Increment()
        {
            int original;

            do
            {
                original = _value;
            } while (original != Interlocked.CompareExchange(ref _value, original + 1, original));
        }

        public int GetValue()
        {
            return _value;
        }
    }
}