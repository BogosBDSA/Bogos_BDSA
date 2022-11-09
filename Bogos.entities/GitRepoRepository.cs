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
        if (repo.Commits.Count() == 0) return Status.EMPTYREPO;
        GitRepo? entity = _Context.Repos
                            .FirstOrDefault(c => c.Commits.FirstOrDefault().Sha == repo.Commits.FirstOrDefault().Sha && c.Uri == repo.Uri);

        if (entity != null) return Status.CONFLICT;
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
        throw new NotImplementedException();
    }
    // Up for grabs
    public Status UpdateRepo(GitRepo repo)
    {
        throw new NotImplementedException();
    }
}