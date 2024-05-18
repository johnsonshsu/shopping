using System.ComponentModel.DataAnnotations;

/// <summary>
/// 忘記密碼用 ViewModel
/// </summary>
public class vmForget
{
    [Display(Name = "帳號或電子信箱")]
    [Required(ErrorMessage = "帳號或電子信箱不可空白!!")]
    public string UserNo { get; set; } = "";
}