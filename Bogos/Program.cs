
namespace Bogos;

public class Program
{
    public static void Main(string[] args)
    {
        /*   Console.WriteLine(args.ToString());
          string path = args
                          .Where(c => c != "-am" && c != "-fm" && Path.GetPathRoot(c) != null || Path.GetFullPath(c) != null)
                          .Select(c => c).First();

          if (path == null) throw new UnmatchedPathException();
          var repo = new Repository(path + "/.git/");
          if (args.Contains("-am")) new GitAuthorMode(new RunningConsole(), repo);
          if (args.Contains("-fm")) new frequency(repo);

   */
        var repo = new Repository(@"/home/lunero/ITU/3. Semester/BDSA (analysis, design and software architecture)/Big Project/Bogos_BDSA");
        _ = new GitAuthorMode(new RunningConsole(), repo);

        /*    var repo = new frequency("../");
           foreach (var element in repo.gitInsightfrequency())
           {
               Console.WriteLine(element);
           } */
    }

}
