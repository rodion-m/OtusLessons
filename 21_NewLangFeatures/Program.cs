//Top-level statement

using _21_NewLangFeatures;
using _21_NewLangFeatures.AsyncStreams;

await FileReader.Example();

return;

//<WarningsAsErrors>nullable</WarningsAsErrors> - работает только на уровне компиляции

NRT.ReverseOrNull(null!);


return;
var s = Console.ReadLine();
ParseResult parseResult = ParseString(s);

ParseResult newResult = parseResult with { Result = 1000 };


ParseResult ParseString(string s)
{
    if (int.TryParse(s, out var result))
    {
        return new(result, true);
    }
    else
    {
        return new(null, false);
    }
}

// Immutable
record ParseResult(int? Result, bool Success)
{
    public ParseResult(int? Result, bool Success, DateTimeOffset parsedAt) : this(Result, Success)
    {
        ParsedAt = parsedAt;
    }
    
    public DateTimeOffset ParsedAt { get; set; }
}