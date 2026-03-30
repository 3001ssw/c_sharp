using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfHttpReqResp
{
    public class MainViewModel : BindableBase
    {
        private static readonly HttpClient client = new HttpClient();
        public DelegateCommand PostCommand { get; private set; }

        private string responeText = "";
        public string ResponeText { get => responeText; set => SetProperty(ref responeText, value); }

        public MainViewModel()
        {
            PostCommand = new DelegateCommand(OnPost, CanPost);
        }

        private async void OnPost()
        {
            ResponeText = "";
            var newUser = new UserInfo { Name = "Gemini", Job = "AI" };

            // 객체를 JSON으로 직렬화
            string json = JsonSerializer.Serialize(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // POST 요청 전송
            var response = await client.PostAsync("https://httpbin.org/post", content);

            if (response.IsSuccessStatusCode)
            {
                ResponeText = await response.Content.ReadAsStringAsync();
            }
        }

        private bool CanPost()
        {
            return true;
        }
    }
}
