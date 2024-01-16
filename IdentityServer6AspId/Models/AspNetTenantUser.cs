using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetTenantUser
    {
        [Key]
        public long TenantUserId { get; set; }
        public Guid FxTenantId { get; set; }
        public Guid FxUserId { get; set; }
        public string LoginProvider { get; set; }
        public string LoginUserId { get; set; }
        public string LoginUsername { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual AspNetTenant AspNetTenant { get; set; }
    }
}
