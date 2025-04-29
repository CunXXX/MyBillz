using Common;
using Dapper;
using DBModel;
using System.Security.Principal;

namespace MyBillzService.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly IDBService _dbService;

    public MemberRepository(IDBService dbService)
    {
        _dbService = dbService;
    }

    /// <summary>
    /// 依帳號取得會員資料
    /// </summary>
    /// <param name="strAccount"></param>
    /// <returns></returns>
    public async Task<Member?> GetMemberByAccountAsync(string strAccount)
    {
        var _strSql = $"SELECT * FROM {Member.TableName} WHERE Account = @Account";

        var _dpParameter = new DynamicParameters();
        _dpParameter.Add("@Account", strAccount);

        var _ienumMemberRtn = await _dbService.QueryAsync<Member>(_strSql, _dpParameter);

        return _ienumMemberRtn.FirstOrDefault();
    }

    /// <summary>
    /// 新增/更新會員資料
    /// </summary>
    /// <param name="mUser"></param>
    /// <returns></returns>
    public async Task<Member?> CreateOrUpdateMembertAsync(Member mUser)
    {
        Member? _mRtn = null;
        try
        {
            var _strSql = $@"
            MERGE INTO {Member.TableName} AS Target
            USING (SELECT @Account AS Account) AS Source
            ON Target.Account = Source.Account
            WHEN MATCHED THEN
                UPDATE SET 
                    Password = @Password,
                    Email = @Email,
                    Mobile = @Mobile,
                    Token = @Token,
                    LoginTime = @LoginTime,
                    LogoutTime = @LogoutTime,
                    Status = @Status,
                    IsDeleted = @IsDeleted,
                    ModifyTime = @ModifyTime,
                    ModifyUser = @ModifyUser
            WHEN NOT MATCHED THEN
                INSERT (Account, Password, Email, Mobile, Token, LoginTime, LogoutTime, Status, IsDeleted, CreateTime, CreateUser, ModifyTime, ModifyUser)
                VALUES (@Account, @Password, @Email, @Mobile, @Token, @LoginTime, @LogoutTime, @Status, @IsDeleted, @CreateTime, @CreateUser, @ModifyTime, @ModifyUser)
            OUTPUT inserted.Id;
        ";

            var _ienumMembers = await _dbService.QueryAsync<int>(_strSql, mUser);
            var _iRtn = _ienumMembers.FirstOrDefault();

            if (_iRtn != 0)
            {
                _mRtn = mUser;
            }
        }
        catch (Exception ex)
        {
            _mRtn = null;
            throw new Exception(ex.Message);
        }

        return _mRtn;
    }

}
