namespace check_unchecked
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // overflow 1
            //uint a = uint.MaxValue;
            //
            //unchecked
            //{
            //    Console.WriteLine(a + 3);  // output: 2
            //}
            //
            //try
            //{
            //    checked
            //    {
            //        Console.WriteLine(a + 3);
            //    }
            //}
            //catch (OverflowException e)
            //{
            //    Console.WriteLine(e.Message);  // output: Arithmetic operation resulted in an overflow.
            //}

            // overflow 2
            //double a = double.MaxValue;
            //
            //int b = unchecked((int)a);
            //Console.WriteLine(b);  // output: -2147483648
            //
            //try
            //{
            //    b = checked((int)a);
            //}
            //catch (OverflowException e)
            //{
            //    Console.WriteLine(e.Message);  // output: Arithmetic operation resulted in an overflow.
            //}

            // overflow 3
            int Multiply(int a, int b) => a * b;            
            int factor = 2;            
            try
            {
                checked
                {
                    Console.WriteLine(Multiply(factor, int.MaxValue));  // output: -2, 함수 내 오버플로우 검사 X
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            
            try
            {
                checked
                {
                    Console.WriteLine(Multiply(factor, factor * int.MaxValue));
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);  // output: Arithmetic operation resulted in an overflow.
            }
        }

    }
}
