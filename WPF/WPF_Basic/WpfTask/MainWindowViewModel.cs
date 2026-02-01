using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfTask.ViewModels;

namespace WpfTask
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<TabItemBaseViewModel> tabs = new ObservableCollection<TabItemBaseViewModel>();
        public ObservableCollection<TabItemBaseViewModel> Tabs { get => tabs; set => SetProperty(ref tabs, value); }

        private TabItemBaseViewModel? selectedTab = null;
        public TabItemBaseViewModel? SelectedTab { get => selectedTab; set => SetProperty(ref selectedTab, value); }

        private TabItemSimpleTaskViewModel simpleTaskVM = new TabItemSimpleTaskViewModel();
        private TabItemTaskViewModel taskVM = new TabItemTaskViewModel();
        private TabItemFuncTaskViewModel functionTaskVM = new TabItemFuncTaskViewModel();

        #region constructor
        public MainWindowViewModel()
        {
            Tabs.Add(simpleTaskVM);
            Tabs.Add(taskVM);
            Tabs.Add(functionTaskVM);
            SelectedTab = taskVM;
        }
        #endregion
    }
}
