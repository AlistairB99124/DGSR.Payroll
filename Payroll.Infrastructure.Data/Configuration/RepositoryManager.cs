using Payroll.Domain.Interfaces.Infrastructure;
using Payroll.Infrastructure.Data.Context;
using System.Web;

namespace Payroll.Infrastructure.Data.Configuration
{
    public class RepositoryManager:IRepositoryManager
    {
        public const string ContextHttp = "ContextHttp";

        public ContextBank Context
        {
            get
            {
                if (HttpContext.Current.Items[ContextHttp] == null)
                    HttpContext.Current.Items[ContextHttp] = new ContextBank();
                return HttpContext.Current.Items[ContextHttp] as ContextBank;
            }
        }

        public void Finalise()
        {
            if (HttpContext.Current.Items[ContextHttp] != null)
                (HttpContext.Current.Items[ContextHttp] as ContextBank).Dispose();
        }
    }
}
