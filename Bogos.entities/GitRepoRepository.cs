namespace Bogos.entities;

public class GitRepoRepository : IGitRepoRepository
{
    public Status CreateRepo(GitRepo repo)
    {
        throw new NotImplementedException();
    }

    public Status DeleteRepo()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GitCommit> GetAllCommits()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GitRepo> GetAllRepos()
    {
        throw new NotImplementedException();
    }

    public GitCommit GetLatestCommit()
    {
        throw new NotImplementedException();
    }

    public GitRepo GetRepoByID(int ID)
    {
        throw new NotImplementedException();
    }

    public GitRepo GetRepoByName(string name)
    {
        throw new NotImplementedException();
    }

    public Status UpdateRepo(GitRepo repo)
    {
        throw new NotImplementedException();
    }
}