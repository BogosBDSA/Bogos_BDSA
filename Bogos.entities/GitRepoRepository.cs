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
                            .FirstOrDefault(r => r.Uri.Equals(repo.Uri));
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
        var _listOfReposInContext = new List<GitRepo>();
        foreach (var repo in _Context.Repos)
        {
        _listOfReposInContext.Add(repo);
        }
        return _listOfReposInContext;
    }

    public GitRepo? ReadRepoByID(int id)
    {
        return _Context.Repos.FirstOrDefault(k => k.Id == id);
    }


    public GitRepo ReadRepoByUri(string uri)
    {
        throw new NotImplementedException();
    }
    // Up for grabs
    public Status UpdateRepo(GitRepo repo)
    {   
        var state = _Context.Update(repo);
        if(state.State==EntityState.Modified)
        {
        return Status.UPDATED;
        } else return Status.NOTFOUND;
    }
}