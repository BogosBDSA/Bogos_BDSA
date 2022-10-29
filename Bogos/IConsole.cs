namespace Bogos;

public interface IConsole
{
    void Write(string value);
    void WriteLine(string value);
    string Readline();

}