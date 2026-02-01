using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTask.Util;

namespace WpfTask.ViewModels
{
    public class TabItemFuncTaskViewModel : TabItemBaseViewModel
    {
        private object _lockLogs = new object(); // 동기화를 위한 자물쇠

        private ObservableCollection<Log> logs = new ObservableCollection<Log>();
        public ObservableCollection<Log> Logs { get => logs; set => SetProperty(ref logs, value); }

        public Func<Task?> _func1
        {
            get => async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Logs.Add(new Log()
                    {
                        Message = $"{i}",
                    });
                    await Task.Delay(100);
                }
            };
        }

        public Func<Task?> _func2
        {
            get => async () =>
            {
                for (int i = 10; i < 20; i++)
                {
                    Logs.Add(new Log()
                    {
                        Message = $"{i}",
                    });
                    await Task.Delay(100);
                }
            };
        }

        //private void temp ()
        //{
        //    Logs.Clear();
        //    try
        //    {
        //        if (bTrigger)
        //            _task2 = Task.Run(_func1);
        //        else
        //            _task2 = Task.Run(_func2);
        //        bTrigger = !bTrigger;
        //
        //        Test02Command.RaiseCanExecuteChanged();
        //        Test03Command.RaiseCanExecuteChanged();
        //        await _task2;
        //    }
        //    catch (Exception ex)
        //    {
        //        // 에러가 났을 때 로그를 남기는 등 예외 처리 가능
        //        Logs.Add(new Log() { Message = $"에러 발생: {ex.Message}" });
        //    }
        //    finally
        //    {
        //        Test02Command.RaiseCanExecuteChanged();
        //        Test03Command.RaiseCanExecuteChanged();
        //        _task2 = null;
        //    }
        //}

        #region constructor
        public TabItemFuncTaskViewModel()
        {
            Header = "Func Task";
        }
        #endregion
    }
}
