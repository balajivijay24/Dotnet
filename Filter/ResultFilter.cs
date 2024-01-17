using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityManagement.Filters
{
public class CustomResultFilterAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        // Code to be executed before the action result executes
         Console.WriteLine("Result  Action executing");
        
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        // Code to be executed after the action result executes
        Console.WriteLine("Result  Action executed");
          string script = "<script>alert('Please Enter Proper User Id And Password');</script>";
            context.HttpContext.Response.WriteAsync(script);
         
    }
}
}