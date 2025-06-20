using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_Notifier
{
    public class Person : ObservableCollection<Human>
    {
        public Person()
        {
            this.Add(new Human() { Name = "Son", Age = 10 });
            this.Add(new Human() { Name = "Kim", Age = 20 });
            this.Add(new Human() { Name = "Lee", Age = 30 });
            this.Add(new Human() { Name = "Park", Age = 40 });
            this.Add(new Human() { Name = "Choi", Age = 50 });
        }

        public IEnumerable<Human> FindHuman(string searchString)
        {
            return this.Where(item => item.Name.Contains(searchString));
        }
    }

    public class Human : Notifier
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                OnPropertyChanged("age");
            }
        }
    }
}
