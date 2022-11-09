namespace Bogos.entities;

public class GitRepoRepository : IGitRepoRepository
{
    private readonly GitContext _Context;
    public GitRepoRepository(GitContext context)
    {
        _Context = context;

    }


    public Status CreateRepo(GitRepo repo)
    {
        // Make sure repo doesn't alredy exist in repository
        GitRepo? entity = _Context.Repos
                            .FirstOrDefault(r => r == repo);
        if (entity != null) return Status.CONFLICT;

        // Add repo to repository
        _Context.Repos.Add(repo);
        _Context.SaveChanges();
        return Status.CREATED;
    }

    public Status DeleteRepo(GitRepo repo)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GitRepo> ReadAllRepos()
    {
        throw new NotImplementedException();
    }

    public GitRepo ReadRepoByID(int id)
    {
        throw new NotImplementedException();
    }

    public GitRepo ReadRepoByUri(string uri)
    {
       var tempUri = new Uri(uri);
        return ReadRepoByUri(tempUri);
    }

    public GitRepo ReadRepoByUri(Uri uri){

        // foreach(var repo in _Context.Repos){
        //     if(uri == repo.Uri){
        //         return repo;
        //     }
        // }
        // return null;

        return _Context.Repos.FirstOrDefault(k => k.Uri == uri);

    }

    // Up for grabs
    public Status UpdateRepo(GitRepo repo)
    {
        throw new NotImplementedException();
    }
}