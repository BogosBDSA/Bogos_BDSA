
namespace Bogos;

public class Program
{
    public static void Main(string[] args)
    {
        var repo = new frequency("../");
        foreach (var element in repo.gitInsightfrequency())
        {
            Console.WriteLine(element);
        }
    }

}
