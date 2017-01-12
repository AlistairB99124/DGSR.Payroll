using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface IAdminRepository:IRepositoryBase<Admin>
    {
        Admin Register(Admin admin);
    }
}
