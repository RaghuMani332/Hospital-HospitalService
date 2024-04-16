using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace Hospital_HospitalService.ExceptionHandler
{
    public class ApplicationExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is Exception) 
            {
                context.ModelState.AddModelError("EXCEPTION",context.Exception.Message);
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = context.Exception.GetType().ToString(),
                    Detail = context.Exception.Message // Include stack trace for debugging
                };
                context.Result= new ObjectResult(problemDetails)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}
