using static ConsoleApp1.Program;

namespace ConsoleApp1
{
    internal class Program
    {
        public delegate void PrintIntDelegate(int x);

        public delegate void PrintStringDelegate(string str);

        public delegate void PrintTypeDelegate<T>(T type);

        static void Main(string[] ars)
        {
            // delegate 기본코드
            PrintIntDelegate del1 = new PrintIntDelegate(PrintNo);
            del1(10);
            del1(15);
            del1(20);
            Console.WriteLine("======================");

            // delegate chain
            PrintStringDelegate? del2 = null;
            del2 += PrintStr1;
            del2 += PrintStr2;
            del2 += PrintStr3;
            if (del2 != null)
                del2("hi");

            del2 -= PrintStr2; // Print2 삭제
            Console.WriteLine("--- PrintStr2 제거 ---");
            if (del2 != null)
                del2("hi");
            Console.WriteLine("======================");

            // delegate에 일반화 Type
            PrintTypeDelegate<int>? del3 = null;
            del3 += PrintNo;
            del3(100);
            PrintTypeDelegate<string>? del4 = null;
            del4 += PrintStr1;
            del4 += PrintStr2;
            del4 += PrintStr3;
            del4("hellow");
            Console.WriteLine("======================");
        }

        public static void PrintNo(int x)
        {
            Console.WriteLine("PrintNo: " + x);
        }

        public static void PrintStr1(string str)
        {
            Console.WriteLine("PrintStr1: " + str);
        }

        public static void PrintStr2(string str)
        {
            Console.WriteLine("PrintStr2 [" + str + "]");
        }

        public static void PrintStr3(string str)
        {
            Console.WriteLine(str + " - PrintStr3");
        }
    }
}
