using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityManagement.Filters{
    public class ExceptionLogFilter : ExceptionFilterAttribute,IExceptionFilter{
        public override void OnException(ExceptionContext context)
        {
            var exceptionMessage=context.Exception.Message;
            var StackTrace=context.Exception.StackTrace;
            var controllerName=context.RouteData.Values["Controller"].ToString();
            var actionName=context.RouteData.Values["action"].ToString();
             var Message="Date :"+DateTime.Now.ToString("dd-MM-yyyy hh:mm tt")+"Error Message:"+exceptionMessage+Environment.NewLine+"Stack Trace:"+StackTrace;
             context.Result = new RedirectResult("/Error/Index");
           base.OnException(context);
        }
    }
}