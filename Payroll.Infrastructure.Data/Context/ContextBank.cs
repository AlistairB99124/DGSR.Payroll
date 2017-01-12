using Microsoft.AspNet.Identity.EntityFramework;
using Payroll.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using static Payroll.Infrastructure.Data.Configuration.ConfigurationEF;

namespace Payroll.Infrastructure.Data.Context
{
    public class ContextBank: IdentityDbContext<User>
    {
        public ContextBank()
            : base("DGSR", throwIfV1Schema: false)
        {
        }

        public static ContextBank Create()
        {
            return new ContextBank();
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Clerk> Clerks { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Payroll.Domain.Entities.Payroll> Payrolls { get; set; }
        public DbSet<PayrollItem> PayrollItems { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeSheetItem> TimeSheetItems { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));

            modelBuilder.Properties<string>().Where(p => p.Name.Contains("Description")).Configure(p => p.HasMaxLength(400));

            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new TimeSheetMap());
            modelBuilder.Configurations.Add(new PayrollMap());
            modelBuilder.Configurations.Add(new PayrollItemMap());
            modelBuilder.Configurations.Add(new BankMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new ContractMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new ClerkMap());
            modelBuilder.Configurations.Add(new AdminMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
