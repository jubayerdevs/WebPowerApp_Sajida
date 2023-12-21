using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebPowerApp.Models
{
    public class EmployeeModel : DBCommonFields
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Username { get; set; }
        public string? FullName { get; set; }
        public string? Designation { get; set; }
        public string? Role { get; set; }

        [Required(ErrorMessage = "Please enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Salt { get; set; } = "SaltHere";



        public int? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual RoleModel RoleModel { get; set; }
    }
}
