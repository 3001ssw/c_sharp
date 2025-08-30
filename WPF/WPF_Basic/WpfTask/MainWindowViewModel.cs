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
        private CancellationTokenSource? _cts;
        private Task? _workTask;
        private TaskCompletionSource<bool>? _resumeSignal;

        public ObservableCollection<int> Items { get; } = new();

        public ICommand StartCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResumeCommand { get; }
        public ICommand StopCommand { get; }

        public MainWindowViewModel()
        {
            StartCommand = new Command(_ => Start(), _ => _workTask == null || _workTask.IsCompleted);
            PauseCommand = new Command(_ => Pause(), _ => _workTask != null && !_workTask.IsCompleted && _resumeSignal == null);
            ResumeCommand = new Command(_ => Resume(), _ => _resumeSignal != null);
            StopCommand = new Command(_ => Stop(), _ => _workTask != null && !_workTask.IsCompleted);
        }

        private void Start()
        {
            if (_workTask != null && !_workTask.IsCompleted) return;

            Items.Clear();
            _cts = new CancellationTokenSource();

            _workTask = RunCounterAsync(_cts.Token);
            RaiseCanExecutes();
        }

        private void Pause()
        {
            if (_workTask == null || _workTask.IsCompleted) return;
            if (_resumeSignal != null) return; // 이미 pause 상태

            _resumeSignal = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            RaiseCanExecutes();
        }

        private void Resume()
        {
            if (_resumeSignal == null) return;

            _resumeSignal.TrySetResult(true);
            _resumeSignal = null;
            RaiseCanExecutes();
        }

        private async void Stop()
        {
            if (_workTask == null) return;

            _cts?.Cancel();
            try { await _workTask; }
            catch (OperationCanceledException) { }

            _cts?.Dispose();
            _cts = null;
            _resumeSignal = null;
            _workTask = null;
            RaiseCanExecutes();
        }

        private async Task RunCounterAsync(CancellationToken token)
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    token.ThrowIfCancellationRequested();

                    // pause 상태면 여기서 resumeSignal이 SetResult될 때까지 대기
                    if (_resumeSignal != null)
                    {
                        await _resumeSignal.Task;
                    }

                    Items.Add(i);

                    await Task.Delay(1000, token);
                }
            }
            finally
            {
                _resumeSignal = null;
                RaiseCanExecutes();
            }
        }

        private void RaiseCanExecutes()
        {
            (StartCommand as Command)?.RaiseCanExecuteChanged();
            (PauseCommand as Command)?.RaiseCanExecuteChanged();
            (ResumeCommand as Command)?.RaiseCanExecuteChanged();
            (StopCommand as Command)?.RaiseCanExecuteChanged();
        }
    }
}

