namespace DBModel;

public class Member
{
    private static string _TableName = "Member";

    public static string TableName { get { return _TableName; } set { TableName = value; } }
    
    /// <summary>
    /// PK
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 帳號
    /// </summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 密碼
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 電子信箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手機號碼
    /// </summary>
    public string? Mobile { get; set; } = string.Empty;

    /// <summary>
    /// Token
    /// </summary>
    public string? Token { get; set; } = string.Empty;

    /// <summary>
    /// 登入時間點
    /// </summary>
    public DateTime? LoginTime { get; set; } = null;

    /// <summary>
    /// 登出時間點
    /// </summary>
    public DateTime? LogoutTime { get; set; } = null;

    /// <summary>
    /// 帳號狀態 (0=正常, 1=停權,2=黑名單)
    /// </summary>
    public int Status { get; set; } = 0;

    /// <summary>
    /// 是否刪除
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreateTime { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 建立人員
    /// </summary>
    public string CreateUser { get; set; } = string.Empty;

    /// <summary>
    /// 修改時間
    /// </summary>
    public DateTime ModifyTime { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 修改人員
    /// </summary>
    public string ModifyUser { get; set; } = string.Empty;
}
