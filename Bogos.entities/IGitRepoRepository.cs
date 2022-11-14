
namespace Bogos.entities;
public interface IGitRepoRepository
{
    public GitRepo ReadRepoByID(int id);
    public GitRepo ReadRepoByUri(string uri);
    public IEnumerable<GitRepo> ReadAllRepos();
    public Status DeleteRepo(GitRepo repo);
    public Status UpdateRepo(GitRepo repo);
    public (Status,GitRepo) CreateRepo(GitRepo repo);
}