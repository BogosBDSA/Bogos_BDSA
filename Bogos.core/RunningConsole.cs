namespace Bogos.core;
class RunningConsole : IConsole
{
    public string ReadLine()
    {
        return Console.ReadLine()!;
    }

    public void WriteLine(string value)
    {
        Console.WriteLine(value);
    }

    public void Write(string value)
    {
        Console.Write(value);
    }

}