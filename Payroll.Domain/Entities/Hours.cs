using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public class Hours
    {
        public Hours()
        {

        }
        public decimal Normal { get; set; }
        public decimal TimeOneThird { get; set; }
        public decimal TimeOneHalf { get; set; }
        public decimal TimeDouble { get; set; }
        public decimal EffectiveHours { get; set; }

        public Hours GetHours(List<TimeSheetItem> items)
        {
            decimal normal = 0;
            decimal TimeOneThird = 0;
            decimal TimeOneHalf = 0;
            decimal TimeDouble = 0;
            foreach (var item in items)
            {
                if (item.TimeIn.DayOfWeek != DayOfWeek.Saturday && item.TimeIn.DayOfWeek != DayOfWeek.Sunday)
                {
                    decimal totalTime = Convert.ToDecimal((item.TimeOut - item.TimeIn).TotalHours);
                    if (totalTime > 9.0M)
                    {
                        normal += 9;
                        TimeOneThird += totalTime - 9;
                    }
                    else
                    {
                        normal += totalTime;
                    }
                }
                else
                {
                    decimal totalTime = Convert.ToDecimal((item.TimeOut - item.TimeIn).TotalHours);
                    if (item.TimeIn.DayOfWeek == DayOfWeek.Saturday)
                    {
                        if (totalTime > 5)
                        {
                            TimeOneHalf += 5;
                            TimeDouble += totalTime - 5;
                        }
                        else
                        {
                            TimeOneHalf += totalTime;
                        }
                    }
                    else
                    {
                        TimeDouble += totalTime;
                    }
                }
            }
            decimal Effective = normal + (1.333333333333333333333333M * TimeOneThird) + (1.5M * TimeOneHalf) + (2 * TimeDouble);
            Hours hours = new Hours()
            {
                Normal = normal,
                TimeOneThird = TimeOneThird,
                TimeOneHalf = TimeOneHalf,
                TimeDouble = TimeDouble,
                EffectiveHours = Effective
            };
            return hours;
        }
    }
}
