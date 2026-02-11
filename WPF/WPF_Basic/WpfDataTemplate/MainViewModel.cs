using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDataTemplate.Chat;

namespace WpfDataTemplate
{
    public class MainViewModel : BindableBase
    {
        private ObservableCollection<ChatMessage> messages = new ObservableCollection<ChatMessage>();
        public ObservableCollection<ChatMessage> Messages { get => messages; set => SetProperty(ref messages, value); }



        #region constructor
        public MainViewModel()
        {
            Messages.Add(new ChatMessage()
            {
                Text = "hi",
                IsMe = true,
            });
            Messages.Add(new ChatMessage()
            {
                Text = "hi",
                IsMe = false,
            });
        }
        #endregion
    }
}
