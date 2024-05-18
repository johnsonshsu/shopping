using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopping.Models
{
    [ModelMetadataType(typeof(z_metaRoles))]
    public partial class Roles
    {
    }
}

public partial class z_metaRoles
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "角色代號")]
    public string? RoleNo { get; set; }
    [Display(Name = "角色名稱")]
    public string? RoleName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}