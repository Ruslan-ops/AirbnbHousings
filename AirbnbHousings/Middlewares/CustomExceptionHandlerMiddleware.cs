using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Web.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Console.Out.WriteLineAsync("@@@@@ INSIDE EXCEPTION HENDLER");
                await _next(context);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("#### HANDLED");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (ex)
            {
                case ValidationException validationEx:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { errors = validationEx.Errors.Select(e => e.ErrorMessage), message = validationEx.Message });
                    break;
                default: 
                    code = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(ex.StackTrace); //String.Empty;
                    break;
                //case EntityNotFoundException notFoundEx:
                //    code = HttpStatusCode.NotFound;
                //    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            Console.WriteLine($"%%%% Result: {result}");

            //if (result != string.Empty) 
            //{
            //    result = JsonConvert.SerializeObject(new { error = ex.Message });    
            //}

            return context.Response.WriteAsync(result);
        }
    }
}
