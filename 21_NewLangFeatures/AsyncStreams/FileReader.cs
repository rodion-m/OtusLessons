namespace _21_NewLangFeatures.AsyncStreams;

public class FileReader
{
    public static async Task<bool> Contains(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        // 1 долго
        // 2 дорого по памяти
        var lines = await File.ReadAllLinesAsync("file.txt");
        var result = lines.Any(it => it == text);
        return result;
    }
    
    public static async Task Example()
    {
        File.WriteAllLines("file.txt", new []
        {
            "1 line", "stop", "2 line", "3 line"
        });
        
        var fileReader = new FileReader();
        await foreach (var line in fileReader.ReadLinesStream("file.txt"))
        {
            await Console.Out.WriteLineAsync(line);
            if(line == "stop") break;
        }
    }
    
    public async IAsyncEnumerable<string> ReadLinesStream(string fileName)
    {
        yield return "start";
        yield return "start 2";
        using StreamReader reader = File.OpenText(fileName);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            yield return line!;
        }
    }
}