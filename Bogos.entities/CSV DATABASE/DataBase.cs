namespace Bogos.core;

public class DataBase
{

    public DataBase()
    {

    }



    public static IEnumerable<string> GetAllCommitsFromAuthor(string AuthorName)
    {
        var author = DataCollection.Create();


        return (from commits in author
                where commits.Author.Contains(AuthorName)
                select commits.Message);

    }

    public static int GetNumberOfCommitsFromAuthor(string AuthorName)
    {

        var author = DataCollection.Create();

        return (int)(from commits in author
                     where commits.Author.Contains(AuthorName)
                     select commits.Commit).Count();

    }


}