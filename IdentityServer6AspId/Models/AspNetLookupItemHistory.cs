using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetLookupItemHistory
    {
        [Key]
        public long LookupItemHistoryId { get; set; }
        public long LookupItemId { get; set; }
        public long LookupId { get; set; }
        public string Value { get; set; }
        public string Custom1 { get; set; }
        public bool? IsActive { get; set; }
        public DateTime AuditDate { get; set; }
        public string CurrentUser { get; set; }
        public string AuditAction { get; set; }

        public virtual AspNetLookup AspNetLookup { get; set; }
        public virtual AspNetLookupItem AspNetLookupItem { get; set; }
    }
}
