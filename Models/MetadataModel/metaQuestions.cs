using System;
using System.Collections.Generic;

namespace shopping.Models
{
    [ModelMetadataType(typeof(z_metaQuestions))]
    public partial class Questions
    {

    }
}

public class z_metaQuestions
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "問題")]
    public string? QuestionText { get; set; }
    [Display(Name = "答案")]
    public string? AnswerText { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}