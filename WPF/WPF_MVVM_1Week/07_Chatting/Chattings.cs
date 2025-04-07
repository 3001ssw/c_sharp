using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Chatting
{
    public class Chattings : ObservableCollection<Message>
    {
        public Chattings()
        {
            this.Add(new Message() { Sender = "Son", Content = "hi" });
            this.Add(new Message() { Sender = "Kim", Content = "nice to meet you" });
            this.Add(new Message() { Sender = "Son", Content = "how are you" });
            this.Add(new Message() { Sender = "Kim", Content = "i'm fine thanks and you?" });
            this.Add(new Message() { Sender = "Son", Content = "i'm fine too" });
        }
    }

    public class Message
    {
        public string Sender { get; set; }
        public string Content { get; set; }
    }
}
