using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using YOGBIS.Common.Exceptions;

namespace YOGBIS.UI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case YogbisBusinessException businessException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            success = false,
                            message = businessException.Message
                        }));
                    }
                    else
                    {
                        context.Response.Redirect("/Home/Error");
                    }
                    break;

                case YogbisNotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            success = false,
                            message = notFoundException.Message
                        }));
                    }
                    else
                    {
                        context.Response.Redirect("/Home/Error");
                    }
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Redirect("/Home/Error");
                    break;
            }
        }
    }
}
