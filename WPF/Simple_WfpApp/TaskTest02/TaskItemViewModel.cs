using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace TaskTest02
{
    public class TaskItemViewModel : Notifier
    {
        public string Title { get; set; } = "";

        public int Progress { get; set; } = 0;

        public string Status { get; set; } = "";

        public string Ect { get; set; } = "";

        public int Delay { get; set; } = 100;

        public TaskItemViewModel()
        {

        }

        public async Task StartProgressAsync(CancellationToken token)
        {
            Status = "시작";

            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    Progress = i;
                    Status = $"{Progress} 진행 중";
                    Ect = $"Delay: {Delay}";

                    OnPropertyChangedAll();
                    await Task.Delay(Delay, token);
                    token.ThrowIfCancellationRequested();
                }

                Status = "완료";
            }
            catch (TaskCanceledException)
            {
                Status = $"취소 됨";
            }
            finally
            {
                OnPropertyChangedAll();
            }
        }
    }
}
