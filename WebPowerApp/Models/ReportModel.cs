using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPowerApp.Models
{
    public class ReportModel
    {
        [Key]
        public int Id { get; set; }
        public string? ReportName { get; set; }
        public string? AuthenticationType { get; set; }
        public string? ApplicationId { get; set; }
        public Guid? WorkspaceId { get; set; }
        public Guid? ReportId { get; set; }
        public string? AuthorityUrl { get; set; }
        public string? Scope { get; set; }
        public string? UrlPowerBiServiceApiRoot { get; set; }
        public string? ApplicationSecret { get; set; }
        public string? Tenant { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; } = true;

    }
}
