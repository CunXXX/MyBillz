using DBModel;

namespace MyBillzService.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetMemberByAccountAsync(string strAccount);
    Task<Member?> CreateOrUpdateMembertAsync(Member mUser);
}