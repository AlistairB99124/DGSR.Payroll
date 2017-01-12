using Payroll.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;

namespace Payroll.Domain.Services
{
    public class PayrollItemServiceDomain: ServiceDomainBase, IPayrollItemServiceDomain
    {
        private readonly IPayrollItemRepository _payrollItemRepository;

        public PayrollItemServiceDomain(IPayrollItemRepository payrollItemRepository)
        {
            _payrollItemRepository = payrollItemRepository;
        }

        public PayrollItem GetItem(int ID)
        {
            return _payrollItemRepository.RecoverById(ID);
        }

        public void UpdateItem(PayrollItem payrollItem)
        {
            try
            {
                StartTransaction();
                _payrollItemRepository.Edit(payrollItem);
                PersistTransaction();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        
    }
}
