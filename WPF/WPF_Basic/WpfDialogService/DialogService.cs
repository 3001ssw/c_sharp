using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDialogService
{
    public class DialogService : IDialogService
    {
        public Window? window = null;

        public bool? ShowDialog(object owner, object viewModel)
        {
            // ViewModel -> View 자동 매핑 (UserDialogViewModel → UserDialogView)
            string viewTypeName = viewModel.GetType().Name.Replace("ViewModel", "View"); // 이름에서 ViewModel을 View로 바꾼다

            var assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            Type? type = types.FirstOrDefault(x => x.Name == viewTypeName);
            if (type == null)
                return false;

            Window? window = Activator.CreateInstance(type) as Window;
            if (window == null)
                return false;

            window.DataContext = viewModel;

            window.Owner = Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // ViewModel이 RequestClose 이벤트 제공하면 연결
            if (viewModel is IDialogViewModel vm)
            {
                vm.RequestClose += (s, dialogResult) =>
                {
                    window.DialogResult = dialogResult;
                    window.Close();
                };
            }

            return window.ShowDialog(); // <-- ShowDialog 구현 완료
        }

        public void CloseDialog(bool bResult)
        {
            window?.Close();
        }

    }

    public interface IDialogService
    {
        bool? ShowDialog(object owner, object viewModel);
    }

    public interface IDialogViewModel
    {
        event EventHandler<bool?> RequestClose;
    }
}
