using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetLookupItem
    {
        public AspNetLookupItem()
        {
            AspNetLookupItemHistory = new HashSet<AspNetLookupItemHistory>();
            AspNetTenantConfig = new HashSet<AspNetTenantConfig>();
            AspNetTenantConfigHistory = new HashSet<AspNetTenantConfigHistory>();
        }
        [Key]
        public long LookupItemId { get; set; }
        public long LookupId { get; set; }
        public string Value { get; set; }
        public string Custom1 { get; set; }
        public bool? IsActive { get; set; }

        public virtual AspNetLookup AspNetLookup { get; set; }
        public virtual ICollection<AspNetLookupItemHistory> AspNetLookupItemHistory { get; set; }
        public virtual ICollection<AspNetTenantConfig> AspNetTenantConfig { get; set; }
        public virtual ICollection<AspNetTenantConfigHistory> AspNetTenantConfigHistory { get; set; }
    }
}
