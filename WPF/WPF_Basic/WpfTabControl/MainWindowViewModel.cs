using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace WpfTabControl
{
    public class MainWindowViewModel : Notifier
    {
        public ObservableCollection<TabItemViewModel> Tabs { get; }

        public MainWindowViewModel()
        {
            Tabs = new ObservableCollection<TabItemViewModel>
            {
                new TabItemViewModel
                {
                    Header = "첫번째 탭",
                    Content = new Views.MyControl()   // UserControl 직접 할당
                },
                new TabItemViewModel
                {
                    Header = "두번째 탭",
                    Content = new Views.MyControl()   // 또 다른 UserControl
                }
            };
        }
    }
}
