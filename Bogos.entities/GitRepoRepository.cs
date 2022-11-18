namespace Bogos.entities;

public class GitRepoRepository : IGitRepoRepository
{
    private readonly GitContext _Context;
    public GitRepoRepository(GitContext context)
    {
        _Context = context;

    }

    public (Status, GitRepo?) HandleUri(String uri) 
    {
        var existingRepo = ReadRepoByUri(uri);
        if (existingRepo == null) {
            return CreateRepo(new GitRepo(uri));
        }
        var updatedRepo = new GitRepo(uri);
        return UpdateRepo(updatedRepo);
    }
    
    public (Status, GitRepo?) CreateRepo(GitRepo repo)
    {
        // Make sure repo doesn't alredy exist in repository
        GitRepo? entity = _Context.repos
                            .FirstOrDefault(r => r.Uri.Equals(repo.Uri));
        if (entity != null) return (Status.CONFLICT, null);

        // Add repo to repository
        _Context.repos.Add(repo);
        _Context.SaveChanges();
        return (Status.CREATED, repo);
    }

    public (Status, GitRepo?) UpdateRepo(GitRepo repo)
    {   
        var existingRepo = _Context.repos.FirstOrDefault(r => r.Uri == repo.Uri);
        if (existingRepo == null) {
            return (Status.NOTFOUND, null); 
        }

        // Update
        var result = _Context.Update(repo);
        _Context.SaveChanges();

        if (result.Entity.Equals(existingRepo)) { 
            return (Status.UNCHANGED, result.Entity);
        }

        return (Status.UPDATED, result.Entity);
    }


    public Status DeleteRepo(GitRepo repo)
    {
        var entity = _Context.repos.Find(repo.Id);
        if (entity == null) {
            return Status.NOTFOUND;
        }
        
        _Context.repos.Remove(entity);
        _Context.SaveChanges(); 
        return Status.DELETED;
    }

    public IEnumerable<GitRepo> ReadAllRepos()
    {
        var _listOfReposInContext = new List<GitRepo>();
        foreach (var repo in _Context.repos)
        {
            _listOfReposInContext.Add(repo);
        }
        return _listOfReposInContext;
    }

    public GitRepo? ReadRepoByID(int id)
    {
        return _Context.repos.FirstOrDefault(k => k.Id == id);
    }


    public GitRepo? ReadRepoByUri(string uri)
    {
        var tempUri = new Uri(uri);
        return ReadRepoByUri(tempUri);
    }

    public GitRepo? ReadRepoByUri(Uri uri) 
    {
        return _Context.repos.FirstOrDefault(k => k.Uri.Equals(uri.AbsoluteUri));
    }
}