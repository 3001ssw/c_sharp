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
        public DelegateCommand GetCommand { get; private set; }
        public DelegateCommand PostCommand { get; private set; }
        public DelegateCommand PutCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }

        private string responeText = "";
        public string ResponeText { get => responeText; set => SetProperty(ref responeText, value); }

        public MainViewModel()
        {
            GetCommand = new DelegateCommand(OnGet, CanGet);
            PostCommand = new DelegateCommand(OnPost, CanPost);
            PutCommand = new DelegateCommand(OnPut, CanPut);
            DeleteCommand = new DelegateCommand(OnDelete, CanDelete);
        }

        private async void OnGet()
        {
            try
            {
                ResponeText = "조회 중...";
                string targetName = "Gemini";

                // URL 뒤에 ?key=value 형태로 붙입니다.
                // 결과: https://httpbin.org/get?name=Gemini
                var response = await client.GetAsync($"https://httpbin.org/get?name={targetName}");

                if (response.IsSuccessStatusCode)
                {
                    ResponeText = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                ResponeText = $"에러 발생: {ex.Message}";
            }
        }

        private bool CanGet()
        {
            return true;
        }

        private async void OnPost()
        {
            try
            {
                ResponeText = "요청 중...";
                var newUser = new UserInfo { Name = "Gemini", Job = "AI" };
                string json = JsonSerializer.Serialize(newUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://httpbin.org/post", content);
                ResponeText = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                ResponeText = $"에러 발생: {ex.Message}";
            }
        }

        private bool CanPost()
        {
            return true;
        }

        private async void OnPut()
        {
            try
            {
                ResponeText = "요청 중...";
                var updatedUser = new UserInfo { Name = "Gemini", Job = "Updated AI" };
                string json = JsonSerializer.Serialize(updatedUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("https://httpbin.org/put", content);
                ResponeText = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                ResponeText = $"에러 발생: {ex.Message}";
            }
        }

        private bool CanPut()
        {
            return true;
        }

        private async void OnDelete()
        {
            try
            {
                ResponeText = "삭제 요청 중 (Query)...";
                string targetName = "Gemini";

                // URL에 직접 포함: https://httpbin.org/delete?name=Gemini
                var response = await client.DeleteAsync($"https://httpbin.org/delete?name={targetName}");

                if (response.IsSuccessStatusCode)
                {
                    ResponeText = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                ResponeText = $"에러: {ex.Message}";
            }
        }

        private bool CanDelete()
        {
            return true;
        }
    }
}
