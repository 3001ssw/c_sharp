﻿using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static bool bFlag = false;
        static void Main(string[] args)
        {
            //Console.WriteLine("Start Main");

            // 람다
            //Thread thLambda = new Thread(() =>
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        Console.WriteLine($"Thread: {i}");
            //        Thread.Sleep(1000);
            //    }
            //});
            //thLambda.Start();
            //thLambda.Join();

            // 시작
            //Thread thread = new Thread(Run);
            //Thread thread = new Thread(new ThreadStart(Run)); // 생성
            //thread.Start(); // 쓰레드 시작

            // join
            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.Start(); // 쓰레드 시작
            //thread.Join(); // 종료할 때 까지 대기

            // join 3초
            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.Start(); // 쓰레드 시작
            //thread.Join(3000); // 3초 대기

            // IsAlive
            //Thread thread = new Thread(new ThreadStart(Run));
            //Console.WriteLine($"before start. is alive? : {thread.IsAlive}");
            //thread.Start(); // 쓰레드 시작
            //Console.WriteLine($"after start. is alive? : {thread.IsAlive}");
            //thread.Join(); // 종료할 때 까지 대기

            // Abort
            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.Start(); // 쓰레드 시작
            //Thread.Sleep(3000);
            //thread.Abort(); // 강제 종료

            // Suspend, Resume
            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.Start(); // 쓰레드 시작
            //Thread.Sleep(3000);
            //thread.Suspend(); // 일시 중지
            //Thread.Sleep(1000);
            //thread.Resume(); // 다시 시작
            //thread.Join(); // 종료할 때 까지 대기

            // Interrupt
            //Thread thread = new Thread(new ThreadStart(Run));
            //thread.Start();
            //if (thread.IsAlive)
            //{
            //    Thread.Sleep(3000); // 3초 대기
            //    thread.Interrupt(); // 인터럽트 발생
            //    thread.Join(); // 종료할 때 까지 대기
            //}

            // Flag
            //Thread thread = new Thread(new ThreadStart(Run));
            //bFlag = true; // 시작 전 플래그 true 처리
            //thread.Start();
            //if (thread.IsAlive)
            //{
            //    Thread.Sleep(3000); // 3초 대기
            //    bFlag = false; // 종료를 위해 플래그 false 처리
            //    thread.Join(); // 종료할 때 까지 대기
            //}

            // parameter
            Thread thParam = new Thread(new ParameterizedThreadStart(Run));
            thParam.Start(3);
            //thParam.Start(5);
            thParam.Join();

            //Console.WriteLine("Close Main");
        }

        static void Run()
        {
            Console.WriteLine("Start Thread");

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    if (bFlag == false) // false이 되면 for문 종료
                        break;

                    Console.WriteLine($"Thread: {i}");
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadInterruptedException exInterrupt)
            {
                Console.WriteLine(exInterrupt.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Close Thread");
        }

        static void Run(object? iParamCount)
        {
            if (iParamCount != null)
            {
                int iCount = (int)iParamCount; // for 횟수
                Console.WriteLine("param val: " + iCount);

                for (int i = 0; i < iCount; i++)
                {
                    Console.WriteLine($"Thread: {i}");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
