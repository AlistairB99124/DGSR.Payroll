using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IList<TEntity> RecoverAll();
        TEntity RecoverById(int Id);
        void Insert(TEntity obj);
        void Edit(TEntity obj);
        void Remove(TEntity obj);
        void Remove(int Id);
    }
}
