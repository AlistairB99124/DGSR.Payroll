using Payroll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Data.Configuration
{
    public class ConfigurationEF
    {
        public class ClerkMap : EntityTypeConfiguration<Clerk>
        {
            public ClerkMap()
            {
                this.HasKey(t => t.Id);

                this.ToTable("Clerk", "dbo");

                this.HasRequired(t => t.User);

            }
        }
        public class AdminMap : EntityTypeConfiguration<Admin>
        {
            public AdminMap()
            {
                this.HasKey(t => t.Id);

                this.HasRequired(t => t.User);

                this.ToTable("Admin", "dbo");                
            }
        }
        public class EmployeeMap : EntityTypeConfiguration<Employee>
        {
            public EmployeeMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.Active).IsRequired();

                this.Property(t => t.BankId).IsRequired();

                this.Property(t => t.Cell).IsRequired();

                this.Property(t => t.DateOfBirth).IsRequired();

                this.Property(t => t.Email).IsOptional();

                this.Property(t => t.EmpId).IsRequired();

                this.Property(t => t.FirstName).IsRequired();

                this.Property(t => t.Gender).IsRequired();

                this.Property(t => t.GradedTaxNo).IsRequired();

                this.Property(t => t.LastName).IsRequired();

                this.Property(t => t.LocationId).IsRequired();

                this.Property(t => t.NationalId).IsRequired();

                this.Property(t => t.Nationality).IsRequired();

                this.Property(t => t.Race).IsRequired();

                this.Property(t => t.DateEmployed).IsRequired();

                this.ToTable("Employee", "dbo");                           

            }

        }

        public class TimeSheetMap : EntityTypeConfiguration<TimeSheet>
        {
            public TimeSheetMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.Day).IsRequired();                

                this.ToTable("TimeSheet", "dbo");
            }
        }

        public class TimeSheetItemMap : EntityTypeConfiguration<TimeSheetItem>
        {
            public TimeSheetItemMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.EmployeeId).IsRequired();

                this.Property(t => t.TimeIn).IsRequired();

                this.Property(t => t.TimeOut).IsRequired();

                this.HasOptional(t => t.TimeSheet);

                this.HasRequired(t => t.Employee);

                this.ToTable("TimeSheetItem", "dbo");                
            }
        }

        public class PayrollMap : EntityTypeConfiguration<Payroll.Domain.Entities.Payroll>
        {
            public PayrollMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.Month).IsRequired();

                this.Property(t => t.Year).IsRequired();

                this.ToTable("Payroll", "dbo");

            }
        }

        public class PayrollItemMap : EntityTypeConfiguration<PayrollItem>
        {
            public PayrollItemMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.Employee_Id).IsRequired();

                this.Property(t => t.Advance).IsOptional();

                this.Property(t => t.AmountGross).IsRequired();

                this.Property(t => t.AmountTaxable).IsRequired();

                this.Property(t => t.Amount_Net).IsRequired();

                this.Property(t => t.Amount_Paid).IsRequired();

                this.Property(t => t.Bonus).IsOptional();

                this.Property(t => t.DoubleTimeHours).IsOptional();

                this.Property(t => t.EffectiveHours).IsRequired();

                this.Property(t => t.Employee_Id).IsRequired();

                this.Property(t => t.General_Tax).IsOptional();

                this.Property(t => t.Gross_Wage).IsRequired();

                this.Property(t => t.Loan).IsOptional();

                this.Property(t => t.NormalHours).IsRequired();

                this.Property(t => t.PAYE).IsRequired();

                this.Property(t => t.PayrollId).IsRequired();

                this.Property(t => t.SNPF_Employee).IsOptional();

                this.Property(t => t.TimeOneHalfHours).IsOptional();

                this.Property(t => t.SNPF_Employer).IsOptional();

                this.Property(t => t.TimeOneThirdHours).IsOptional();

                this.HasRequired(t => t.Employee);

                this.HasRequired(t => t.Payroll);

                this.ToTable("PayrollItem", "dbo");
            }
        }

        public class ContractMap : EntityTypeConfiguration<Contract>
        {
            public ContractMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.EmployeeId).IsRequired();

                this.Property(t => t.JobId).IsRequired();

                this.Property(t => t.Commencement).IsRequired();

                this.Property(t => t.Wage).IsOptional();

                this.Property(t => t.Salary).IsOptional();

                this.Property(t => t.Salaried).IsRequired();

                this.Property(t => t.Waged).IsRequired();

                this.Property(t => t.Permanent).IsRequired();

                this.Property(t => t.Contractor).IsRequired();

                this.Property(t => t.ExpiryDate).IsRequired();

                this.Property(t => t.JobId).IsRequired();

                this.ToTable("Contract", "dbo");

                this.HasRequired(t => t.Job);

                this.HasRequired(t => t.Employee);

            }
        }

        public class BankMap : EntityTypeConfiguration<Bank>
        {
            public BankMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.Account_No).IsRequired();

                this.Property(t => t.Branch_Code).IsRequired();

                this.Property(t => t.Name).IsRequired();

                this.ToTable("Bank", "dbo");

            }
        }

        public class JobMap : EntityTypeConfiguration<Job>
        {
            public JobMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.Description).IsOptional();

                this.Property(t => t.Title).IsRequired();

                this.ToTable("Job", "dbo");               

            }
        }

        public class LocationMap : EntityTypeConfiguration<Location>
        {
            public LocationMap()
            {
                this.HasKey(t => t.Id);

                this.Property(t => t.Address1).IsRequired();

                this.Property(t => t.Address2).IsRequired();

                this.Property(t => t.City).IsRequired();

                this.Property(t => t.Country).IsRequired();

                this.Property(t => t.Postal_Code).IsRequired();

                this.Property(t => t.Province).IsRequired();

                this.ToTable("Location", "dbo");

            }
        }
    }
}
