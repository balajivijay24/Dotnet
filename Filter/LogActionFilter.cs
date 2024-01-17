using System;
using Microsoft.AspNetCore.Mvc.Filters;
namespace IdentityManagement.Filters
{
public class LogActionFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Code to execute before the action method is called
        Console.WriteLine("Executing action: " + context.ActionDescriptor.DisplayName);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        // Code to execute after the action method has been called
        Console.WriteLine("Action executed");
    }
}


}