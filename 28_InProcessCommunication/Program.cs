using System.Diagnostics;
using System.Numerics;

Console.WriteLine("Running...");

Console.WriteLine(Environment.CurrentManagedThreadId);
Console.WriteLine("Parallel");
Parallel.Invoke(PrintThreadId, PrintThreadId, PrintThreadId);
return;

void PrintThreadId() => Console.WriteLine(Environment.CurrentManagedThreadId);

PLINQExample();


void PLINQExample()
{
    var numbers = Enumerable.Range(0, 10_000_000)
        .Select(_ => Random.Shared.Next(0, 1_000_000))
        .Select(n => (long) n)
        .ToArray();
    {
        var sw = Stopwatch.StartNew();
        var ordered = numbers
            .OrderBy(it => it)
            .ToArray()
            ;
        Console.WriteLine(ordered);
        Console.WriteLine(sw.ElapsedMilliseconds);
    }
    
    {
        var sw = Stopwatch.StartNew();
        var sum = numbers
            .AsParallel()
            .OrderBy(it => it)
            .WithDegreeOfParallelism(2)
            .ToArray();
        Console.WriteLine(sum);
        Console.WriteLine(sw.ElapsedMilliseconds);
    }
}

async Task AddUserAsync(CancellationToken cancellationToken)
{
    await Task.Delay(100, cancellationToken);
    //await dbContext.Users.AddAsync(new User(), cancellationToken);
}