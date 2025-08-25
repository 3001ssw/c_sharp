using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Util;

namespace WpfTask
{
    public class MainWindowViewModel : Notifier
    {
        // ListView 바인딩 대상
        public ObservableCollection<string> Logs { get; }

        // 버튼 커맨드
        public Command CommandStart { get; }
        public Command CommandPause { get; }
        public Command CommandStop { get; }

        // 취소 토큰
        private CancellationTokenSource? _cts = null;


        // 일시정지/재개 신호
        private TaskCompletionSource<bool> _resumeSignal;

        // 현재 일시정지 여부
        private bool _isPaused;

        public MainWindowViewModel()
        {
            Logs = new ObservableCollection<string>();

            CommandStart = new Command(OnStart);
            CommandPause = new Command(OnPause);
            CommandStop = new Command(OnStop);

            _resumeSignal = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            _resumeSignal.SetResult(true); // 기본은 '열림(진행 가능)' 상태
        }

        private async void OnStart()
        {
            // 실행 중일 때는 새로 시작하지 않음
            if (_cts != null)
                return;

            _cts = new CancellationTokenSource();
            await RunWorkAsync(_cts.Token);
            Logs.Add($"[{DateTime.Now:HH:mm:ss}] OnStart 종료");
        }

        private void OnPause()
        {
            if (_cts == null)
                return; // 실행중이 아니면 무시

            if (!_isPaused)
            {
                // 일시정지: 새 미완료 TCS로 갈아끼워서 대기시키기
                _isPaused = true;
                _resumeSignal = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                Logs.Add($"[{DateTime.Now:HH:mm:ss}] Pause");
            }
            else
            {
                // 재개: 신호 완료시켜서 대기중인 작업들 통과시키기
                _isPaused = false;
                _resumeSignal.TrySetResult(true);
                Logs.Add($"[{DateTime.Now:HH:mm:ss}] Resume");
            }
        }

        private void OnStop()
        {
            if (_cts != null)
            {
                _resumeSignal?.TrySetResult(true);

                _cts.Cancel();   // 취소 신호 보냄
                _cts.Dispose();
                _cts = null;

                Logs.Add($"[{DateTime.Now:HH:mm:ss}] Stop (취소 요청됨)");
            }
        }


        // ===== 실제 작업 =====
        private async Task RunWorkAsync(CancellationToken token)
        {
            Logs.Add($"[{DateTime.Now:HH:mm:ss}] Start");

            Task[] tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                int id = i; // 캡처 문제 방지
                tasks[i] = Task.Run(async () =>
                {
                    // UI에 로그 추가
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Logs.Add($"[{DateTime.Now:HH:mm:ss}] Task {id} 시작");
                    });

                    await Task.Delay(5000, token); // 토큰 연결

                    // 일시정지 중이면 여기서 대기 (Resume 시 통과)
                    await _resumeSignal.Task;

                    // UI에 로그 추가
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Logs.Add($"[{DateTime.Now:HH:mm:ss}] Task {id} 완료");
                    });
                }, token);
            }

            try
            {
                await Task.WhenAll(tasks);
                Logs.Add($"[{DateTime.Now:HH:mm:ss}] 모든 Task 완료");
            }
            catch (OperationCanceledException e)
            {
                
                Logs.Add($"[{DateTime.Now:HH:mm:ss}] 모든 Task 취소됨({e.Message})");
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
            Logs.Add($"[{DateTime.Now:HH:mm:ss}] 모든 Task 완료");
        }
    }
}
