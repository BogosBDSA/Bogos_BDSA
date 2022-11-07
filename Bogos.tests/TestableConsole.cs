
class TestableConsole : IConsole
{
    public List<string> WrittenLines { get; set; }
    public string? LineToread { get; set; }
    public TestableConsole()
    {
        WrittenLines = new List<string>();

    }
    public string ReadLine()
    {
        return LineToread!;
    }

    public void Write(string value)
    {
        WrittenLines.Add(value);
    }

    public void WriteLine(string value)
    {
        WrittenLines.Add(value);
    }
}