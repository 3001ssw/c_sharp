namespace _lock
{
    internal class Program
    {
        static object lock_obj = new object();
        static int number = 0;

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(() => Run())
            {
                Name = "Thread1",
            };
            Thread thread2 = new Thread(() => Run())
            {
                Name = "Thread2",
            };

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        static void Run()
        {
            for (int i = 0; i < 100; i++)
            {
                //lock(lock_obj)
                {
                    int temp = number;
                    Console.WriteLine($"현재 숫자 : [{number}]");

                    number++;
                    Console.WriteLine($"1 증가 시킴");

                    if (temp + 1 == number)
                        Console.WriteLine($"1 증가 잘 됨");
                    else
                        Console.WriteLine($"???????? [{number}] ????????");

                    Thread.Sleep(10);
                }
            }
        }
    }
}
