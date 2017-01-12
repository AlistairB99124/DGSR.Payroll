using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public Admin Register(Admin admin)
        {
            return _context.Admins.Add(admin);
        }
    }
}
