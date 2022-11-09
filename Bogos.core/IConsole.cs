namespace Bogos.core;

public interface IConsole
{
    void Write(string value);
    void WriteLine(string value);
    string ReadLine();

}