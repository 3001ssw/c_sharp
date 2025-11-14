using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Util;

namespace DispatcherTimer01
{
    public class MainWindowViewModel : Notifier
    {
        public double MillSec { get; set; } = 500;

        public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();

        public Command StartTimerCommand { get; }
        public Command StopTimerCommand { get; }

        private DispatcherTimer _timer = new DispatcherTimer();

        public MainWindowViewModel()
        {
            StartTimerCommand = new Command(OnStartTimerCommand, OnCanStartTimerCommand);
            StopTimerCommand = new Command(OnStopTimerCommand, OnCanStopTimerCommand);
        }

        private void OnStartTimerCommand()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(MillSec);
            _timer.Tick += (s, e) => // Sender, EventArg
            {
                string message = DateTime.Now.ToString("HH:mm:ss.fff");
                Messages.Add(message);
            };
            _timer.Start();
        }

        private bool OnCanStartTimerCommand()
        {
            return !_timer.IsEnabled;
        }

        private void OnStopTimerCommand()
        {
            _timer.Stop();
        }

        private bool OnCanStopTimerCommand()
        {
            return _timer.IsEnabled;
        }
    }
}
