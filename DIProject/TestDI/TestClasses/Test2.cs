namespace TestDI.TestClasses
{
    public interface ITest2
    {
        int GetObjectCreatorCounter();
    }
    public class Test2 : ITest2
    {
        public static int ObjectCreationalCounter = 0;
        public ITest1 Test1 { get; set; }

        public Test2(ITest1 test)
        {
            Test1 = test;
            ObjectCreationalCounter++;
        }
        public int GetObjectCreatorCounter()
        {
            return ObjectCreationalCounter;
        }
    }
}
