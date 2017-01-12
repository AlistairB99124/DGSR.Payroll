using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class ContractRepository:RepositoryBase<Contract>,IContractRepository
    {
    }
}
