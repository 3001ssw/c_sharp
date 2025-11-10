using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Util;

namespace ThreadTest01
{
    public class MainWindowViewModel : Notifier
    {
        private MainWindowModel _model = new MainWindowModel();
        public int Count
        {
            get => _model.Count;
            set
            {
                _model.Count = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ListItem> ListMsg
        {
            get => _model.ListMsg;
            set
            {
                _model.ListMsg = value;
                OnPropertyChanged();
            }
        }

        public Command ThreadStartCommand { get; set; }
        public Command ThreadPauseCommand { get; set; }
        public Command ThreadStopCommand { get; set; }
        public Command TestCommand { get; set; }

        // ui
        SynchronizationContext _uiThread;
        // 백그라운드에서 숫자를 증가시키는 작업 스레드
        private Thread _thread;
        // 일시 정지/재개 제어 (true=실행, Reset()하면 Wait()에서 멈춤)
        private ManualResetEventSlim _pause;
        // 중지 토큰 (정지 신호)
        private CancellationTokenSource _cts;


        // delegate
        private delegate void ThreadEvent(Object sender, int count);
        private event ThreadEvent onThreadEventOccured = null;


        public MainWindowViewModel()
        {
            ThreadStartCommand = new Command(OnThreadStartCommand, OnExecThreadStartCommand);
            ThreadPauseCommand = new Command(OnThreadPauseCommand, OnExecThreadPauseCommand);
            ThreadStopCommand = new Command(OnThreadStopCommand, OnExecThreadStopCommand);
            TestCommand = new Command(OnTestCommand, OnExecTestCommand);

            _uiThread = SynchronizationContext.Current ?? DispatcherSynchronizationContext.Current;

            onThreadEventOccured += OnThreadEventOccured;
        }

        private void OnThreadStartCommand()
        {
            if (_thread?.IsAlive == true)
            {
                if (_pause?.IsSet is false)
                    _pause?.Set();

                return;
            }

            _cts = new CancellationTokenSource();
            _pause = new ManualResetEventSlim(true);
            _pause?.Set();
            _thread = new Thread(() => Run(_cts.Token, _pause, _uiThread))
            {
                IsBackground = true,
                Name = "Run"
            };
            _thread?.Start();
            OnPropertyChangedAll();
        }


        private bool OnExecThreadStartCommand()
        {
            if (_thread is null || _thread?.IsAlive is false)
                return true;
            else
            {
                if (_pause?.IsSet is false)
                    return true;
                else
                    return false;
            }
        }

        private void OnThreadPauseCommand()
        {
            _pause?.Reset();

            OnPropertyChangedAll();
        }

        private bool OnExecThreadPauseCommand()
        {
            if (_thread?.IsAlive == true)
            {
                if (_pause?.IsSet is true)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        private void OnThreadStopCommand()
        {
            if (_thread?.IsAlive == true)
            {
                _cts?.Cancel();
            }
            OnPropertyChangedAll();
        }

        private bool OnExecThreadStopCommand()
        {
            if (_thread?.IsAlive == true)
                return true;
            else
                return false;
        }


        // 백그라운드에서 숫자를 증가시키는 작업 스레드
        private Thread _threadTest;
        // 일시 정지/재개 제어 (true=실행, Reset()하면 Wait()에서 멈춤)
        private ManualResetEventSlim _pauseTest;
        // 중지 토큰 (정지 신호)
        private CancellationTokenSource _ctsTest;

        private void OnTestCommand()
        {
            if (_threadTest?.IsAlive is true)
            {
                if (_pauseTest?.IsSet is false)
                    _pauseTest?.Set();

                return;
            }


            _ctsTest = new CancellationTokenSource();
            _pauseTest = new ManualResetEventSlim(true);
            _pauseTest?.Set();
            _threadTest = new Thread(() => ThreadTest(_ctsTest.Token, _pauseTest, _uiThread))
            {
                IsBackground = true,
                Name = "ThreadTest",
            };
            _threadTest.Start();
        }

        private bool OnExecTestCommand()
        {
            if (_threadTest is null || _threadTest?.IsAlive is false)
                return true;
            else
            {
                if (_pauseTest?.IsSet is false)
                    return true;
                else
                    return false;
            }
        }

        private void OnThreadEventOccured(Object sender, int count)
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
               }, null);

                int count = 0;
                while (true)
                {
                    ListItem item = new ListItem(count, string.Format($"msg: {count}"), string.Format($"desc: {count}"));
                    uiThread?.Post(_ =>
                    {
                        ListMsg.Add(item);
                        Count = count;
                    }, null);
                    onThreadEventOccured?.Invoke(this, count);

                    count++;

                    Thread.Sleep(1000);
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


        /// <summary>
        /// thread
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pause"></param>
        /// <param name="uiThread"></param>
        private void ThreadTest(CancellationToken token, ManualResetEventSlim pause, SynchronizationContext uiThread)
        {
            try
            {
                if (0 < ListMsg.Count)
                {
                    ListItem itemReturn = ListMsg.FirstOrDefault((item) =>
                    {
                        if (item.Index == 1)
                            return true;

                        return false;
                    });

                    uiThread?.Post(_ =>
                    {
                        itemReturn.Message = "Find index 1";

                    }, null);
                }
                Thread.Sleep(1000);
                pause.Wait(token);
                token.ThrowIfCancellationRequested();
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
