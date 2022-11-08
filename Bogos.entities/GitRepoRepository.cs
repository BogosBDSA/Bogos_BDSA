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
        if (repo.commits.Count() == 0) return Status.EMPTYREPO;
        var entity = _Context.Repos
                    .Select(c => c)
                    .Where(c => c.commits.First().Sha == repo.commits.First().Sha)
                    .First();

        if (entity != null) return Status.CONFLICT;
        _Context.Repos.Add(repo);
        _Context.SaveChanges();
        return Status.CREATED;


    }

    public Status DeleteRepo(GitRepo repo)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GitRepo> GetAllRepos()
    {
        throw new NotImplementedException();
    }

    public GitRepo GetRepoByID(int ID)
    {
        throw new NotImplementedException();
    }

    public GitRepo GetRepoByUri(string URI)
    {
        throw new NotImplementedException();
    }

    public Status UpdateRepo(GitRepo repo)
    {
        throw new NotImplementedException();
    }
}