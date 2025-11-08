using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Util;

namespace WpfThread
{
    public class MainWindowViewModel : Notifier
    {
        // 화면에 표시할 숫자 목록 (UI에 바인딩됨, DataGrid/ListView 등과 연결)
        public ObservableCollection<int> Numbers { get; set; } = new ObservableCollection<int>();

        public Command StartCommand { get; }
        public Command PauseCommand { get; }
        public Command StopCommand { get; }

        // 백그라운드에서 숫자를 증가시키는 작업 스레드
        private Thread? _thread;
        // 일시 정지/재개 제어 (true=실행, Reset()하면 Wait()에서 멈춤)
        private ManualResetEventSlim? _pause;
        // 중지 토큰 (정지 신호)
        private CancellationTokenSource? _cts;

        // UI 스레드 컨텍스트 (Dispatcher 대신 사용)
        private readonly SynchronizationContext? _uiThread;

        private string _statusText = "대기";
        public string StatusText
        {
            get => _statusText;
            private set => Set(ref _statusText, value);
        }

        public MainWindowViewModel()
        {
            _uiThread = SynchronizationContext.Current ?? DispatcherSynchronizationContext.Current;

            StartCommand = new Command(OnStart);
            PauseCommand = new Command(OnPause);
            StopCommand = new Command(Stop);
        }

        private void OnStart()
        {
            // 동작 중이면 재개
            if (_thread?.IsAlive is true)
            {
                _pause?.Set();
                StatusText = "실행(재개)";
                return;
            }

            // 새 실행
            _cts = new CancellationTokenSource();
            _pause = new ManualResetEventSlim(true);
            _pause.Set();
            StatusText = "실행(시작)";

            _thread = new Thread(() => Loop(_cts!.Token, _pause, _uiThread))
            {
                IsBackground = true,
                Name = "NumberRunner-Thread"
            };
            _thread.Start();
        }

        private void OnPause()
        {
            _pause?.Reset();
            StatusText = "일시정지";
        }

        private void Stop()
        {
            _cts?.Cancel();
            _uiThread?.Post(_ => Numbers.Clear(), null);
            StatusText = "중지";

            if (_thread?.IsAlive is true )
                _thread?.Join();
            _thread = null;
            _cts = null;
            _pause = null;
        }

        private void Loop(CancellationToken token, ManualResetEventSlim pause, SynchronizationContext uiThread)
        {
            try
            {
<<<<<<< HEAD
                ObservableCollection<int> numbers = new ObservableCollection<int>();
                int current = 0;
                for (int i = current + 1; i <= 10; i++)
                {
                    pause.Wait(token); // 일시정지 시에도 token으로 깨어남

=======
                token.ThrowIfCancellationRequested();

                int _current = 0;
                for (int i = _current + 1; i <= 10; i++)
                {
>>>>>>> e05555d3cc31d1db09c5d23578fc5e941aba72c4
                    int v = i;
                    numbers.Add(v);
                    current = v;

<<<<<<< HEAD
                    if (WaitHandle.WaitAny(new[] { token.WaitHandle }, 1000) != WaitHandle.WaitTimeout)
=======
                    _pause.Wait(token); // 일시정지 시에도 token으로 깨어남

                    if (WaitHandle.WaitAny(new WaitHandle[] { token.WaitHandle }, 1000) != WaitHandle.WaitTimeout)
>>>>>>> e05555d3cc31d1db09c5d23578fc5e941aba72c4
                        break;
                }

                if (token.IsCancellationRequested is true)
                {
                    uiThread?.Post(_ => StatusText = "취소됨", null);
                }
                else
                {
                    if (100 <= current)
                        uiThread?.Post(_ => StatusText = "완료", null);
                    else
                        uiThread?.Post(_ => StatusText = "완료", null);
                }
                uiThread?.Post(_ => Numbers = numbers, null);
            }
            catch (OperationCanceledException e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                uiThread?.Post(_ => Refresh(), null);
            }
        }

        public void Refresh()
        {
            OnPropertyChanged("");
        }
    }
}
