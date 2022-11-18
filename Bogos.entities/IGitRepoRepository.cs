
namespace Bogos.entities;
public interface IGitRepoRepository
{
    public (Status, GitRepo?) HandleUri(String uri);
    public (Status, GitRepo?) CreateRepo(GitRepo repo);
    public (Status, GitRepo?) UpdateRepo(GitRepo repo);
    public Status DeleteRepo(GitRepo repo);
    public IEnumerable<GitRepo> ReadAllRepos();
    public GitRepo ReadRepoByID(int id);
    public GitRepo ReadRepoByUri(string uri);
}