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
            thread1.Join();

            Console.WriteLine("Close Main");
        }

        static void RunThread()
        {
            Console.WriteLine("Start Thread");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
            Console.WriteLine("Close Thread");
        }
    }
}
