using Bogos;

class TestableConsole : IConsole
{
    public IEnumerable<string> WrittenLines { get; set; }
    public string? LineToread { get; set; }
    public TestableConsole()
    {
        WrittenLines = new List<string>();

    }
    public string Readline()
    {
        return LineToread!;
    }

    public void Write(string value)
    {
        WrittenLines.Append(value);
    }

    public void WriteLine(string value)
    {
        WrittenLines.Append(value);
    }
}