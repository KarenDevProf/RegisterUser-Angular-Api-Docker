using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using RegisterUser.BusinessLayer.Exceptions;
using RegisterUser.BusinessLayer.Resources;
using RegisterUser.API.Models;
using RegisterUser.BusinessLayer.Enums;

namespace RegisterUser.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<ResponseCodes> _localize;
        public ErrorHandlingMiddleware(RequestDelegate next, IStringLocalizer<ResponseCodes> localize)
        {
            _next = next;
            _localize = localize;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ResponseObjectModel<object> responseObject = new ResponseObjectModel<object>
            {
                HasError = true,
                Data = null
            };

            if (ex is BaseException shopException)
            {
                responseObject.Message = string.Format(_localize[shopException.ErrorMessageResourceKey], shopException.ErrorType);
                responseObject.ErrorCode = ((int)Enum.Parse(typeof(ErrorCodes), shopException.ErrorMessageResourceKey)).ToString();
            }
            else
            {
                responseObject.Message = $"{_localize["InternalServerError"]}:{ex.Message}";
                responseObject.ErrorCode = ((int)Enum.Parse(typeof(ErrorCodes), "InternalServerError")).ToString();
            }

            string result = JsonConvert.SerializeObject(responseObject, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}
