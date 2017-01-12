using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public partial class PayrollItem
    {
        public int Id { get; set; }
        public int PayrollId { get; set; }
        [ForeignKey("Employee")]
        public int Employee_Id { get; set; }
        public decimal NormalHours { get; set; }
        public decimal TimeOneThirdHours { get; set; }
        public decimal TimeOneHalfHours { get; set; }
        public decimal DoubleTimeHours { get; set; }
        public decimal EffectiveHours { get; set; }
        public decimal AmountGross { get; set; }
        public decimal Bonus { get; set; }
        public decimal Gross_Wage
        {
            get
            {
                return GetGross_Wage();
            }
            set
            {
                var grossW = GetGross_Wage();
                grossW = value;
            }
        }
        private decimal GetGross_Wage() { return AmountGross + Bonus; }
        public decimal SNPF_Employee
        {
            get
            {
                return GetSNPF_Employee();
            }
            set
            {
                var snpf = GetSNPF_Employee();
                snpf = value;
            }
        }
        private decimal GetSNPF_Employee()
        {
            if (Employee == null)
            {
                return 0;
            }
            else
            {
                if (Employee.Nationality == "SWZ")
                {
                    if (this.Gross_Wage >= 2100)
                    {
                        return 105;
                    }
                    else
                    {
                        return 0.05M * this.Gross_Wage;
                    }
                }
                else
                {
                    return 0;
                }
            }
           
        }
        public decimal SNPF_Employer
        {
            get
            {
                return this.SNPF_Employee;
            }
            set
            {
                this.SNPF_Employee = value;
            }
        }

        public decimal AmountTaxable
        {
            get
            {
                return GetAmountTaxable();
            }
            set
            {
                var aTax = GetAmountTaxable();
                aTax = value;
            }
        }
        private decimal GetAmountTaxable()
        {

            return this.AmountGross - this.SNPF_Employee;

        }

        public decimal General_Tax
        {
            get
            {
                return GetGeneral_Tax();
            }
            set
            {
                var gTax = GetGeneral_Tax();
                gTax = value;
            }
        }
        private decimal GetGeneral_Tax()
        {
            return 18.00M / 12.00M;
        }
        public decimal PAYE
        {
            get
            {
                return GetPAYE();
            }
            set
            {
                var paye = GetPAYE();
                paye = value;
            }
        }
        private decimal GetPAYE()
        {
            TaxTable taxTable = new TaxTable();
            double annualTaxable = Convert.ToDouble((this.AmountTaxable * 12)+this.Bonus);
            double grossIncomeTax = 0;
            foreach (var taxBand in taxTable.GetEnumerator(2017))
            {
                if ((annualTaxable >= taxBand.Threshold) && (annualTaxable <= taxBand.Limit))
                    grossIncomeTax = taxBand.BaseAmount + (taxBand.MarginalRate * (annualTaxable - taxBand.Threshold));
            }
            double rebate = 8200;
            double monthlyTaxRate = (grossIncomeTax - rebate) / 12;
            if (monthlyTaxRate <= 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(monthlyTaxRate);
            }
        }
        public decimal Amount_Net
        {
            get
            {
                return GetAmount_Net();
            }
            set
            {
                var amtNet = GetAmount_Net();
                amtNet = value;
            }
        }
        private decimal GetAmount_Net()
        {
            return this.Gross_Wage - this.PAYE - this.SNPF_Employee - this.General_Tax;
        }
        public decimal Loan { get; set; }
        public decimal Advance { get; set; }
        public decimal Amount_Paid
        {
            get
            {
                return GetAmount_Paid();
            }
            set
            {
                var amtPaid = GetAmount_Paid(); amtPaid = value;
            }
        }
        private decimal GetAmount_Paid() { return Amount_Net - Loan + Advance; }

        public virtual Employee Employee { get; set; }
        public virtual Payroll Payroll { get; set; }


        public PayrollItem()
        {

        }

        public PayrollItem(Employee emp, Payroll pay, Hours hours)
        {
            Contract contract = emp.Contracts.Where(x => x.ExpiryDate > DateTime.Now).FirstOrDefault();
            this.Employee_Id = emp.Id;
            this.Employee = emp;
            this.PayrollId = pay.Id;
            this.Payroll = pay;
            this.NormalHours = hours.Normal;
            this.TimeOneThirdHours = hours.TimeOneThird;
            this.TimeOneHalfHours = hours.TimeOneHalf;
            this.DoubleTimeHours = hours.TimeDouble;
            this.EffectiveHours = hours.EffectiveHours;
            if (contract.Waged)
            {
                this.AmountGross = this.EffectiveHours * contract.Wage;
            }
            else
            {
                this.AmountGross = contract.Salary;
            }
        }
        public PayrollItem(int itemId, Employee emp, Payroll pay, Hours hours, decimal bonus, decimal loan, decimal advance)
        {
            this.Id = itemId;
            Contract contract = emp.Contracts.Where(x => x.ExpiryDate > DateTime.Now).FirstOrDefault();
            this.Employee_Id = emp.Id;
            this.Employee = emp;
            this.PayrollId = pay.Id;
            this.Payroll = pay;
            this.NormalHours = hours.Normal;
            this.TimeOneThirdHours = hours.TimeOneThird;
            this.TimeOneHalfHours = hours.TimeOneHalf;
            this.DoubleTimeHours = hours.TimeDouble;
            this.EffectiveHours = hours.EffectiveHours;
            if (contract.Waged)
            {
                this.AmountGross = this.EffectiveHours * contract.Wage;
            }
            else
            {
                this.AmountGross = contract.Salary;
            }
            this.Bonus = bonus;
            this.Loan = loan;
            this.Advance = advance;
        }
    }
}
