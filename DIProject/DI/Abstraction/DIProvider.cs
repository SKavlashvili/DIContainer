using DI.Exceptions;
using System.Dynamic;
using System.Reflection;

namespace DI
{
    internal class DIProvider : DI.IServiceProvider
    {
        private Dictionary<string, LifeTime> _lifeTimeInfo;
        private Dictionary<Type, Type> T_TImplementations;
        private Dictionary<Type, object> _singletons;
        private Dictionary<Type, object> _scopedes;
        internal DIProvider(Dictionary<string,LifeTime> lifeTimeInfo, Dictionary<Type,Type> T_TImpl, Dictionary<Type, object> singletons)
        {
            _lifeTimeInfo = lifeTimeInfo;
            T_TImplementations = T_TImpl;
            _singletons = singletons;
            _scopedes = new Dictionary<Type, object>();
        }
        public T GetService<T>()
        {
            Type requestedType = typeof(T);
            if (!_lifeTimeInfo.ContainsKey(requestedType.FullName)) throw new NotRegisteredServiceException(requestedType.FullName);

            if (_lifeTimeInfo[requestedType.FullName] == LifeTime.Transient)
            {
                return (T)ResolveTransient(T_TImplementations[requestedType]);
            }
            else if(_lifeTimeInfo[requestedType.FullName] == LifeTime.Scoped)
            {
                if (_scopedes.ContainsKey(requestedType)) return (T)_scopedes[requestedType];
                else return (T)CreateScoped(T_TImplementations[requestedType], requestedType);
            }
            else //if: (_lifeTimeInfo[requestedType.FullName] == LifeTime.Singleton)
            {
                if (_singletons.ContainsKey(requestedType)) return (T)_singletons[requestedType];
                else return (T)CreateSingleton(T_TImplementations[requestedType], requestedType);
            }
        }

        private object CreateScoped(Type type, Type requestedType)
        {
            ConstructorInfo ctr = type.GetConstructors()[0];
            List<Type> argTypes = ctr.GetParameters().Select(p => p.ParameterType).ToList();
            if (argTypes.Count == 0)
            {
                _scopedes[requestedType] = ctr.Invoke(null);
                return _scopedes[requestedType];
            }
            for (int i = 0; i < argTypes.Count; i++)
            {
                if (!_lifeTimeInfo.ContainsKey(argTypes[i].FullName)) throw new NotRegisteredServiceException(argTypes[i].FullName);
            }
            object[] args = new object[argTypes.Count];
            for (int i = 0; i < argTypes.Count; i++)
            {
                MethodInfo GenericGetService = typeof(DIProvider).GetMethod("GetService").MakeGenericMethod(argTypes[i]);
                args[i] = GenericGetService.Invoke(this, null);
            }
            _scopedes[requestedType] = ctr.Invoke(args);
            return _scopedes[requestedType];
        }


        private object CreateSingleton(Type type,Type requestedType)
        {
            ConstructorInfo ctr = type.GetConstructors()[0];
            List<Type> argTypes = ctr.GetParameters().Select(p => p.ParameterType).ToList();
            if(argTypes.Count == 0)
            {
                _singletons[requestedType] = ctr.Invoke(null);
                return _singletons[requestedType];
            }
            for (int i = 0; i < argTypes.Count; i++)
            {
                if (!_lifeTimeInfo.ContainsKey(argTypes[i].FullName)) throw new NotRegisteredServiceException(argTypes[i].FullName);
            }
            object[] args = new object[argTypes.Count];
            for (int i = 0; i < argTypes.Count; i++)
            {
                MethodInfo GenericGetService = typeof(DIProvider).GetMethod("GetService").MakeGenericMethod(argTypes[i]);
                args[i] = GenericGetService.Invoke(this, null);
            }
            _singletons[requestedType] = ctr.Invoke(args);
            return _singletons[requestedType];
        }
        private object ResolveTransient(Type type)
        {
            ConstructorInfo ctr = type.GetConstructors()[0];
            List<Type> argTypes = ctr.GetParameters().Select(p => p.ParameterType).ToList();
            if (argTypes.Count == 0) return ctr.Invoke(null);
            for(int i = 0; i < argTypes.Count; i++)
            {
                if (!_lifeTimeInfo.ContainsKey(argTypes[i].FullName)) throw new NotRegisteredServiceException(argTypes[i].FullName);
            }
            object[] args = new object[argTypes.Count];
            for(int i = 0; i < argTypes.Count; i++)
            {
                MethodInfo GenericGetService = typeof(DIProvider).GetMethod("GetService").MakeGenericMethod(argTypes[i]);
                args[i] = GenericGetService.Invoke(this,null);
            }
            return ctr.Invoke(args);

        }
    }
}
