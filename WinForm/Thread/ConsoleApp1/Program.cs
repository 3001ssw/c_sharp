using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Main");
            Thread thread1 = new Thread(new ThreadStart(StartThread));
            Thread thread2 = new Thread(new ParameterizedThreadStart(StartThreadParam));

            thread1.Name = "Thread1";
            thread2.Name = "Thread2";

            thread1.Start();
            thread2.Start(2);

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Close Main");
        }

        static void StartThread()
        {
            Console.WriteLine("Start Thread: " + Thread.CurrentThread.Name);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Hello, World!");
                Thread.Sleep(100);
            }
            Console.WriteLine("Close Thread");
        }

        static void StartThreadParam(object objParam)
        {
            int iParam = (int)objParam;
            Console.WriteLine("Start Thread: " + Thread.CurrentThread.Name + ", param: " + iParam);

            for (int i = 0; i < iParam; i++)
            {
                Console.WriteLine("print " + i);
                Thread.Sleep(1000);
            }
            Console.WriteLine("Close Thread");
        }
    }
}
