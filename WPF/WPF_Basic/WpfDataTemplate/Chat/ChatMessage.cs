using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDataTemplate.Chat
{
    public class ChatMessage : BindableBase
    {
        private DateTime time = DateTime.Now;
        public DateTime Time { get => time; set => SetProperty(ref time, value); }

        private string text = "";
        public string Text { get => text; set => SetProperty(ref text, value); }

        private bool isMe = false;
        public bool IsMe { get => isMe; set => SetProperty(ref isMe, value); }
    }
}
