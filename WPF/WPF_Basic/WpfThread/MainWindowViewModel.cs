using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        // 일시정지/재개 제어 (true=실행, Reset()하면 Wait()에서 멈춤)
        private readonly ManualResetEventSlim _pause = new ManualResetEventSlim(true);
        private volatile bool _threadStop;
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
            _threadStop = false;
            _pause.Set();
            StatusText = "실행(시작)";

            _thread = new Thread(Loop)
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
            _threadStop = true;
            _pause.Set();      // 정지 중 깨어서 종료되게
            uiThread?.Post(_ => Numbers.Clear(), null);
            StatusText = "중지";

            if (_thread is { IsAlive: true })
            {
                // 짧게 종료 대기
                _thread.Join(200);
            }
            _thread = null;
        }

        private void Loop()
        {
            try
            {
                int _current = 0;
                for (int i = _current + 1; i <= 100; i++)
                {
                    if (_threadStop)
                        break;

                    _pause.Wait();       // 일시정지 지원
                    if (_threadStop)
                        break;

                    int v = i;
                    uiThread?.Post(_ => Numbers.Add(v), null);
                    _current = v;

                    Thread.Sleep(80);    // 데모용 지연
                }

                if (!_threadStop && _current >= 100)
                    uiThread?.Post(_ => StatusText = "완료", null);
            }
            finally
            {
                _thread = null;
            }
        }
    }
}
