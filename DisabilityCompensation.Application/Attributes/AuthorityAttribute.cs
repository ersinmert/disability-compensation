using DisabilityCompensation.Application.Filters;
using DisabilityCompensation.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DisabilityCompensation.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorityAttribute : TypeFilterAttribute
    {
        public AuthorityAttribute(Authority authority) : base(typeof(AuthorityFilter))
        {
            Arguments = new object[] { authority };
        }
    }
}
