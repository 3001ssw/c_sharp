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

namespace WpfDevDockLayoutManager
{
    public class MainViewModel : ViewModelBase
    {
        #region fields, properties

        private ObservableCollection<MyPanelViewModel> panels = new ObservableCollection<MyPanelViewModel>();
        public ObservableCollection<MyPanelViewModel> Panels { get => panels; set => SetValue(ref panels, value); }

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
                Display = "Display 1",
            };
            MyPanel1ViewModel vm2 = new MyPanel1ViewModel()
            {
                Caption = "View Model 2",
                Display = "Display 2",
            };
            MyPanel1ViewModel vm3 = new MyPanel1ViewModel()
            {
                Caption = "View Model 3",
                Display = "Display 3",
            };
            Panels.Add(vm1);
            Panels.Add(vm2);
            Panels.Add(vm3);
        }

        private void OnTest()
        {
            Debug.WriteLine("test");
            foreach (MyPanelViewModel vm in Panels)
            {
                if (vm.IsPanelVisible == Visibility.Collapsed)
                {
                    //vm.IsClosed = false;
                    vm.IsPanelVisible = Visibility.Visible;
                }
            }
        }

        private bool CanTest()
        {
            return true;
        }

        private void OnLeft()
        {
            if (ActivePanel?.DataContext is MyPanelViewModel vm)
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
            if (ActivePanel?.DataContext is MyPanelViewModel vm)
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
