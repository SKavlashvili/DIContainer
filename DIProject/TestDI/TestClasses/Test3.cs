using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDI.TestClasses
{
    public interface ITest3
    {
        int GetObjectCreatorCounter();
    }
    public class Test3 : ITest3
    {
        public static int ObjectCreationalCounter = 0;
        public ITest2 Test2 { get; set; }
        public ITest1 Test1 { get; set; }
        public Test3(ITest2 test2, ITest1 test1)
        {
            Test2 = test2;
            Test1 = test1;
            ObjectCreationalCounter++;
        }
        public int GetObjectCreatorCounter()
        {
            return ObjectCreationalCounter;
        }
    }
}
