using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Util;

namespace ThreadTest02
{
    public class MainWindowViewModel : Notifier
    {
        private MainWindowModel _model = new MainWindowModel();
        public MainWindowModel Model
        {
            get => _model;
        }

        public int Count
        {
            get => _model.Count;
            set
            {
                _model.Count = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Item> ListMsg
        {
            get => _model.ListMsg;
            set
            {
                _model.ListMsg = value;
                OnPropertyChanged();
            }
        }

        public Command StartCommand { get; set; }
        public Command PauseCommand { get; set; }
        public Command StopCommand { get; set; }

        public Command StartTestCommand { get; set; }
        public Command PauseTestCommand { get; set; }
        public Command StopTestCommand { get; set; }

        // ui
        SynchronizationContext _uiThread;
        // 백그라운드에서 숫자를 증가시키는 작업 스레드
        private Task _task;
        // 일시 정지/재개 제어 (true=실행, Reset()하면 Wait()에서 멈춤)
        private ManualResetEventSlim _pause;
        // 중지 토큰 (정지 신호)
        private CancellationTokenSource _cts;


        // delegate
        private delegate void ThreadEvent(Object sender, int count);
        private event ThreadEvent onThreadEventOccured = null;


        public MainWindowViewModel()
        {
            StartCommand = new Command(OnStartCommand, OnExecStartCommand);
            PauseCommand = new Command(OnPauseCommand, OnExecPauseCommand);
            StopCommand = new Command(OnStopCommand, OnExecStopCommand);

            StartTestCommand = new Command(OnStartTestCommand, OnExecStartTestCommand);
            PauseTestCommand = new Command(OnPauseTestCommand, OnExecPauseTestCommand);
            StopTestCommand = new Command(OnStopTestCommand, OnExecStopTestCommand);

            _uiThread = SynchronizationContext.Current ?? DispatcherSynchronizationContext.Current;

            onThreadEventOccured += OnEventOccured;
        }

        private async void OnStartCommand()
        {
            if (_task?.IsCompleted == false)
            {
                if (_pause?.IsSet is false)
                    _pause?.Set();

                return;
            }

            _cts = new CancellationTokenSource();
            _pause = new ManualResetEventSlim(true);
            _pause?.Set();
            _task = Task.Run(() => Run(_cts.Token, _pause, _uiThread));
            await _task;
            //_task?.Start();
            OnPropertyChangedAll();
        }


        private bool OnExecStartCommand()
        {
            if (_task is null || _task?.IsCompleted == true)
                return true;
            else
            {
                if (_pause?.IsSet is false)
                    return true;
                else
                    return false;
            }
        }

        private void OnPauseCommand()
        {
            _pause?.Reset();

            OnPropertyChangedAll();
        }

        private bool OnExecPauseCommand()
        {
            if (_task?.IsCompleted == false)
            {
                if (_pause?.IsSet is true)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        private void OnStopCommand()
        {
            if (_task?.IsCompleted == false)
            {
                _cts?.Cancel();
            }
            OnPropertyChangedAll();
        }

        private bool OnExecStopCommand()
        {
            if (_task?.IsCompleted == false)
                return true;
            else
                return false;
        }

        /// ////////////////////////////////////////////////////////////////////////////
        /// task test
        /// ////////////////////////////////////////////////////////////////////////////
        private CancellationTokenSource _ctsTest;
        private SemaphoreSlim _pauseSignal;

        private async void OnStartTestCommand()
        {
            if (_pauseSignal != null)
            {
                _pauseSignal.Release();
                return;
            }

            _ctsTest = new CancellationTokenSource();
            _pauseSignal = new SemaphoreSlim(ListMsg.Count, ListMsg.Count);

            List<Task> tasks = new List<Task>();
            foreach (Item item in ListMsg)
            {
                tasks.Add(item.StartWorkAsync(_ctsTest.Token, _pauseSignal));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch(TaskCanceledException)
            {

            }
            finally
            {
                _ctsTest.Cancel();
                _ctsTest = null;
                _pauseSignal = null;
            }
        }

        private bool OnExecStartTestCommand()
        {
            if (_ctsTest == null || _ctsTest.IsCancellationRequested)
                return true;
            else
            {
                if (_pauseSignal.CurrentCount == 0)
                    return true;
                else
                    return false;
            }
        }

        private void OnPauseTestCommand()
        {
            while (0 < _pauseSignal?.CurrentCount)
                _pauseSignal.Wait();
        }

        private bool OnExecPauseTestCommand()
        {
            if (_pauseSignal != null)
                return true;
            else
                return false;
        }

        private void OnStopTestCommand()
        {
            _ctsTest.Cancel();
        }

        private bool OnExecStopTestCommand()
        {
            if (_ctsTest != null && _ctsTest.IsCancellationRequested is false)
                return true;
            else
            {
                return false;
            }
        }

        private void OnEventOccured(Object sender, int count)
        {
            //ListMsg.Add($"{count}");
            //Count = count;
        }

        /// <summary>
        /// thread
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pause"></param>
        /// <param name="uiThread"></param>
        private void Run(CancellationToken token, ManualResetEventSlim pause, SynchronizationContext uiThread)
        {
            try
            {
                uiThread?.Post(_ =>
               {
                   ListMsg.Clear();
                   Count = 0;
               }, null);

                while (true)
                {
                    uiThread?.Post(_ =>
                    {
                        Count = Model.AddListMsg(string.Format($"msg: {Count}"), string.Format($"desc: {Count}"));
                    }, null);

                    Thread.Sleep(100);
                    pause.Wait(token);
                    token.ThrowIfCancellationRequested();

                }
            }
            catch (OperationCanceledException)
            {

            }
            finally
            {

            }
        }
    }
}
