using Common;
using DTOs;
using Helper;
using MyBillzService.Logging;
using MyBillzService.Repositories;

namespace MyBillzService.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository m_MemberRepo;
    private readonly IDBService m_dbService;
    private readonly ILoggingService m_Log;

    public MemberService(IMemberRepository memberRepo,
                         IDBService dbService,
                         ILoggingService log)
    {
        m_MemberRepo = memberRepo;
        m_dbService = dbService;
        m_Log = log;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dtoRequest)
    {
        try
        {
            var _mUser = await m_MemberRepo.GetMemberByAccountAsync(dtoRequest.Account);

            if (_mUser == null)
                throw new Exception($"登入失敗：找不到帳號 {dtoRequest.Account}");

            if (_mUser.Password != dtoRequest.Password)
                throw new Exception($"登入失敗：密碼錯誤 for 帳號 {dtoRequest.Account}");

            //更新登入時間、Token
            var _strToken = TokenHelper.GenerateJwtToken(_mUser.Account);
            var _dtNow = DateTime.Now;

            _mUser.Token = _strToken;
            _mUser.LoginTime = _dtNow;
            _mUser.LogoutTime = null;
            _mUser.ModifyTime = _dtNow;

            _mUser = await m_MemberRepo.CreateOrUpdateMembertAsync(_mUser);

            if (_mUser == null)
                throw new Exception($"登入失敗：更新使用者資料失敗 for 帳號 {dtoRequest.Account}");

            m_Log.LogInfo($"登入成功：帳號 {dtoRequest.Account}");

            //
            await m_dbService.CommitAsync();

            return new LoginResponseDto
            {
                UserId = _mUser.Id,
                Account = _mUser.Account,
                Token = _strToken,
                ExpiresIn = 3600
            };
        }
        catch (Exception ex)
        {
            // 
            await m_dbService.RollbackAsync();
            throw new Exception($"登入流程異常：{ex.Message}", ex);
            throw;
        }
    }
}