using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Web.ExceptionsPresenter
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        readonly IDictionary<Type, IExceptionHandler> ExceptionHandlers;

        public ApiExceptionFilterAttribute(IDictionary<Type, IExceptionHandler> exceptionHandlers) =>
            ExceptionHandlers = exceptionHandlers;
        public override void OnException(ExceptionContext context)
        {
            Type ExceptionType = context.Exception.GetType();
            if (ExceptionHandlers.ContainsKey(ExceptionType))
            {
                ExceptionHandlers[ExceptionType].Handle(context);
            }
            else
            {
                new ExceptionHandlerBase().SetResult(context, StatusCodes.Status500InternalServerError, context, " Ha ocurrido un error al procesar la respuesta.", "");
            }
            base.OnException(context);
        }
    }
}
