using LeaseManager.Core.Domain.Entities;
using LeaseManager.Core.FrameWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Infrastucture.Interfaces
{
    public interface ITenantRepository : IGenericRepository<Tenant>
    {
    }
}
