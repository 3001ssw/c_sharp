using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Main");
            Thread thread1 = new Thread(new ThreadStart(RunThread));
            thread1.Start();

            Console.WriteLine("Close Main");
        }

        static void RunThread()
        {
            Console.WriteLine("Start Thread" + Thread.CurrentThread.Name);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello, World!");
                Thread.Sleep(100);
            }
            Console.WriteLine("Close Thread");
        }
    }
}
