using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfDevDockLayoutManager.Panel;
using WpfDevDockLayoutManager.Panel.ViewModels;

namespace WpfDevDockLayoutManager
{
    public class MainViewModel : ViewModelBase
    {
        #region fields, properties

        private ObservableCollection<PanelBaseViewModel> panels = new ObservableCollection<PanelBaseViewModel>();
        public ObservableCollection<PanelBaseViewModel> Panels { get => panels; set => SetValue(ref panels, value); }

        private BaseLayoutItem activePanel = null;
        public BaseLayoutItem ActivePanel { get => activePanel; set => SetValue(ref activePanel, value); }

        #endregion

        #region commands

        public DelegateCommand TestCommand { get; private set; }
        public DelegateCommand LeftCommand { get; private set; }

        public DelegateCommand RightCommand { get; private set; }
        #endregion


        public MainViewModel()
        {
            TestCommand = new DelegateCommand(OnTest, CanTest);
            LeftCommand = new DelegateCommand(OnLeft, CanLeft);
            RightCommand = new DelegateCommand(OnRight, CanRight);

            MyPanel1ViewModel vm1 = new MyPanel1ViewModel()
            {
                Caption = "View Model 1",
                Display = "MyPanel1ViewModel",
            };
            MyPanel2ViewModel vm2 = new MyPanel2ViewModel()
            {
                Caption = "View Model 2",
            };
            MyPanel1ViewModel vm3 = new MyPanel1ViewModel()
            {
                Caption = "View Model 3",
                Display = "MyPanel1ViewModel",
            };
            Panels.Add(vm1);
            Panels.Add(vm2);
            Panels.Add(vm3);
        }

        private void OnTest()
        {
            Debug.WriteLine("test");
            //foreach (PanelBaseViewModel vm in Panels)
            //{
            //    vm.IsVisibility = (vm.IsVisibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            //    if (vm.IsVisibility == Visibility.Collapsed)
            //    {
            //        //vm.IsClosed = false;
            //        vm.IsVisibility = Visibility.Visible;
            //    }
            //}
            Panels[0].IsActive = (Panels[0].IsActive == true) ? Panels[0].IsActive = false : Panels[0].IsActive = true;
        }

        private bool CanTest()
        {
            return true;
        }

        private void OnLeft()
        {
            if (ActivePanel?.DataContext is PanelBaseViewModel vm)
            {
                vm.TargetName = "LeftGroup";
            }
        }

        private bool CanLeft()
        {
            return true;
        }

        private void OnRight()
        {
            if (ActivePanel?.DataContext is PanelBaseViewModel vm)
            {
                vm.TargetName = "RightGroup";
            }
        }

        private bool CanRight()
        {
            return true;
        }
    }
}
