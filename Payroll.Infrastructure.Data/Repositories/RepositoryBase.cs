using Microsoft.Practices.ServiceLocation;
using Payroll.Domain.Interfaces.Infrastructure;
using Payroll.Domain.Interfaces.Repositories;
using Payroll.Infrastructure.Data.Configuration;
using Payroll.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ContextBank _context;

        public RepositoryBase()
        {
            var manager = (RepositoryManager)ServiceLocator.Current.GetInstance<IRepositoryManager>();
            _context = manager.Context;
        }

        public void Edit(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
        }

        public IList<TEntity> RecoverAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity RecoverById(int Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }

        public void Remove(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
        }

        public void Remove(int Id)
        {
            TEntity obj = RecoverById(Id);
            Remove(obj);
        }
    }
}
