using Challenge.Atm.Application.Exceptions;
using Challenge.Atm.Application.Wrappers;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Challenge.Atm.WebUI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate  _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new CustomResponse<string>(false, e.Message); 
                switch (e)
                {

                    case CustomValidationException ex:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = ex.Errors;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
