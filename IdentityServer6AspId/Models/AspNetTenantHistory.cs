using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetTenantHistory
    {
        [Key]
        public long TenantHistoryId { get; set; }
        public long TenantId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? CancelledDate { get; set; }
        public bool? ForceUseAad { get; set; }
        public Guid? FxTenantId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime AuditDate { get; set; }
        public string CurrentUser { get; set; }
        public string AuditAction { get; set; }

        public virtual AspNetTenant AspNetTenant { get; set; }
    }
}
