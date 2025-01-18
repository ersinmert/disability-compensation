using DisabilityCompensation.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class AdminAttribute : TypeFilterAttribute
    {
        public AdminAttribute() : base(typeof(AdminFilter))
        {
        }
    }
}
