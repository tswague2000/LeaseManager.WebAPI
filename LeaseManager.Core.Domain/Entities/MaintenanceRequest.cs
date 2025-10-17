using LeaseManager.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Entities
{
    public class MaintenanceRequest
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public string Description { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public MaintenanceStatus Status { get; set; } = MaintenanceStatus.Pending;
    }
}
