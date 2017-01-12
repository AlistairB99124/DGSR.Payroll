using Payroll.Domain.Interfaces.Domain;
using Payroll.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;

namespace Payroll.Domain.Services
{
    public class ReportServiceDomain:ServiceDomainBase,IReportServiceDomain
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IPayrollItemRepository _payrollItemRepository;

        public ReportServiceDomain(IPayrollItemRepository payrollItemRepository, IPayrollRepository payrollRepository)
        {
            _payrollItemRepository = payrollItemRepository;
            _payrollRepository = payrollRepository;
        }

        public List<PayrollItem> GetPayrollByBank(string Bank)
        {
            return _payrollItemRepository.GetPayrollByBank(Bank);
        }
    }
}
