using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfTask.Util;

namespace WpfTask.ViewModels
{
    public class TabItemTaskViewModel : TabItemBaseViewModel
    {
        #region fields, properties

        // 메시지 표시용 ListBox
        private ObservableCollection<Log> logs = new ObservableCollection<Log>();
        public ObservableCollection<Log> Logs { get => logs; set => SetProperty(ref logs, value); }

        private object _lockLogs = new object();

        // 백그라운드에서 실행되는 작업(Task)
        private Task<int>? _workTask;

        // 작업 취소를 제어하기 위한 토큰 소스
        private CancellationTokenSource? _cts;

        // 일시정지/재개를 제어하는 신호 (Pause -> Resume)
        private TaskCompletionSource<bool>? _resumeSignal;

        #endregion

        #region commands

        // 각 버튼에 연결할 Command
        public DelegateCommand StartCommand { get; }
        public DelegateCommand PauseCommand { get; }
        public DelegateCommand ResumeCommand { get; }
        public DelegateCommand StopCommand { get; }


        #endregion

        /// <summary>
        /// 1~10까지 1초 간격으로 출력하는 작업 시작
        /// </summary>
        private async void OnStart()
        {
            // 이미 실행 중이면 무시
            if (_workTask != null && _workTask.IsCompleted is false)
                return;

            // 기존 데이터 초기화
            Logs.Clear();


            // 백그라운드 카운터 작업 시작
            int i = 0;
            try
            {
                // 새 취소 토큰 준비
                _cts = new CancellationTokenSource();
                _workTask = Task.Run(async () =>
                {
                    int i = 0;
                    Logs.Add(new Log("Start"));
                    while (_cts.IsCancellationRequested == false)
                    {
                        // 숫자를 Items에 추가 (UI에 표시됨)
                        Logs.Add(new Log($"{i}"));
                        i++;

                        // 일시정지 상태라면 Resume 신호가 들어올 때까지 대기
                        if (_resumeSignal != null)
                            await _resumeSignal.Task;

                        // 1초 대기
                        await Task.Delay(100, _cts.Token);
                        // 취소 요청 시 즉시 종료
                        _cts.Token.ThrowIfCancellationRequested();
                    }

                    return i;
                }, _cts.Token);
                UpdateUi();
                i = await _workTask;
            }
            catch (OperationCanceledException)
            {
                Logs.Add(new Log("catch OperationCanceledException"));
            }
            catch (Exception e)
            {
                Logs.Add(new Log($"catch Exception: {e.Message}"));
            }
            finally
            {
                // 리소스 정리
                _resumeSignal = null;
                _cts?.Dispose();
                _cts = null;
                _workTask = null;
                UpdateUi();

                Logs.Add(new Log($"Task result {i}"));
            }
        }

        /// <summary>
        /// 일시정지 - Resume 신호를 기다리도록 설정
        /// </summary>
        private void OnPause()
        {
            if (_workTask == null || _workTask.IsCompleted)
                return;
            if (_resumeSignal != null)
                return; // 이미 일시정지 중이면 무시

            // Resume될 때까지 대기할 수 있도록 새로운 TaskCompletionSource 생성
            _resumeSignal = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            UpdateUi();
        }

        /// <summary>
        /// 재시작 - 대기 중인 ResumeSignal을 완료시킴
        /// </summary>
        private void OnResume()
        {
            if (_resumeSignal == null)
                return;

            // Pause 상태 해제 -> 대기 중인 Task를 계속 진행시킴
            _resumeSignal.TrySetResult(true);
            _resumeSignal = null;
            UpdateUi();
        }

        /// <summary>
        /// 작업 중지
        /// </summary>
        private void OnStop()
        {
            // 작업하는게 없으면
            if (_workTask == null || _workTask.IsCompleted is true)
                return;

            _resumeSignal?.TrySetResult(true);
            // 취소 신호 전달
            _cts?.Cancel();
            UpdateUi();
        }

        public TabItemTaskViewModel()
        {
            Header = "Start, Pause, Resume, Stop";

            BindingOperations.EnableCollectionSynchronization(Logs, _lockLogs);

            // Start 버튼: 실행 중인 작업이 없을 때만 가능
            StartCommand = new DelegateCommand(OnStart, () => _workTask == null || _workTask.IsCompleted);

            // Pause 버튼: 작업이 실행 중이고, 아직 일시정지 상태가 아닐 때만 가능
            PauseCommand = new DelegateCommand(OnPause, () => _workTask != null && !_workTask.IsCompleted && _resumeSignal == null);

            // Resume 버튼: 현재 일시정지 상태일 때만 가능
            ResumeCommand = new DelegateCommand(OnResume, () => _resumeSignal != null);

            // Stop 버튼: 작업이 실행 중일 때만 가능
            StopCommand = new DelegateCommand(OnStop, () => _workTask != null && !_workTask.IsCompleted);
        }

        #region functions

        private void UpdateUi()
        {
            StartCommand.RaiseCanExecuteChanged();
            PauseCommand.RaiseCanExecuteChanged();
            ResumeCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
        }

        #endregion

    }
}
