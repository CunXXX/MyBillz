namespace Common;

public interface IDBService
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
    Task<int> ExecuteAsync(string sql, object? param = null);
    Task CommitAsync();
    Task RollbackAsync();
}
