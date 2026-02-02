using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using WpfTask.Util;

namespace WpfTask.ViewModels
{
    public class TabItemSimpleTaskViewModel : TabItemBaseViewModel
    {
        #region fields, properties
        private object _lockLogs = new object(); // 동기화를 위한 자물쇠

        private ObservableCollection<Log> logs = new ObservableCollection<Log>();
        public ObservableCollection<Log> Logs { get => logs; set => SetProperty(ref logs, value); }


        private int count = 0;
        public int Count { get => count; set => SetProperty(ref count, value); }

        private string res = "";
        public string Result { get => res; set => SetProperty(ref res, value); }


        private Task? _task = null;
        private Task<int>? _taskIntResult = null;

        #endregion


        #region commands

        public DelegateCommand Test01Command { get; private set; }
        public DelegateCommand Test02Command { get; private set; }
        public DelegateCommand Test03Command { get; private set; }
        public DelegateCommand Test04Command { get; private set; }

        private async void OnTest01()
        {
            // Task 변수 없이 시작
            Logs.Clear();
            try
            {
                await Task.Run(async () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Logs.Add(new Log()
                        {
                            Message = $"{i}",
                        });
                        await Task.Delay(100);
                    }
                });
            }
            finally
            {
            }
        }

        private async void OnTest02()
        {
            // Task 변수 사용
            Logs.Clear();
            try
            {
                _task = Task.Run(async () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Logs.Add(new Log()
                        {
                            Message = $"{i}",
                        });
                        await Task.Delay(100);
                    }
                });
                UpdateAllButton();
                await _task;
            }
            catch (Exception ex)
            {
                // 에러가 났을 때 로그를 남기는 등 예외 처리 가능
                Logs.Add(new Log() { Message = $"에러 발생: {ex.Message}" });
            }
            finally
            {
                UpdateAllButton();
                _task = null;
            }
        }

        private bool CanTest02()
        {
            if (_task == null || _task?.IsCanceled == true || _task?.IsCompleted == true)
                return true;

            return false;
        }

        private async void OnTest03()
        {
            Logs.Clear();

            try
            {
                _task = Task.Run(async () =>
                {
                    // 바깥 Count 속성 사용
                    for (int i = 0; i < Count; i++)
                    {
                        Logs.Add(new Log()
                        {
                            Message = $"{i}",
                        });
                        await Task.Delay(100);
                    }
                });
                UpdateAllButton();
                await _task;
            }
            finally
            {
                _task = null;
                UpdateAllButton();
            }
        }

        private bool CanTest03()
        {
            if (_task == null || _task?.IsCanceled == true || _task?.IsCompleted == true)
                return true;

            return false;
        }

        private async void OnTest04()
        {
            int res = 0;
            try
            {
                _taskIntResult = Task.Run(async () =>
                {
                    // 바깥 Count 속성 사용
                    for (int i = 0; i < Count; i++)
                        {
                            Logs.Add(new Log()
                            {
                                Message = $"{i}",
                            });
                            await Task.Delay(100);
                        }
                        return Count;
                });
                UpdateAllButton();
                res = await _taskIntResult;
            }
            finally
            {
                _taskIntResult = null;
                Result = $"Count : {res}";
                UpdateAllButton();
            }
        }

        private bool CanTest04()
        {
            if (_taskIntResult == null || _taskIntResult?.IsCanceled == true || _taskIntResult?.IsCompleted == true)
                return true;

            return false;
        }

        #endregion


        #region constructor
        public TabItemSimpleTaskViewModel()
        {
            Header = "Simple Task";

            BindingOperations.EnableCollectionSynchronization(Logs, _lockLogs);

            Test01Command = new DelegateCommand(OnTest01);
            Test02Command = new DelegateCommand(OnTest02, CanTest02);
            Test03Command = new DelegateCommand(OnTest03, CanTest03);
            Test04Command = new DelegateCommand(OnTest04, CanTest04);
        }
        #endregion

        #region functions

        private void UpdateAllButton()
        {
            Test01Command.RaiseCanExecuteChanged();
            Test02Command.RaiseCanExecuteChanged();
            Test03Command.RaiseCanExecuteChanged();
        }

        #endregion

    }
}
