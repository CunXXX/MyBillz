using Common;
using DTOs;
using Helper;
using MyBillzService.Logging;
using MyBillzService.Repositories;

namespace MyBillzService.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepo;
    private readonly IDBService _dbService;
    private readonly ILoggingService _log;

    public MemberService(IMemberRepository memberRepo,
                         IDBService dbService,
                         ILoggingService log)
    {
        _memberRepo = memberRepo;
        _dbService = dbService;
        _log = log;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        try
        {
            var user = await _memberRepo.GetMemberByAccountAsync(request.Account);

            if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.Password))
            {
                _log.LogWarning($"登入失敗：找不到帳號 {request.Account}");
                await _dbService.RollbackAsync();
                return null;
            }

            if (user.Password != request.Password) 
            {
                _log.LogWarning($"登入失敗：密碼錯誤 for 帳號 {request.Account}");
                await _dbService.RollbackAsync();
                return null;
            }

            _log.LogInfo($"登入成功：帳號 {request.Account}");


            //
            await _dbService.CommitAsync();

            var _strToken = TokenHelper.GenerateJwtToken(user.Account);

            return new LoginResponseDto
            {
                UserId = user.UserId,
                Account = user.Account,
                Token = "mock-token-here",
                ExpiresIn = 3600
            };
        }
        catch (Exception ex)
        {
            // 
            await _dbService.RollbackAsync();
            _log.LogError("登入流程發生錯誤", ex);
            throw;
        }
    }
}