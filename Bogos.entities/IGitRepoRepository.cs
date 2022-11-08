
namespace Bogos.entities;
public interface IGitRepoRepository
{
    public GitRepo GetRepoByID(int ID);
    public GitRepo GetRepoByName(string name);
    public IEnumerable<GitRepo> GetAllRepos();
    public IEnumerable<GitCommit> GetAllCommits();
    public GitCommit GetLatestCommit();
    public Status DeleteRepo();
    public Status UpdateRepo(GitRepo repo);
    public Status CreateRepo(GitRepo repo);
}