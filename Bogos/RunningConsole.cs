namespace Bogos;
class RunningConsole : IConsole
{
    public string Readline()
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