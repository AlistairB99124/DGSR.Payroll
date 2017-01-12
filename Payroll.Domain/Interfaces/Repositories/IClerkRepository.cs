﻿using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Interfaces.Repositories
{
    public interface IClerkRepository:IRepositoryBase<Clerk>
    {
        Clerk Register(Clerk clerk);
    }
}