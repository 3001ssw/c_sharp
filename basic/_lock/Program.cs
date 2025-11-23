namespace _lock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(() => Run());
            Thread thread2 = new Thread(() => Run());

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        static object lock_obj = new object(); // lock 개체
        static int number = 0; // 두 개의 쓰레드에 공유된 자원

        static void Run()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (lock_obj) // lock_obj를 기준으로 임계영역 설정
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
                } // lock 블록 종료.
            }
        }
    }
}
