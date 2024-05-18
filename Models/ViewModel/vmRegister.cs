using System.ComponentModel.DataAnnotations;

/// <summary>
/// 註冊用 ViewModel
/// </summary>
public class vmRegister
{
    [Display(Name = "登入帳號")]
    [Required(ErrorMessage = "登入帳號不可空白!!")]
    public string? UserNo { get; set; } = "";
    [Display(Name = "登入名稱")]
    [Required(ErrorMessage = "登入名稱不可空白!!")]
    public string? UserName { get; set; } = "";
    [Display(Name = "登入密碼")]
    [Required(ErrorMessage = "登入密碼不可空白!!")]
    [DataType(DataType.Password)]
    public string? Password { get; set; } = "";
    [Display(Name = "確認密碼")]
    [Required(ErrorMessage = "確認密碼不可空白!!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "與登入密碼輸入不相符!!")]
    public string? ConfirmPassword { get; set; } = "";
    [Display(Name = "性別")]
    public string? GenderCode { get; set; } = "";
    [Display(Name = "電子信箱")]
    [Required(ErrorMessage = "電子信箱不可空白!!")]
    [EmailAddress(ErrorMessage = "電子信箱格式輸入錯誤!!")]
    public string? Email { get; set; } = "";
    [Display(Name = "連絡電話")]
    public string? Tel { get; set; } = "";
    [Display(Name = "連絡地址")]
    public string? Address { get; set; } = "";
}