
namespace Bogos.entities;
public interface IGitRepoRepository
{
    public GitRepo GetRepoByID(int ID);
    public GitRepo GetRepoByUri(string URI);
    public IEnumerable<GitRepo> GetAllRepos();
    public Status DeleteRepo(GitRepo repo);
    public Status UpdateRepo(GitRepo repo);
    public Status CreateRepo(GitRepo repo);
}