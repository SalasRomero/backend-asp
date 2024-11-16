using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_asp.Filtros
{
    public class FiltroDeExcepcion: ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroDeExcepcion> _logger;
        public FiltroDeExcepcion(ILogger<FiltroDeExcepcion> logger)
        {
            this._logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            this._logger.LogError(context.Exception,context.Exception.Message);
            base.OnException(context); // base permite llamar el metodo de la clase ExceptionFilterAttribute
        }
    }
}
