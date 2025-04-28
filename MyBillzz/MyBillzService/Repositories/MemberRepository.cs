using Common;
using DBModel;

namespace MyBillzService.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly IDBService _dbService;

    public MemberRepository(IDBService dbService)
    {
        _dbService = dbService;
    }

    public async Task<Member?> GetMemberByAccountAsync(string account)
    {
        var sql = "SELECT UserId, Account, Password FROM Members WHERE Account = @Account";
        var result = await _dbService.QueryAsync<Member>(sql, new { Account = account });

        return result.FirstOrDefault();
    }
}
