using Payroll.Domain.Interfaces.Domain;
using System;
using Payroll.Domain.Interfaces.Repositories;
using Payroll.Domain.Entities;
using System.Collections.Generic;

namespace Payroll.Domain.Services
{
    public class PayrollServiceDomain : ServiceDomainBase, IPayrollServiceDomain
    {
        private readonly IPayrollRepository _payrollRepository;
        public PayrollServiceDomain(IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }

        public Payroll.Domain.Entities.Payroll CommitToPayroll(DateTime Date)
        {
            try
            {
                StartTransaction();
                var payroll = _payrollRepository.CommitToPayroll(Date);
                PersistTransaction();
                return payroll;
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public List<PayrollItem> GetItemsByPayrollId(int ID)
        {
            try
            {
                StartTransaction();
                var payrollItems = _payrollRepository.GetItemsByPayrollId(ID);
                PersistTransaction();
                return payrollItems;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
       
    }
}
