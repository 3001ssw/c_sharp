using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
            _timer.Interval = TimeSpan.FromMilliseconds(MillSec); // 주기
            _timer.Tick += (sender, eventargs) => // Tick
            {
                string message = DateTime.Now.ToString("HH:mm:ss.fff");
                Messages.Add(message);
                //Thread.Sleep(1000); // 사용 금지
            };
            _timer.Start();
        }

        private bool OnCanStartTimerCommand()
        {
            return !_timer.IsEnabled; // timer가 실행 중인지
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
