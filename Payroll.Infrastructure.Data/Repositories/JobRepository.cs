using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;

namespace Payroll.Infrastructure.Data.Repositories
{
    public class JobRepository:RepositoryBase<Job>,IJobRepository
    {
    }
}
