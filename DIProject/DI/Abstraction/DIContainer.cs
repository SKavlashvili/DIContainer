using DI.Exceptions;

namespace DI
{
    public enum LifeTime
    {
        Singleton,
        Scoped,
        Transient
    }
    public class DIContainer : IServiceCollection
    {
        private Dictionary<string, LifeTime> _lifeTimeInfo;
        private Dictionary<Type, Type> T_TImplementations;
        private Dictionary<Type, object> _singletons;

        public DIContainer()
        {
            _lifeTimeInfo = new Dictionary<string, LifeTime>();
            T_TImplementations = new Dictionary<Type, Type>();
            _singletons = new Dictionary<Type, object>();
        }

        public void AddTransient<T, TImplementation>() where TImplementation : T
        {
            Type addedType = typeof(T);
            if (_lifeTimeInfo.ContainsKey(addedType.FullName)) throw new ServiceAlreadyRegisteredException(_lifeTimeInfo[addedType.ToString()]);
            _lifeTimeInfo.Add(addedType.FullName, LifeTime.Transient);
            T_TImplementations.Add(addedType, typeof(TImplementation));
        }

        public void AddSingleton<T, TImplementation>() where TImplementation : T
        {
            Type addedType = typeof(T);
            if (_lifeTimeInfo.ContainsKey(addedType.FullName)) throw new ServiceAlreadyRegisteredException(_lifeTimeInfo[addedType.ToString()]);
            _lifeTimeInfo.Add(addedType.FullName, LifeTime.Singleton);
            T_TImplementations.Add(addedType, typeof(TImplementation));
        }

        public void AddScoped<T, TImplementation>() where TImplementation : T
        {
            Type addedType = typeof(T);
            if (_lifeTimeInfo.ContainsKey(addedType.FullName)) throw new ServiceAlreadyRegisteredException(_lifeTimeInfo[addedType.ToString()]);
            _lifeTimeInfo.Add(addedType.FullName, LifeTime.Scoped);
            T_TImplementations.Add(addedType, typeof(TImplementation));
        }

        public IServiceProvider GenerateProvider()
        {
            return new DIProvider(_lifeTimeInfo,T_TImplementations,_singletons);
        }



        public static IServiceCollection BuildContainer()
        {
            return new DIContainer();
        }
    }
}
