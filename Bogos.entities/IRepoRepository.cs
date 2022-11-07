
namespace Bogos.entities;
public interface IRepoRepository
{
    public GitRepo GetRepoByID(int ID);
    public GitRepo GetRepoByName(string name);
    public IEnumerable<GitRepo> GetAllRepos();
    public IEnumerable<Commit> GetAllCommits();
    public Commit GetLatestCommit();
    public Status DeleteRepo();
    public Status UpdateRepo(GitRepo repo);
    public Status CreateRepo(GitRepo repo);
}