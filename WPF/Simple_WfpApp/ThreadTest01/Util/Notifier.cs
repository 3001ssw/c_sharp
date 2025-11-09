using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Util
{
    [Serializable]
    public class Notifier : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;


        public Notifier()
        {
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



    }
}
