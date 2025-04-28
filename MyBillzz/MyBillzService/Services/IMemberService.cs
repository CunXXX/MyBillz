using DTOs;

namespace MyBillzService.Services;

public interface IMemberService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}