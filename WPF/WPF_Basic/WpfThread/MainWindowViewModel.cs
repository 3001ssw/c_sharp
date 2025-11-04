using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace WpfThread
{
    public class MainWindowViewModel : Notifier
    {
        // 화면에 표시할 숫자 목록 (UI에 바인딩됨, DataGrid/ListView 등과 연결)
        public ObservableCollection<int> Numbers { get; } = new ObservableCollection<int>();

        public Command StartCommand { get; }
        public Command PauseCommand { get; }
        public Command StopCommand { get; }

        // 백그라운드에서 숫자를 증가시키는 작업 스레드
        private Thread? _thread;
        // 일시 정지/재개 제어 (true=실행, Reset()하면 Wait()에서 멈춤)
        private readonly ManualResetEventSlim _pause = new ManualResetEventSlim(true);
        // 중지 토큰 (정지 신호)
        private CancellationTokenSource? _cts;

        // UI 스레드 컨텍스트 (Dispatcher 대신 사용)
        private readonly SynchronizationContext? uiThread;

        private string _statusText = "대기";
        public string StatusText
        {
            get => _statusText;
            private set => Set(ref _statusText, value);
        }

        public MainWindowViewModel()
        {
            uiThread = SynchronizationContext.Current;

            StartCommand = new Command(OnStart);
            PauseCommand = new Command(OnPause);
            StopCommand = new Command(Stop);
        }

        private void OnStart()
        {
            // 동작 중이면 재개
            if (_thread?.IsAlive is true)
            {
                _pause.Set();
                StatusText = "실행(재개)";
                return;
            }

            // 새 실행
            _cts = new CancellationTokenSource();
            _pause.Set();
            StatusText = "실행(시작)";

            _thread = new Thread(() => Loop(_cts!.Token))
            {
                IsBackground = true,
                Name = "NumberRunner-Thread"
            };
            _thread.Start();
        }

        private void OnPause()
        {
            _pause.Reset();
            StatusText = "일시정지";
        }

        private void Stop()
        {
            _cts?.Cancel();
            uiThread?.Post(_ => Numbers.Clear(), null);
            StatusText = "중지";

            if (_thread is { IsAlive: true })
            {
                // 짧게 종료 대기
                _thread.Join(200);
            }
            _thread = null;
        }

        private void Loop(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                int _current = 0;
                for (int i = _current + 1; i <= 10; i++)
                {
                    int v = i;
                    uiThread?.Post(_ => Numbers.Add(v), null);
                    _current = v;

                    _pause.Wait(token); // 일시정지 시에도 token으로 깨어남

                    if (WaitHandle.WaitAny(new WaitHandle[] { token.WaitHandle }, 1000) != WaitHandle.WaitTimeout)
                        break;
                }

                if (token.IsCancellationRequested is true)
                {
                    uiThread?.Post(_ => StatusText = "취소됨", null);
                }
                else
                {
                    if (100 <= _current)
                        uiThread?.Post(_ => StatusText = "완료", null);
                    else
                        uiThread?.Post(_ => StatusText = "완료", null);
                }
            }
            catch (OperationCanceledException e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                _thread = null;
            }
        }
    }
}
