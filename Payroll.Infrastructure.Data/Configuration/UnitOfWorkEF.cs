using Payroll.Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using System.Threading.Tasks;
using Payroll.Infrastructure.Data.Context;

namespace Payroll.Infrastructure.Data.Configuration
{
    public class UnitOfWorkEF:IUnitOfWork
    {
        private ContextBank _context;
        //private ApplicationDbContext _contextIdentity;
        public void Persist()
        {
            _context.SaveChanges();
        }

        public void Start()
        {
            var manager = ServiceLocator.Current.GetInstance<IRepositoryManager>() as RepositoryManager;
            _context = manager.Context;
        }
    }
}
