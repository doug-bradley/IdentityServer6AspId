using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetLookup
    {
        public AspNetLookup()
        {
            AspNetLookupHistory = new HashSet<AspNetLookupHistory>();
            AspNetLookupItem = new HashSet<AspNetLookupItem>();
            AspNetLookupItemHistory = new HashSet<AspNetLookupItemHistory>();
        }
        [Key]
        public long LookupId { get; set; }
        public string Name { get; set; }
        public string Custom1Header { get; set; }
        public long? BuiltinId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<AspNetLookupHistory> AspNetLookupHistory { get; set; }
        public virtual ICollection<AspNetLookupItem> AspNetLookupItem { get; set; }
        public virtual ICollection<AspNetLookupItemHistory> AspNetLookupItemHistory { get; set; }
    }
}
