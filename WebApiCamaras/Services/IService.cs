namespace WebApiCamaras.Services
{
    // Principio solid: Es mejor que nuestros controladores dependan de abstracciones y no de tipos concretos
    // Esto da el máximo grado de flexibilidad y genera un acoplamiento bajo
    // El aplication DbContext es unan excepción de esto

    // *Servicio*
    // Es la resolución de una dependencia configurada en el sistema de inyección de dependencias

    // *Sistema de inyección de dependencias*
    // se encarga de instanciar correctamente los servicios con todas sus configuraciones
    public interface IService
    {
        Guid GetScoped();
        Guid GetSingleton();
        Guid GetTransient();
        void RealizarTarea();
    }

    public class ServiceA : IService
    {
        private readonly ILogger<ServiceA> logger;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;

        public ServiceA(
            ILogger<ServiceA> logger, 
            ServiceTransient serviceTransient,
            ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton) 
        {
            this.logger = logger;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
        }

        public Guid GetTransient() { return serviceTransient.Guid; }
        public Guid GetScoped() { return serviceScoped.Guid; }
        public Guid GetSingleton () { return serviceSingleton.Guid; }

        public void RealizarTarea()
        {
        }
    }

    public class ServiceB : IService
    {
        public Guid GetScoped()
        {
            throw new NotImplementedException();
        }

        public Guid GetSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid GetTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea()
        {
            throw new NotImplementedException();
        }
    }
    public class ServiceTransient
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServiceScoped
    {
        public Guid Guid = Guid.NewGuid();
    }
    public class ServiceSingleton
    {
        public Guid Guid = Guid.NewGuid();
    }
}
