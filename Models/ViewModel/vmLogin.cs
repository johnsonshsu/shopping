/// <summary>
/// 登入用 ViewModel
/// </summary>
public class vmLogin
{
    [Display(Name = "登入帳號")]
    [Required(ErrorMessage = "登入帳號不可空白!!")]
    public string UserNo { get; set; } = "";
    [Display(Name = "登入密碼")]
    [Required(ErrorMessage = "登入密碼不可空白!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}