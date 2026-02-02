using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfTasks
{
    public class MainViewModel : BindableBase
    {
        private int count = 0;
        public int Count
        {
            get => count;
            set
            {
                SetProperty(ref count, value);
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand StartCommand { get; }
        public DelegateCommand StopCommand { get; }

        private ObservableCollection<Counter> counters = new ObservableCollection<Counter>();
        public ObservableCollection<Counter> Counters { get => counters; set => SetProperty(ref counters, value); }


        private CancellationTokenSource? cts = null;

        public MainViewModel()
        {
            StartCommand = new DelegateCommand(OnStartCommand, CanStartCommand);
            StopCommand = new DelegateCommand(OnStopCommand, CanStopCommand);
        }


        private async void OnStartCommand()
        {
            cts = new CancellationTokenSource();
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();

            Counters.Clear();
            for (int i = 0; i < Count; i++)
            {
                Counters.Add(new Counter());
            }

            try
            {
                //var ret = await Task.WhenAll(Counters.Select(counter => RunCounter(counter, token)));
                var ret = await Task.WhenAll(Counters.Select(counter =>
                Task.Run(async () =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        counter.Count++;
                        await Task.Delay(counter.Sleep);
                        //Thread.Sleep(counter.Sleep);
                    }
                    cts.Token.ThrowIfCancellationRequested();

                    return counter.Count;
                }, cts.Token)));
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {
                cts?.Dispose();
                cts = null;
            }
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
        }

        private bool CanStartCommand()
        {
            if (0 < Count && cts == null)
                return true;
            else
                return false;
        }

        private void OnStopCommand()
        {
            cts?.Cancel(true);
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
        }

        private bool CanStopCommand()
        {
            if (cts != null)
                return true;
            else
                return false;
        }

        private Task<int> RunCounter(Counter counter, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        try
                        {
                            counter.Count++;
                            await Task.Delay(counter.Sleep);
                            //Thread.Sleep(counter.Sleep);
                        }
                        catch (TaskCanceledException)
                        {
                            break;
                        }
                    }
                    token.ThrowIfCancellationRequested();

                    return counter.Count;
                }
                catch (OperationCanceledException)
                {
                    return counter.Count;
                }
                catch (Exception)
                {
                    return -1;
                }
            }, token);
        }
    }
}
