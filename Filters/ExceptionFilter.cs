using Microsoft.AspNetCore.Mvc.Filters;

namespace StoreAppAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public ExceptionFilter()
        {
        }

        void IExceptionFilter.OnException(ExceptionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
