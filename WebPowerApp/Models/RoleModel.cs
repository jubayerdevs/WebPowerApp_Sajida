using System.ComponentModel.DataAnnotations;

namespace WebPowerApp.Models
{
    public class RoleModel:DBCommonFields
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
