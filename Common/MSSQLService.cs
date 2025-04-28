using Dapper;
using Microsoft.Data.SqlClient;

namespace Common;

public class MSSQLService : IDBService, IDisposable
{
    private readonly SqlConnection _connection;
    private SqlTransaction? _transaction;

    public MSSQLService(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        return await _connection.QueryAsync<T>(sql, param, _transaction);
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        return await _connection.ExecuteAsync(sql, param, _transaction);
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await Task.Run(() => _transaction.Commit()); 
            _transaction = _connection.BeginTransaction();
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await Task.Run(() => _transaction.Rollback()); 
            _transaction = _connection.BeginTransaction();
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection.Dispose();
    }
}
