using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Domain.Entities
{
    public class TaxTable
    {

        public TaxTable()
        {

        }        
        public List<TaxBand> GetEnumerator(int Year)
        {
            List <TaxBand> request = null;
            switch (Year)
            {
                case 2017:
                    var taxTable = new List<TaxBand>
                    {
                        new TaxBand { MarginalRate = 0.2, BaseAmount = 0, Threshold = 0, Limit = 100000 },
                        new TaxBand { MarginalRate = 0.25, BaseAmount = 20000, Threshold = 100001, Limit = 150000 },
                        new TaxBand { MarginalRate = 0.30, BaseAmount = 32500, Threshold = 150001, Limit = 200000 },
                        new TaxBand { MarginalRate = 0.33, BaseAmount = 47500, Threshold = 200001, Limit = double.PositiveInfinity }
                    };
                    request =  taxTable;
                    break;
                default:
                    request = null;
                    break;
            }
            return request;
        }
    }   
}
