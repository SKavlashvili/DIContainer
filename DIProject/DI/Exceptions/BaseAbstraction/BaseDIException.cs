namespace DI.Exceptions
{
    public abstract class BaseDIException : Exception
    {
        public BaseDIException(string message) : base(message) { }

    }
}
