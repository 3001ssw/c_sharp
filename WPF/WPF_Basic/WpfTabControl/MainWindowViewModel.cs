using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using WpfTabControl.TabControlItem.ViewModels;
using WpfTabControl.TabControlItem.Views;

namespace WpfTabControl
{
    public class MainWindowViewModel : Notifier
    {
        public ObservableCollection<TabItemBaseViewModel> Tabs { get; } = new ObservableCollection<TabItemBaseViewModel>();
        public TabItemBaseViewModel SelectedTab { get; set; }

        public MainWindowViewModel()
        {
            TabItem1ViewModel vm1 = new TabItem1ViewModel()
            {
                Header = "View 1",
            };
            TabItem2ViewModel vm2 = new TabItem2ViewModel()
            {
                Header = "View 2",
            };
            Tabs.Add(vm1);
            Tabs.Add(vm2);

            SelectedTab = vm1;
        }
    }
}
