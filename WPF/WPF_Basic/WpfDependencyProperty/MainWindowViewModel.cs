using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDependencyProperty
{
    public class MainWindowViewModel : BindableBase
    {
        private double percent = 0.0;
        public double Percent { get => percent; set => SetProperty(ref percent, value); }

        private bool isChecked = false;
        public bool IsChecked { get => isChecked; set => SetProperty(ref isChecked, value); }

        private CancellationTokenSource cts = null;
        public DelegateCommand StartProgressCommand { get; }

        public MainWindowViewModel()
        {
            StartProgressCommand = new DelegateCommand(OnStartProgress, CanStartProgress);
        }

        private async void OnStartProgress()
        {
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            StartProgressCommand.RaiseCanExecuteChanged();

            await Task.Run(async () =>
            {
                for (double p = 0; p <= 100; p+=0.1)
                {
                    Percent = p;
                    await Task.Delay(10);
                }
            }, token);

            cts?.Dispose();
            cts = null;
            StartProgressCommand.RaiseCanExecuteChanged();
        }

        private bool CanStartProgress()
        {
            if (cts == null)
                return true;
            else
                return false;
        }
    }
}
