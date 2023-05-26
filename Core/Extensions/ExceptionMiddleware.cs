using Core.Utilities.Messages;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceStack.Messaging;
using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace Core.Extensions
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
            catch (CustomException e)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)e.Status;

                string message = null;
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
                await httpContext.Response.WriteAsync(message);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }


        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _ = e.Message;
            string message;
            if (e.GetType() == typeof(ValidationException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message)); ;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(ApplicationException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message)); ;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(UnauthorizedAccessException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message)); ;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (e.GetType() == typeof(SecurityException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message)); ;
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else if (e.GetType() == typeof(NotSupportedException))
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message)); ;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                message = JsonConvert.SerializeObject(new ErrorResult(e.Message));
            }

            await httpContext.Response.WriteAsync(message);
        }
    }
}