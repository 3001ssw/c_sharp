using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfDataTemplate.Chat
{
    public class ChatMessageSelector : DataTemplateSelector
    {
        public DataTemplate? MyTemplate { get; set; }
        public DataTemplate? YourTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            // item은 현재 그려질 데이터 하나 (Message 객체)
            if (item is ChatMessage message)
            {
                if (message.IsMe)
                    return MyTemplate;
                else
                    return YourTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
