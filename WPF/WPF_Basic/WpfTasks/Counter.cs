using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTasks
{
    public class Counter : BindableBase
    {
        private string id = Guid.NewGuid().ToString();
        public string Id { get => id; set => SetProperty(ref id, value); }

        private int count = 0;
        public int Count { get => count; set => SetProperty(ref count, value); }

        private int sleep = 100;
        public int Sleep { get => sleep; set => SetProperty(ref sleep, value); }

        public Counter()
        {
            Random random = new Random();
            Sleep = random.Next(100, 3000);
        }
    }
}
