using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WpfTask.Converters;
using WpfTask.Util;

namespace WpfTask.ViewModels
{
    public class TabItemFuncTaskViewModel : TabItemBaseViewModel
    {
        private FunctionType selectedFunctionType = FunctionType.Func1;
        public FunctionType SelectedFunctionType { get => selectedFunctionType; set => SetProperty(ref selectedFunctionType, value); }

        private object _lockLogs = new object(); // 동기화를 위한 자물쇠

        private ObservableCollection<string> logs = new ObservableCollection<string>();
        public ObservableCollection<string> Logs { get => logs; set => SetProperty(ref logs, value); }

        private Task? _task = null;

        public Func<Task?> _func1
        {
            get => async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Logs.Add($"{i}");
                    await Task.Delay(100);
                }
            };
        }

        public Func<Task?> _func2
        {
            get => async () =>
            {
                for (int i = 100; i < 110; i++)
                {
                    Logs.Add($"{i}");
                    await Task.Delay(100);
                }
            };
        }

        #region command methods

        public DelegateCommand Test01Command { get; private set; }

        private async void OnTest01()
        {
            Func<Task?> fnGet = null;
            if (SelectedFunctionType == FunctionType.Func1)
                fnGet = _func1;
            else
                fnGet = _func2;

                Logs.Clear();
            try
            {
                _task = Task.Run(fnGet);
                UpdateUi();
                await _task;
            }
            catch (Exception ex)
            {
                // 에러가 났을 때 로그를 남기는 등 예외 처리 가능
                Logs.Add($"에러 발생: {ex.Message}");
            }
            finally
            {
                _task = null;
                UpdateUi();
            }


        }

        private bool CanTest01()
        {
            if (_task == null || _task?.IsCanceled == true || _task?.IsCompleted == true)
                return true;

            return false;
        }

        #endregion


        #region constructor
        public TabItemFuncTaskViewModel()
        {
            Header = "Func Task";

            BindingOperations.EnableCollectionSynchronization(Logs, _lockLogs);
            Test01Command = new DelegateCommand(OnTest01, CanTest01);
        }
        #endregion

        #region functions

        private void UpdateUi()
        {
            Test01Command.RaiseCanExecuteChanged();
        }

        #endregion
    }
}
