using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace TaskTest02
{
    public class MainWindowViewModel : Notifier
    {
        public ObservableCollection<TaskItemViewModel> Tasks { get; } = new ObservableCollection<TaskItemViewModel>();

        public Command StartCommand { get; }
        public Command StopCommand { get; }

        private CancellationTokenSource _cts = null;

        public MainWindowViewModel()
        {
            StartCommand = new Command(OnStartCommand, OnExecStartCommand);
            StopCommand = new Command(OnStopCommand, OnExecStopCommand);

            MakeTasks();
        }

        private async void OnStartCommand()
        {
            _cts = new CancellationTokenSource();

            // todo .. : 
            var allTasks = Tasks.Select(task => task.StartProgressAsync(_cts.Token));
            //var allTasks = Tasks.Select(task => Task.Run(() => task.StartProgressAsync(_cts.Token)));
            await Task.WhenAll(allTasks);

            _cts = null;
        }

        private bool OnExecStartCommand()
        {
            if (_cts != null)
                return false;

            return true;
        }

        private void OnStopCommand()
        {
            _cts?.Cancel();
        }

        private bool OnExecStopCommand()
        {
            if (_cts == null)
                return false;

            return true;
        }

        private void MakeTasks()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Tasks.Add(new TaskItemViewModel()
                {
                    Title = $"title {Tasks.Count}",
                    Delay = random.Next(10, 101),
                });
            }
        }
    }
}
