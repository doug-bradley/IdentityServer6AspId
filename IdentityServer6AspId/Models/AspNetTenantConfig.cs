using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetTenantConfig
    {
        public AspNetTenantConfig()
        {
            AspNetTenantConfigHistory = new HashSet<AspNetTenantConfigHistory>();
        }
        [Key]
        public long TenantConfigId { get; set; }
        public long TenantId { get; set; }
        public string SiteType { get; set; }
        public long ApplicationId { get; set; }
        public string WebsiteUrl { get; set; }
        public string ApiUrl { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string CustomLogo { get; set; }
        public string CustomStyleSheet { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsInitialized { get; set; }
        public string ExtApiUrl { get; set; }

        public virtual AspNetLookupItem Application { get; set; }
        public virtual AspNetTenant AspNetTenant { get; set; }
        public virtual ICollection<AspNetTenantConfigHistory> AspNetTenantConfigHistory { get; set; }
    }
}
