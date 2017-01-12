using CommonServiceLocator.SimpleInjectorAdapter;
using Microsoft.Practices.ServiceLocation;
using Payroll.Domain.Interfaces.Domain;
using Payroll.Domain.Interfaces.Infrastructure;
using Payroll.Domain.Interfaces.Repositories;
using Payroll.Domain.Services;
using Payroll.Infrastructure.Data.Configuration;
using Payroll.Infrastructure.Data.Repositories;
using SimpleInjector;

namespace Payroll.Infrastructure.IoC
{
    public class Bindings
    {
        public static void Start(Container container)
        {            
            //Infrastructure
            container.Register<IRepositoryManager, RepositoryManager>();
            container.Register<IUnitOfWork, UnitOfWorkEF>();
            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>), Lifestyle.Scoped);
            container.Register(typeof(IUserRepository), typeof(UserRepository), Lifestyle.Scoped);
            container.Register(typeof(IAdminRepository), typeof(AdminRepository), Lifestyle.Scoped);
            container.Register(typeof(IClerkRepository), typeof(ClerkRepository), Lifestyle.Scoped);
            container.Register(typeof(IEmployeeRepository), typeof(EmployeeRepository), Lifestyle.Scoped);
            container.Register(typeof(ILocationRepository), typeof(LocationRepository), Lifestyle.Scoped);
            container.Register(typeof(IContractRepository), typeof(ContractRepository), Lifestyle.Scoped);
            container.Register(typeof(IJobRepository), typeof(JobRepository), Lifestyle.Scoped);
            container.Register(typeof(IBankRepository), typeof(BankRepository), Lifestyle.Scoped);
            container.Register(typeof(IPayrollRepository), typeof(PayrollRepository), Lifestyle.Scoped);
            container.Register(typeof(IPayrollItemRepository), typeof(PayrollItemRepository), Lifestyle.Scoped);
            container.Register(typeof(ITimeSheetItemRepository), typeof(TimeSheetItemRepository), Lifestyle.Scoped);
            container.Register(typeof(ITimeSheetRepository), typeof(TimeSheetRepository), Lifestyle.Scoped);

            //Domain
            container.Register(typeof(IUserServiceDomain), typeof(UserServiceDomain), Lifestyle.Scoped);
            container.Register(typeof(ITimeSheetServiceDomain), typeof(TimeSheetServiceDomain), Lifestyle.Scoped);
            container.Register(typeof(IPayrollServiceDomain), typeof(PayrollServiceDomain), Lifestyle.Scoped);
            container.Register(typeof(IPayrollItemServiceDomain), typeof(PayrollItemServiceDomain), Lifestyle.Scoped);
            container.Register(typeof(IReportServiceDomain), typeof(ReportServiceDomain), Lifestyle.Scoped);
            //Application
            //To Do

            //Service Locator
            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocatorAdapter(container));

        }
    }
}
