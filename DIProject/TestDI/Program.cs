using DI;
using System.Reflection.Metadata.Ecma335;
using TestDI.TestClasses;

namespace TestDI
{
    public class Program
    {
        public static void Main()
        {
            IServiceCollection services = DIContainer.BuildContainer();

            services.AddTransient<ITest1, Test1>();
            services.AddScoped<ITest2, Test2>();
            services.AddSingleton<ITest3, Test3>();

            DI.IServiceProvider provider = services.GenerateProvider();

            //Test for singleton
            ITest3 test3 = provider.GetService<ITest3>();
            Console.WriteLine(test3.GetObjectCreatorCounter());
            test3 = provider.GetService<ITest3>();
            Console.WriteLine(test3.GetObjectCreatorCounter());
            provider = services.GenerateProvider();
            test3 = provider.GetService<ITest3>();
            Console.WriteLine(test3.GetObjectCreatorCounter());

            Console.WriteLine("======================================");
            //Tests for scoped
            Console.WriteLine(((Test3)test3).Test2.GetObjectCreatorCounter());
            provider = services.GenerateProvider();
            ITest2 test2 = provider.GetService<ITest2>();
            Console.WriteLine(test2.GetObjectCreatorCounter());
            test2 = provider.GetService<ITest2>();
            Console.WriteLine(test2.GetObjectCreatorCounter());
            test2 = provider.GetService<ITest2>();
            Console.WriteLine(test2.GetObjectCreatorCounter());
            provider = services.GenerateProvider();
            test2 = provider.GetService<ITest2>();
            Console.WriteLine(test2.GetObjectCreatorCounter());


            Console.WriteLine("===========================================");
            
        }
    }
}