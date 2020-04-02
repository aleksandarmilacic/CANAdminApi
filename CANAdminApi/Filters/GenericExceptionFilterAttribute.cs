using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace CANAdminApi.Filters
{
    public class GenericExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute, IFilterMetadata
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (!(context.Exception is NotImplementedException))
            {
                // TODO LOG EXCEPTION nicer

                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                #if DEBUG
                context.Response.Content = new StringContent(context.Exception.ToString());
                #endif
            }
        }
    }
}
