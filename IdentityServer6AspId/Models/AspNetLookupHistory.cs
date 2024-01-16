using System.ComponentModel.DataAnnotations;

namespace IdentityServer6AspId.Models
{
    public partial class AspNetLookupHistory
    {
        [Key]
        public long LookupHistoryId { get; set; }
        public long? LookupId { get; set; }
        public string Name { get; set; }
        public string Custom1Header { get; set; }
        public long? BuiltinId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime AuditDate { get; set; }
        public string CurrentUser { get; set; }
        public string AuditAction { get; set; }

        public virtual AspNetLookup AspNetLookup { get; set; }
    }
}
