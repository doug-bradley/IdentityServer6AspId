using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetTenant
    {
        public AspNetTenant()
        {
            AspNetTenantConfig = new HashSet<AspNetTenantConfig>();
            AspNetTenantConfigHistory = new HashSet<AspNetTenantConfigHistory>();
            AspNetTenantHistory = new HashSet<AspNetTenantHistory>();
            AspNetTenantUser = new HashSet<AspNetTenantUser>();
        }
        [Key]
        public long TenantId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CancelledDate { get; set; }
        public bool ForceUseAad { get; set; }
        public Guid FxTenantId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<AspNetTenantConfig> AspNetTenantConfig { get; set; }
        public virtual ICollection<AspNetTenantConfigHistory> AspNetTenantConfigHistory { get; set; }
        public virtual ICollection<AspNetTenantHistory> AspNetTenantHistory { get; set; }
        public virtual ICollection<AspNetTenantUser> AspNetTenantUser { get; set; }
    }
}