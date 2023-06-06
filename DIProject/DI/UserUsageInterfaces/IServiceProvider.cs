namespace DI
{
    public interface IServiceProvider
    {
        T GetService<T>();
    }
}
