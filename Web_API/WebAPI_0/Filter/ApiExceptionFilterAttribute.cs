using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Shared.Exception;
using Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_0.Filter
{
    /// <summary>
    /// I filtri permettono di gestire gli errori che avvengono all'interno del codice
    /// Per gestire errori non gestiti all'interno del codice (routing o framework) utilizzare l'handler
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        //private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        /// <summary>
        ///     ctor
        /// </summary>
        public ApiExceptionFilterAttribute()
        {
            
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BusinessLogicValidationException), HandleInvalidModelStateException },
            };
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            //_logger.LogError(context.Exception, "Handling exception:");

            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            ApiResponseModel details = new ApiResponseModel()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                IsSuccess = false,
                Messages = new string[1]{ context.Exception.Message }

                //Status = StatusCodes.Status500InternalServerError
            };

            context.Result = new ObjectResult(details);

            context.ExceptionHandled = true;

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }      
    }
}
