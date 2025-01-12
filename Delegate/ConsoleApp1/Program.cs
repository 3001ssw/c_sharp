using static ConsoleApp1.Program;

namespace ConsoleApp1
{
    internal class Program
    {
        public delegate void PrintIntDelegate(int x);

        public delegate void PrintStringDelegate(string str);

        public delegate void GenericDelegate<T>(T type);

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

            // delegate callback
            PrintStringDelegate? del3 = null;
            del3 += PrintStr1;
            del3 += PrintStr2;
            DelegateCallback("콜백1", del3);
            Console.WriteLine("======================");
            PrintStringDelegate? del4 = null;
            del4 += PrintStr2;
            del4 += PrintStr3;
            DelegateCallback("콜백2", del3);
            Console.WriteLine("======================");


            // delegate에 일반화 Type
            GenericDelegate<int>? del5 = null;
            del5 += GenericFunction1;
            del5 += GenericFunction2;
            del5 += GenericFunction3;
            del5(100);
            Console.WriteLine("======================");

            GenericDelegate<string>? del6 = null;
            del6 += GenericFunction1;
            del6 += GenericFunction2;
            del6 += GenericFunction3;
            del6("abcdefg");
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

        public static void DelegateCallback(string str, PrintStringDelegate del)
        {
            del(str);
        }

        public static void GenericFunction1<T>(T type)
        {
            Console.WriteLine($"Generic Function1: parameter({type}), parameter type(${type.GetType()})");
        }

        public static void GenericFunction2<T>(T type)
        {
            Console.WriteLine($"Generic Function2 - parameter: {type}, type: ${type.GetType()}");
        }

        public static void GenericFunction3<T>(T type)
        {
            Console.WriteLine($"Generic Function3: parameter, type({type}, ${type.GetType()})");
        }
    }
}
