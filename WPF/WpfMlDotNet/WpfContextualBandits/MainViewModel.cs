using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfContextualBandits
{
    public class MainViewModel : BindableBase
    {
        private MLContext _mlContext = new MLContext();
        private ITransformer _model = null;

        // 1. 왼쪽 DataGrid: 사용 가능한 전체 메뉴 리스트
        public ObservableCollection<string> Menus { get; set; } = new ObservableCollection<string>();

        // 2. 중앙 DataGrid: 학습 이력 (로그)
        public ObservableCollection<MenuData> TrainingHistories { get; set; } = new ObservableCollection<MenuData>();

        // 3. 우측 하단 DataGrid: AI가 추천한 Top 3 메뉴
        public ObservableCollection<string> PredictMenus { get; set; } = new ObservableCollection<string>();

        // 4. 입력 텍스트 (Context)
        private string _inputText = "";
        public string InputText
        {
            get => _inputText;
            set
            {
                SetProperty(ref _inputText, value);
                UpdatePredictions(); // 텍스트 입력 시 실시간 예측
            }
        }

        // 5. 예측 메뉴 선택 시 학습 발생 (Reward)
        private string _selectedPredictMenu;
        public string SelectedPredictMenu
        {
            get => _selectedPredictMenu;
            set
            {
                if (value != null)
                {
                    OnMenuSelected(value); // 선택하는 순간 보상 시스템 작동
                }
                _selectedPredictMenu = null; // 선택 후 해제 (다시 클릭 가능하게)
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            // --- 10개의 고정 메뉴 설정 ---
            Menus.Add("환경설정");
            Menus.Add("로그아웃");
            Menus.Add("장바구니");
            Menus.Add("공지사항");
            Menus.Add("고객센터");
            Menus.Add("내 정보 관리");
            Menus.Add("결제내역");
            Menus.Add("비밀번호 변경");
            Menus.Add("이벤트/쿠폰");
            Menus.Add("자주 묻는 질문");

            // 모델 초기화를 위한 더미 데이터
            TrainingHistories.Add(new MenuData { Context = "초기화", Prediction = "환경설정", Label = true });

            UpdateModel();
        }

        // --- 핵심 로직 1: 실시간 예측 ---
        private void UpdatePredictions()
        {
            if (string.IsNullOrWhiteSpace(InputText) || _model == null || Menus.Count == 0)
            {
                PredictMenus.Clear();
                return;
            }

            try
            {
                var predEngine = _mlContext.Model.CreatePredictionEngine<MenuData, MenuPrediction>(_model);

                // 현재 Menus 리스트에 있는 모든 항목에 대해 점수 계산
                var top3 = Menus
                    .Select(m => new
                    {
                        Name = m,
                        Score = predEngine.Predict(new MenuData { Context = InputText, Prediction = m }).Score
                    })
                    .OrderByDescending(x => x.Score)
                    .ToList();

                PredictMenus.Clear();
                foreach (var item in top3) PredictMenus.Add(item.Name);
            }
            catch { /* 모델 업데이트 중 예측 엔진 충돌 방지 */ }
        }

        // --- 핵심 로직 2: 보상 시스템 (선택 시 학습) ---
        private void OnMenuSelected(string selected)
        {
            var currentBatch = PredictMenus.ToList();
            if (!currentBatch.Contains(selected)) return;

            // 1. 선택된 메뉴는 + 보상(True), 나머지 추천되었던 메뉴는 - 보상(False)
            foreach (var menu in currentBatch)
            {
                var newData = new MenuData
                {
                    Context = InputText,
                    Prediction = menu,
                    Label = (menu == selected)
                };
                TrainingHistories.Add(newData);
            }

            // 2. 모델 재학습
            UpdateModel();

            // 3. 학습 후 예측 결과 갱신
            UpdatePredictions();
        }

        // --- 핵심 로직 3: ML.NET 파이프라인 ---
        private void UpdateModel()
        {
            if (TrainingHistories.Count == 0) return;

            var dataView = _mlContext.Data.LoadFromEnumerable(TrainingHistories);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Context")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Prediction"))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(new[] {
                    new InputOutputColumnPair("ContextVec", "Context"),
                    new InputOutputColumnPair("PredictVec", "Prediction")
                }))
                .Append(_mlContext.Transforms.Concatenate("Features", "ContextVec", "PredictVec"))
                .Append(_mlContext.BinaryClassification.Trainers.FieldAwareFactorizationMachine(new[] { "Features" }));

            _model = pipeline.Fit(dataView);
        }
    }
}
