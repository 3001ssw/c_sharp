using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Util;

namespace TaskTest01
{
    public class MainWindowViewModel : Notifier
    {
        public MainWindowModel Model { get; set; } = new MainWindowModel();

        public ObservableCollection<Student> Students
        {
            get => Model.Students;
        }

        public Command StartAddCommand { get; set; }
        public Command StopAddCommand { get; set; }

        public Command ParallelStartCommand { get; set; }
        public Command ParallelStopCommand { get; set; }


        SynchronizationContext _uiThread;

        private Task<int> _addTask;
        private CancellationTokenSource _addCts;

        public MainWindowViewModel()
        {
            _uiThread = SynchronizationContext.Current ?? DispatcherSynchronizationContext.Current;

            StartAddCommand = new Command(OnStartAdd, CanStartAdd);
            StopAddCommand = new Command(OnStopAdd, CanStopAdd);
        }

        private async void OnStartAdd()
        {
            _addCts = new CancellationTokenSource();
            // 가장보편적임 1
            //int iRes = await Task<int>.Run(() => AddStudent(_addCts.Token, _uiThread));

            // 가장보편적임 2
            _addTask = Task<int>.Run(() => AddStudent(_addCts.Token, _uiThread));
            int iRes = await _addTask;

            // 이렇게는 사용 안함
            //_addTask = new Task<int>(() => AddStudent(_addCts.Token, _uiThread));
            //_addTask.Start();
            //int iRes = await _addTask;

            //_addCts = null;
            Debug.WriteLine($"count: {iRes}");
        }

        private bool CanStartAdd()
        {
            if (_addCts == null)
                return true;
            else
            {
                if (_addCts?.IsCancellationRequested == true)
                    return true;
                else
                    return false;
            }
        }

        private void OnStopAdd()
        {
            _addCts?.Cancel();
        }

        private bool CanStopAdd()
        {
            if (_addCts == null)
                return false;
            else
            {
                if (_addCts?.IsCancellationRequested == true)
                    return false;
                else
                    return true;
            }
        }


        public int AddStudent(CancellationToken token, SynchronizationContext uiThread)
        {
            try
            {
                int count = Model.Students.Count;
                Random random = new Random();

                for (int index = count; index < count + 10; index++)
                {
                    token.ThrowIfCancellationRequested();

                    // UI 변경은 UI thread에서
                    uiThread?.Post(_ =>
                    {
                        Students.Add(new Student
                        {
                            ID = index,
                            Name = $"name: {index}",
                            Age = random.Next(1, 101),
                        });
                    }, null);

                    // 1초 쉬기 (동기 방식)
                    Thread.Sleep(1000);
                }
            }
            catch (OperationCanceledException)
            {

            }

            return Model.Students.Count;
        }

    }
}
