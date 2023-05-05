using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiCamaras.Filtros
{
    public class FiltroDeAccion : IActionFilter
    {
        private readonly ILogger<FiltroDeAccion> logger;

        public FiltroDeAccion(ILogger<FiltroDeAccion> logger) 
        {
            this.logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("Después de ejecutar la acción");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {            
            logger.LogInformation("Antes de ejecutar la acción");
        }
    }
}
