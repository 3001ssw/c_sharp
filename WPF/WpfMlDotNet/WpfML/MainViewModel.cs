using Microsoft.Win32;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfML
{
    public class MainViewModel : BindableBase
    {
        private string resultText = "";
        public string ResultText { get => resultText; set => SetProperty(ref resultText, value); }

        public DelegateCommand SelectImageCommand { get; private set; }
        private void OnSelectImage()
        {
            // 1. 파일 선택 대화상자(탐색기) 설정 및 열기
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "분류할 이미지 선택",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp", // 이미지 파일만 선택하도록 필터링
                Multiselect = false
            };

            // 사용자가 파일을 선택하고 '열기'를 눌렀을 때
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                ResultText = "이미지 분석 중...";

                try
                {
                    // 2. ML.NET 모델에 넣을 입력 데이터 생성
                    // (최신 Model Builder는 이미지를 byte[] 형태로 요구하는 경우가 많습니다)
                    var modelInput = new MLModel1.ModelInput()
                    {
                        ImageSource = File.ReadAllBytes(selectedFilePath)
                        // 만약 위 줄에서 빨간 밑줄(타입 오류)이 난다면, 
                        // 모델이 경로(string)를 요구하는 구버전일 수 있습니다. 
                        // 그럴 때는 아래 코드로 변경해 주세요:
                        // ImageSource = selectedFilePath
                    };

                    // 3. 모델을 통해 이미지 분류 예측 실행
                    var result = MLModel1.Predict(modelInput);

                    // 4. 결과를 UI에 바인딩된 ResultText에 업데이트
                    ResultText = $"[분류 완료]\n" +
                                 $"선택한 파일: {Path.GetFileName(selectedFilePath)}\n" +
                                 $"예측된 결과: {result.PredictedLabel}\n" +
                                 $"확률: {result.Score.Max()}";
                }
                catch (Exception ex)
                {
                    ResultText = $"분류 중 오류가 발생했습니다.\n{ex.Message}";
                }
            }
        }

        private bool CanSelectImage()
        {
            return true;
        }

        public MainViewModel()
        {
            SelectImageCommand = new DelegateCommand(OnSelectImage, CanSelectImage);
        }
    }
}
