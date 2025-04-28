using System.ComponentModel.DataAnnotations;

namespace Model;

public class RegisterInputModel
{
    [Required(ErrorMessage = "帳號為必填")] public string Account { get; set; } = string.Empty;

    [Required(ErrorMessage = "密碼為必填")] public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "請再次確認密碼")] public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email 為必填")][EmailAddress(ErrorMessage = "Email 格式不正確")] public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    [Required] public bool AgreePrivacy { get; set; }
    [Required] public bool AgreeTerms { get; set; }
}

public class FaqModel
{
    public int Id { get; set; } = 0;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public bool IsOpen { get; set; } = false;
}
