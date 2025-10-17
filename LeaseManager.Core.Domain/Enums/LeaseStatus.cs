using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Enums
{
    public enum LeaseStatus
    {
        Pending = 1,
        Active = 2,
        Terminated = 3,
        Expired = 4
    }
}
