namespace DI
{
    public interface IServiceCollection
    {
        void AddSingleton<T, TImplementation>() where TImplementation : T;
        void AddTransient<T, TImplementation>() where TImplementation : T;
        void AddScoped<T, TImplementation>() where TImplementation : T;

        IServiceProvider GenerateProvider();
    }
}
