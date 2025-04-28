namespace DBModel;

public class Member
{
    public int UserId { get; set; }
    public string Account { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Token { get; set; } = string.Empty;
}
