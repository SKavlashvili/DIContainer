namespace TestDI.TestClasses
{
    public interface ITest1
    {
        int GetObjectCreatorCounter();
    }
    public class Test1 : ITest1
    {
        public static int ObjectCreatorCounter = 0;
        public Test1()
        {
            ObjectCreatorCounter++;
        }

        public int GetObjectCreatorCounter()
        {
            return ObjectCreatorCounter;
        }
    }
}
