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

        private string textTargetName = "";
        public string TextTargetName { get => textTargetName; set => SetValue(ref textTargetName, value); }

        private ObservableCollection<PanelBaseViewModel> panels = new ObservableCollection<PanelBaseViewModel>();
        public ObservableCollection<PanelBaseViewModel> Panels { get => panels; set => SetValue(ref panels, value); }

        private BaseLayoutItem activePanel = null;
        public BaseLayoutItem ActivePanel { get => activePanel; set => SetValue(ref activePanel, value); }

        #endregion

        #region commands

        public DelegateCommand TestCommand { get; private set; }
        public DelegateCommand LeftCommand { get; private set; }

        public DelegateCommand RightCommand { get; private set; }
        public DelegateCommand<DockOperationCompletedEventArgs> DockOperationCompletedCommand { get; private set; }
        #endregion


        public MainViewModel()
        {
            TestCommand = new DelegateCommand(OnTest, CanTest);
            LeftCommand = new DelegateCommand(OnLeft, CanLeft);
            RightCommand = new DelegateCommand(OnRight, CanRight);
            DockOperationCompletedCommand = new DelegateCommand<DockOperationCompletedEventArgs>(OnDockOperationCompleted);

            MyPanel1ViewModel vm1 = new MyPanel1ViewModel()
            {
                Caption = "View Model 1",
                Display = "MyPanel1ViewModel",
                TargetName = "top_left",
            };
            Panels.Add(vm1);

            MyPanel2ViewModel vm2 = new MyPanel2ViewModel()
            {
                Caption = "View Model 2",
                TargetName = "top_right",
            };
            Panels.Add(vm2);

            MyPanel1ViewModel vm3 = new MyPanel1ViewModel()
            {
                Caption = "View Model 3",
                Display = "MyPanel1ViewModel",
                TargetName = "mid_left_top",
            };
            Panels.Add(vm3);

            MyPanel1ViewModel vm4 = new MyPanel1ViewModel()
            {
                Caption = "View Model 4",
                Display = "MyPanel1ViewModel",
                TargetName = "mid_left_bottom",
            };
            Panels.Add(vm4);

            MyPanel1ViewModel vm5 = new MyPanel1ViewModel()
            {
                Caption = "View Model 5",
                TargetName = "mid_center",
            };
            Panels.Add(vm5);

            MyPanel1ViewModel vm6 = new MyPanel1ViewModel()
            {
                Caption = "View Model 6",
                TargetName = "mid_right_top",
            };
            Panels.Add(vm6);

            MyPanel1ViewModel vm7 = new MyPanel1ViewModel()
            {
                Caption = "View Model 7",
                TargetName = "mid_right_bottom",
            };
            Panels.Add(vm7);

            MyPanel1ViewModel vm8 = new MyPanel1ViewModel()
            {
                Caption = "View Model 8",
                TargetName = "root_bottom",
            };
            Panels.Add(vm8);

            MyPanel1ViewModel vm9 = new MyPanel1ViewModel()
            {
                Caption = "View Model 9",
                TargetName = "root_bottom",
            };
            Panels.Add(vm9);
        }

        private void OnTest()
        {
            if (string.IsNullOrEmpty(TextTargetName))
                return;

            BaseLayoutItem current = ActivePanel as BaseLayoutItem;
            PanelBaseViewModel vm = current?.DataContext as PanelBaseViewModel;
            Debug.WriteLine("test");
            if (vm != null)
            {
                vm.TargetName = TextTargetName;
            }
            //foreach (PanelBaseViewModel vm in Panels)
            //{
            //    vm.IsVisibility = (vm.IsVisibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            //    if (vm.IsVisibility == Visibility.Collapsed)
            //    {
            //        //vm.IsClosed = false;
            //        vm.IsVisibility = Visibility.Visible;
            //    }
            //}
            //Panels[0].IsActive = (Panels[0].IsActive == true) ? Panels[0].IsActive = false : Panels[0].IsActive = true;
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
        private void OnDockOperationCompleted(DockOperationCompletedEventArgs e)
        {
            if (e.DockOperation != DockOperation.Dock && e.DockOperation != DockOperation.Move)
                return;

            LayoutPanel panel = e.Item as LayoutPanel;
            PanelBaseViewModel vm = panel?.DataContext as PanelBaseViewModel;

            if (panel == null || vm == null)
                return;

            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                BaseLayoutItem current = panel.Parent as BaseLayoutItem;
                while (current != null)
                {
                    Debug.WriteLine($"Parent: {current.GetType().Name}, Name='{current.Name}'");
                    if (!string.IsNullOrEmpty(current.Name))
                    {
                        Debug.WriteLine($"{vm.TargetName} => {current.Name}");
                        vm.TargetName = current.Name;
                        break;
                    }
                    current = current.Parent as BaseLayoutItem;
                }
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }
    }
}
