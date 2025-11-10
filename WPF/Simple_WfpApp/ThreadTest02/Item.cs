using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace ThreadTest02
{
    public class Item : Notifier
    {
        public int Index { get; set; } = 0;

        private string message = "";
        public string Message
        {
            get => message;
            set => OnPropertyChanged(ref message, value);
        }
        public string Description { get; set; } = "";

        public Item(int index, string message, string description)
        {
            Index = index;
            Message = message;
            Description = description;
        }
    }
}
