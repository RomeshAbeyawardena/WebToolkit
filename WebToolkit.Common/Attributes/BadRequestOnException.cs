using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebToolkit.Common.Attributes
{
    public class BadRequestOnExceptionAttribute : Attribute, IExceptionFilter
    {
        private readonly Exception[] _exceptions;

        public BadRequestOnExceptionAttribute(params Exception[] exceptions)
        {
            _exceptions = exceptions;
        }

        public void OnException(ExceptionContext context)
        {
            foreach (var exception in _exceptions)
            {
                if (context.Exception.GetType() != exception.GetType())
                    continue;

                context.Result = new BadRequestObjectResult(context.Exception);
            }
        }
    }
}