namespace Bogos;

public class DataBase{

    public DataBase(){
        
    }

    /* public void CountCommitsFromAuthor(){
        throw new NotImplementedException();
    } */
    
    public static IEnumerable<string> GetAllCommitsFromAuthor(string AuthorName)
    {
        var author  = DataCollection.Create();


        return (from commits in author
                where commits.Author.Contains(AuthorName)
                select commits.Message);

    }


}