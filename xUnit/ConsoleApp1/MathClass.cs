using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MathClass
    {
        public MathClass()
        {
            
        }

        static public int Sum(int a, int b)
        {
            int sum = a + b;
            return sum;
        }

        static public int Division(int a, int b)
        {
            int div = a / b;
            return div;
        }
    }
}
