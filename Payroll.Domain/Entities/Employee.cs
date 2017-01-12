using System;
using System.Collections.Generic;

namespace Payroll.Domain.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            this.PayrollItems = new HashSet<PayrollItem>();
            this.TimeSheets = new HashSet<TimeSheetItem>();
            this.Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string EmpId { get; set; }
        public string NationalId { get; set; }
        public int BankId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public DateTime DateEmployed { get; set; }
        public string GradedTaxNo { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Race { get; set; }
        public int LocationId { get; set; }
        public Nullable<bool> Active { get; set; }
        
        public virtual Bank Bank { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<PayrollItem> PayrollItems { get; set; }
        public virtual ICollection<TimeSheetItem> TimeSheets { get; set; }
    }
}
