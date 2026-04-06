using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfContextualBandits
{
    public class TextData
    {
        [LoadColumn(0)] public string Context { get; set; }   // 현재 입력 중인 텍스트
        [LoadColumn(1)] public string Prediction { get; set; } // AI가 제안하는 후보
        [LoadColumn(2)] public bool Label { get; set; }        // 보상 여부 (맞으면 true)
    }

    public class TextPrediction
    {
        [ColumnName("Score")] public float Score { get; set; } // 추천 강도
    }

    public class MainViewModel : BindableBase
    {
        private MLContext _mlContext = new MLContext();
        private ITransformer _model = null;

        // AI가 추천할 후보군 리스트 (사용자가 한 번이라도 입력한 단어들이 여기 추가됨)
        private HashSet<string> _actionPool = new HashSet<string> { "안녕하세요", "반갑습니다", "감사합니다" };
        private List<TextData> _trainingHistory = new List<TextData>();

        private string inputText = "";
        public string InputText
        {
            get => inputText;
            set
            {
                SetProperty(ref inputText, value);
                PredictNextText();
            }
        }

        private string predictText = "";
        public string PredictText { get => predictText; set => SetProperty(ref predictText, value); }

        public DelegateCommand InputTextCommand { get; private set; }

        public MainViewModel()
        {
            InputTextCommand = new DelegateCommand(OnInputText, CanInputText);

            _trainingHistory.Add(new TextData { Context = "안녕", Prediction = "안녕하세요", Label = true });
            UpdateModel();
        }

        private void OnInputText()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(PredictText))
                return;

            // 1. 새로운 단어라면 후보군에 추가
            _actionPool.Add(InputText);

            // 2. 보상 데이터 생성 (현재 입력값과 추천값이 같으면 긍정 보상)
            bool isCorrect = (InputText == PredictText);

            _trainingHistory.Add(new TextData
            {
                Context = InputText,
                Prediction = PredictText,
                Label = isCorrect // 같으면 true(1.0점), 다르면 false(0.0점)
            });

            // 3. 재학습 (AI의 뇌 업데이트)
            UpdateModel();

            System.Diagnostics.Debug.WriteLine($"학습 완료: {InputText} == {PredictText} ({isCorrect})");

            InputText = "";
            //PredictText = "";
        }

        private bool CanInputText()
        {
            return true;
        }

        private void PredictNextText()
        {
            if (string.IsNullOrEmpty(InputText) || _model == null) return;

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<TextData, TextPrediction>(_model);

            // 후보군(_actionPool) 중에서 현재 InputText와 결합했을 때 점수가 가장 높은 놈을 찾음
            var bestMatch = _actionPool
                .Select(candidate => new
                {
                    Text = candidate,
                    Score = predictionEngine.Predict(new TextData { Context = InputText, Prediction = candidate }).Score
                })
                .OrderByDescending(x => x.Score)
                .FirstOrDefault();

            if (bestMatch != null && bestMatch.Score > 0)
                PredictText = bestMatch.Text;
            else
                PredictText = "추천 없음";
        }

        private void UpdateModel()
        {
            var dataView = _mlContext.Data.LoadFromEnumerable(_trainingHistory);

            // 파이프라인 수정
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Context")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Prediction"))
                // 1. Key 데이터를 원-핫 인코딩(벡터)으로 변환합니다. (이 과정이 추가되어야 함!)
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(
                    new[] {
                new InputOutputColumnPair("ContextVector", "Context"),
                new InputOutputColumnPair("PredictionVector", "Prediction")
                    }))
                // 2. 변환된 벡터 데이터들을 합칩니다.
                .Append(_mlContext.Transforms.Concatenate("Features", "ContextVector", "PredictionVector"))
                // 3. 마지막으로 알고리즘 적용
                .Append(_mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new[] { "Features" }));

            _model = pipeline.Fit(dataView);
        }
    }
}
