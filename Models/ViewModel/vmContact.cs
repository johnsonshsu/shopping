using System.ComponentModel.DataAnnotations;

public class vmContact
{
    [Display(Name = "連絡人名稱")]
    [Required(ErrorMessage = "連絡人名稱不可空白!!")]
    public string ContactorName { get; set; } = "";
    [Display(Name = "電子信箱")]
    [Required(ErrorMessage = "電子信箱不可空白!!")]
    [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
    public string ContactorEmail { get; set; } = "";
    [Display(Name = "標題主旨")]
    [Required(ErrorMessage = "主旨不可空白!!")]
    public string ContactorSubject { get; set; } = "";
    [Display(Name = "內容文字")]
    [Required(ErrorMessage = "內容文字不可空白!!")]
    public string ContactorMessage { get; set; } = "";
}