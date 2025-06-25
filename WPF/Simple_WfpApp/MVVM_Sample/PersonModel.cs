using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVVM_Sample.Model
{
    public class PersonModel
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public int Gender { get; set; }

        public PersonModel()
        {
            Name = "";
            Age = 0;
            Gender = 0;
        }

        public PersonModel(string name = "", int age = 0, int gender = 0)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }
    }
}
