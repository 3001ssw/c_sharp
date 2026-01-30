using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAction.Interfaces
{
    public class DialogService : IDialogService
    {
        public void Show<T>(object viewModel, Action<IDialogViewModel, bool?>? onResult = null) where T : Window
        {
            Window? window = Activator.CreateInstance<T>() as Window;
            if (window == null)
                throw new Exception();

            window.DataContext = viewModel;
            window.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowInTaskbar = false;

            // IDialogViewModel 이면
            if (viewModel is IDialogViewModel vm)
            {
                vm.DoCloseAction = (dialogResult) =>
                {
                    onResult?.Invoke(vm, dialogResult);
                    onResult = null;
                    window.Close(); // Closed 호출 됨
                };

                // x, Alt+F4 누르면 Close 호출된 뒤 수행 함
                window.Closed += (s, e) =>
                {
                    onResult?.Invoke(vm, false);
                    window.DataContext = null;
                    vm.DoCloseAction = null;
                };
            }

            window.Show();
        }

        public bool? ShowDialog<T>(object viewModel) where T : Window
        {
            Window? window = Activator.CreateInstance<T>() as Window;
            if (window == null)
                throw new Exception();

            window.DataContext = viewModel;
            window.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowInTaskbar = false;

            // IDialogViewModel 이면
            if (viewModel is IDialogViewModel vm)
            {
                vm.DoCloseAction = (dialogResult) =>
                {
                    window.DialogResult = dialogResult; // 여기서 내부적으로 window.Close() 호출
                    //window.Close();
                };

                // x, Alt+F4 누르면 Close 호출된 뒤 수행 함
                window.Closed += (s, e) =>
                {
                    window.DataContext = null;
                    vm.DoCloseAction = null;
                };
            }

            return window.ShowDialog();
        }

        public bool? ShowDialogDataTemplate(object viewModel)
        {
            // 이방식으로 만들어진 xaml은 Window가 아니여야함
            /* 어딘가엔 
             * 이렇게 되어있어야함
             * < DataTemplate DataType = "{x:Type local:UserControlViewModel}" >
             *     < local:UserControlView />
             * </ DataTemplate >
            */
            Window? owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            Window window = new Window
            {
                Content = viewModel,
                Owner = owner,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = false,
            };

            // IDialogViewModel 이면
            if (viewModel is IDialogViewModel vm)
            {
                vm.DoCloseAction = (dialogResult) =>
                {
                    window.DialogResult = dialogResult; // 여기서 내부적으로 window.Close() 호출
                    //window.Close();
                };

                // x, Alt+F4 누르면 Close 호출된 뒤 수행 함
                window.Closed += (s, e) =>
                {
                    window.DataContext = null;
                    vm.DoCloseAction = null;
                };
            }

            return window.ShowDialog();
        }
    }

    /// <summary>
    /// ShowDialog, Show를 하기위한 Interface
    /// </summary>
    public interface IDialogService
    {
        void Show<T>(object viewModel, Action<IDialogViewModel, bool?>? onClosed = null) where T : Window;
        bool? ShowDialog<T>(object viewModel) where T : Window;
        bool? ShowDialogDataTemplate(object viewModel);
    }

    /// <summary>
    /// Close를 하기 위한 Interface
    /// </summary>
    public interface IDialogViewModel
    {
        Action<bool?>? DoCloseAction { get; set; }
    }
}
