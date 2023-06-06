namespace DI.Exceptions
{
    public class ServiceAlreadyRegisteredException : BaseDIException
    {
        public ServiceAlreadyRegisteredException(LifeTime lifeTime) 
            : base($"This service is already registered as {lifeTime.ToString()}") { }
    }
}
