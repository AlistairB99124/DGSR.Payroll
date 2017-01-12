using Microsoft.Practices.ServiceLocation;
using Payroll.Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Services
{
    public class ServiceDomainBase
    {
        private IUnitOfWork _unitOfWork;

        public virtual void StartTransaction()
        {
            _unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();
            _unitOfWork.Start();
        }

        public virtual void PersistTransaction()
        {
            _unitOfWork.Persist();
        }
    }
}
