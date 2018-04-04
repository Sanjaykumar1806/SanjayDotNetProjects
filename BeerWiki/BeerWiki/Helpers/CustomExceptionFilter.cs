using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerWiki.Helpers
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is NullReferenceException)
            {
                filterContext.Result = new RedirectResult("~/Shared/Error");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}