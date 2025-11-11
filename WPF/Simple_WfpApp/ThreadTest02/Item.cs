using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace ThreadTest02
{
    public class Item : Notifier
    {
        public int Index { get; set; } = 0;

        private string message = "";
        public string Message { get => message; set => OnPropertyChanged(ref message, value); }

        private string description = "";
        public string Description { get => description; set => OnPropertyChanged(ref description, value); }

        public Item(int index, string message, string description)
        {
            Index = index;
            Message = message;
            Description = description;
        }

        public async Task StartWorkAsync(CancellationToken token, SemaphoreSlim pause)
        {
            int disp = Index;
            while(!token.IsCancellationRequested)
            {
                Debug.WriteLine($"================ {Index}: disp: {disp}===============");

                Debug.WriteLine($"{Index}: semaphore current count: {pause.CurrentCount}");
                await pause.WaitAsync(token);

                Debug.WriteLine($"{Index}: after WaitAsync, semaphore current count: {pause.CurrentCount}");

                Message = $"message: {disp}";
                Description = $"discription: {disp}";

                Debug.WriteLine($"{Index}: complete job");

                pause.Release();
                Debug.WriteLine($"{Index}: after release, semaphore current count: {pause.CurrentCount}");

                Random random = new Random();
                int delay = random.Next(10, 1001); // 10 이상, 1000 이하
                Debug.WriteLine($"{Index}: delay: {delay}");
                await Task.Delay(delay, token);
                disp++;
            }
        }
    }
}
