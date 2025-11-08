using i_FOS_X.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace i_FOS_X.Util
{
    [Serializable]
    public class Notifier : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private IDialogService _dialogService;

        public Notifier()
        {
            _dialogService = null;
        }

        public Notifier(Window dialog)
        {
            DialogService service = new DialogService(dialog);
            _dialogService = service;
        }

        public void SetDlgService(Window dialog)
        {
            DialogService service = new DialogService(dialog);
            _dialogService = service;
        }

        public void OnPropertyChanged<T>(ref T field, T newValue, [CallerMemberName] string name = null)
        {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void OnPropertyChangedAll() =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));


        public void CloseDialog(bool result = false)
        {
            _dialogService?.CloseDialog(result);
        }

    }
}
