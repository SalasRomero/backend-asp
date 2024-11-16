using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_asp.Filtros
{
    public class MiFiltroDeAccion : IActionFilter
    {

        private readonly ILogger<MiFiltroDeAccion> _logger;
        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger) {
            _logger = logger;
        }
      
        public void OnActionExecuting(ActionExecutingContext context)
        {
            this._logger.LogInformation("Antes de ejecutar la acción");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            this._logger.LogInformation("Despues de ejecutar la acción");

        }


    }
}
