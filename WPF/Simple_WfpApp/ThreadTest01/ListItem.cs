using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace ThreadTest01
{
    public class ListItem : Notifier
    {
        public int Index { get; set; } = 0;

        private string message = "";
        public string Message
        {
            get => message;
            set => OnPropertyChanged(ref message, value);
        }
        public string Description { get; set; } = "";

        public ListItem(int index, string message, string description)
        {
            Index = index;
            Message = message;
            Description = description;
        }
    }
}
