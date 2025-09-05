using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Util;

namespace WpfTask
{
    public class MainWindowViewModel : Notifier
    {
        // 백그라운드에서 실행되는 작업(Task)
        private Task<int>? _workTask;

        // 메시지 표시용 ListBox
        public ObservableCollection<string> Logs { get; } = new();

        // 작업 취소를 제어하기 위한 토큰 소스
        private CancellationTokenSource? _cts;

        // 일시정지/재개를 제어하는 신호 (Pause -> Resume)
        private TaskCompletionSource<bool>? _resumeSignal;

        // 각 버튼에 연결할 Command
        public ICommand StartCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResumeCommand { get; }
        public ICommand StopCommand { get; }

        public MainWindowViewModel()
        {
            // Start 버튼: 실행 중인 작업이 없을 때만 가능
            StartCommand = new Command(Start, () => _workTask == null || _workTask.IsCompleted);

            // Pause 버튼: 작업이 실행 중이고, 아직 일시정지 상태가 아닐 때만 가능
            PauseCommand = new Command(Pause, () => _workTask != null && !_workTask.IsCompleted && _resumeSignal == null);

            // Resume 버튼: 현재 일시정지 상태일 때만 가능
            ResumeCommand = new Command(Resume, () => _resumeSignal != null);

            // Stop 버튼: 작업이 실행 중일 때만 가능
            StopCommand = new Command(Stop, () => _workTask != null && !_workTask.IsCompleted);
        }

        /// <summary>
        /// 1~10까지 1초 간격으로 출력하는 작업 시작
        /// </summary>
        private async void Start()
        {
            // 이미 실행 중이면 무시
            if (_workTask != null && _workTask.IsCompleted is false)
                return;

            // 기존 데이터 초기화
            Logs.Clear();

            // 새 취소 토큰 준비
            _cts = new CancellationTokenSource();

            // 백그라운드 카운터 작업 시작
            _workTask = CounterAsync(_cts.Token);
            int i = await _workTask;

            Logs.Add(string.Format($"Task result {i}"));

            CommandManager.InvalidateRequerySuggested();

            // 리소스 정리
            _cts?.Dispose();
            _cts = null;
            _workTask = null;
        }

        /// <summary>
        /// 일시정지 - Resume 신호를 기다리도록 설정
        /// </summary>
        private void Pause()
        {
            if (_workTask == null || _workTask.IsCompleted)
                return;
            if (_resumeSignal != null)
                return; // 이미 일시정지 중이면 무시

            // Resume될 때까지 대기할 수 있도록 새로운 TaskCompletionSource 생성
            _resumeSignal = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        /// <summary>
        /// 재시작 - 대기 중인 ResumeSignal을 완료시킴
        /// </summary>
        private void Resume()
        {
            if (_resumeSignal == null)
                return;

            // Pause 상태 해제 -> 대기 중인 Task를 계속 진행시킴
            _resumeSignal.TrySetResult(true);
            _resumeSignal = null;
        }

        /// <summary>
        /// 작업 중지
        /// </summary>
        private void Stop()
        {
            // 작업하는게 없으면
            if (_workTask == null || _workTask.IsCompleted is true)
                return;

            // 취소 신호 전달
            _cts?.Cancel();
            _resumeSignal?.TrySetResult(false);
            _resumeSignal = null;
        }

        /// <summary>
        /// 1~10까지 숫자를 1초마다 추가하는 비동기 작업
        /// </summary>
        private async Task<int> CounterAsync(CancellationToken token)
        {
            int res = 0;
            try
            {
                res = await Task.Run(async () =>
                {
                    _ = Application.Current.Dispatcher.BeginInvoke(() => Logs.Add("Start"));
                    int i = 0;
                    while (i < 10)
                    {
                        // 취소 요청 시 즉시 종료
                        token.ThrowIfCancellationRequested();

                        // 일시정지 상태라면 Resume 신호가 들어올 때까지 대기
                        if (_resumeSignal != null)
                            await _resumeSignal.Task;

                        // 숫자를 Items에 추가 (UI에 표시됨)
                        _ = Application.Current.Dispatcher.BeginInvoke(() => Logs.Add($"{i}"));
                        i++;

                        // 1초 대기
                        await Task.Delay(1000, token);
                    }

                    return i;
                }, token);
            }
            catch (OperationCanceledException)
            {
                _ = Application.Current.Dispatcher.BeginInvoke(() => Logs.Add("catch OperationCanceledException"));
            }
            catch (Exception e)
            {
                _ = Application.Current.Dispatcher.BeginInvoke(() => Logs.Add($"catch Exception: {e.Message}"));
            }
            finally
            {
                // 루프가 끝나면 일시정지 신호 초기화
                _resumeSignal = null;
            }
            return res;
        }
    }
}
