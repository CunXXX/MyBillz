using System.Security.Cryptography;
using System.Text;

namespace Helper;

public static class PasswordHelper
{
    public static string HashPassword(string strPassword)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(strPassword);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public static bool VerifyPassword(string strPassword, string strHashedPassword)
    {
        var hashOfInput = HashPassword(strPassword);
        return hashOfInput == strHashedPassword;
    }
}
