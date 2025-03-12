using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    public class Human
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Car
    {
        public string CarName { get; set; }
        public double Speed { get; set; }

        public Human Driver { get; set; }
    }
}
