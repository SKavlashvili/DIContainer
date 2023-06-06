namespace DI.Exceptions
{
    public class NotRegisteredServiceException : BaseDIException
    {
        public NotRegisteredServiceException(string serviceName) :
            base($"{serviceName} is not registered") { }
    }
}
