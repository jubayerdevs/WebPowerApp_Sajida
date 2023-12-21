using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebPowerApp.Models
{
    public class DBCommonFields
    {
        [DisplayName("Active")]
        public bool IsActive { get; set; } = true;

        [DisplayName("Created By")]
        public string? CreatedBy { get; set; } = string.Empty;
        [Column(TypeName = "DateTime")]
        [DisplayFormat(DataFormatString = "{0:MM'/'dd'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Modify By")]
        public string? ModifyBy { get; set; } = string.Empty;
        [Column(TypeName = "DateTime")]
        [DisplayFormat(DataFormatString = "{0:MM'/'dd'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ModifyDate { get; set; }

        [DisplayName("Delete By")]
        public string? DeleteBy { get; set; } = string.Empty;

        [Column(TypeName = "DateTime")]
        [DisplayFormat(DataFormatString = "{0:MM'/'dd'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DeleteDate { get; set; }
    }
}
