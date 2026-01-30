using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDialogService
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

        // xxxxxViewModel -> xxxxxView 이름 규칙을 명백히 지켜줘야함
        public bool? ShowDialogActivator(object viewModel)
        {
            // 1. namespace까지 포함된 이름
            // 이렇게 하면 Type.GetType으로 한번에 얻을 수 있다.
            // 대신 View, ViewModel이 같은 namespace여야함
            //string? viewTypeName = viewModel.GetType().FullName?.Replace("ViewModel", "View"); // 이름에서 ViewModel을 View로 바꾼다
            //if (viewTypeName == null)
            //    throw new Exception();
            //
            //Type? type = Type.GetType(viewTypeName);
            //if (type == null)
            //    throw new Exception();

            // 2. 이름만으로 Assembly에서 찾는 방법
            // 이렇게 하면 같은 프로젝트에서만 찾을 수 있다.
            string? viewTypeName = viewModel.GetType().Name?.Replace("ViewModel", "View"); // 이름에서 ViewModel을 View로 바꾼다
            Assembly? assembly = Assembly.GetExecutingAssembly();
            if (assembly == null)
                throw new Exception();
            Type[] types = assembly.GetTypes();
            Type? type = types.FirstOrDefault(x => x.Name == viewTypeName);
            if (type == null)
                throw new Exception();

            Window? window = Activator.CreateInstance(type) as Window;
            if (window == null)
                throw new Exception();

            window.DataContext = viewModel;
            window.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowInTaskbar = false;

            // ViewModel이 RequestClose 이벤트 제공하면 연결
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
        bool? ShowDialogActivator(object viewModel);
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
