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

        public DelegateCommand InputTextCommand { get; private set; }
        private void OnInputText()
        {
            UpdatePredictions(); // 텍스트 입력 시 실시간 예측
        }

        private bool CanInputText()
        {
            if (string.IsNullOrEmpty(InputText))
                return false;

            return true;
        }

        // 4. 입력 텍스트 (Context)
        private string inputText = "";
        public string InputText
        {
            get => inputText;
            set
            {
                SetProperty(ref inputText, value);
                UpdatePredictions();
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
            InputTextCommand = new DelegateCommand(OnInputText, CanInputText);

            // [1. 사용자 및 계정 관련]
            Menus.Add("내 프로필 설정");      // 검색어 예: "이름", "사진", "내정보"
            Menus.Add("비밀번호 변경");      // 검색어 예: "암호", "보안", "비번"
            Menus.Add("로그아웃");          // 검색어 예: "종료", "나가기", "계정"
            Menus.Add("계정 권한 관리");     // 검색어 예: "등급", "권한", "관리자"

            // [2. 시스템 및 환경 설정]
            Menus.Add("일반 환경 설정");     // 검색어 예: "셋팅", "언어", "기본"
            Menus.Add("알림 및 푸시 설정");   // 검색어 예: "소리", "메시지", "팝업"
            Menus.Add("테마 및 디자인");     // 검색어 예: "다크모드", "UI", "색상"
            Menus.Add("네트워크/연결 설정");  // 검색어 예: "서버", "IP", "연결"

            // [3. 데이터 및 파일 관리]
            Menus.Add("데이터 백업/복구");    // 검색어 예: "저장", "복원", "백업"
            Menus.Add("파일 업로드");        // 검색어 예: "가져오기", "등록", "첨부"
            Menus.Add("엑셀 데이터 내보내기"); // 검색어 예: "출력", "저장", "다운로드"
            Menus.Add("히스토리 및 로그");    // 검색어 예: "기록", "로그", "활동"

            // [4. 조회 및 통계]
            Menus.Add("대시보드 보기");      // 검색어 예: "현황", "메인", "요약"
            Menus.Add("실시간 매출 통계");    // 검색어 예: "돈", "정산", "그래프"
            Menus.Add("사용자 활동 분석");    // 검색어 예: "트래픽", "방문자", "인기"
            Menus.Add("상세 보고서 생성");    // 검색어 예: "리포트", "PDF", "결과"

            // [5. 고객 지원 및 매뉴얼]
            Menus.Add("사용자 설명서(F1)");   // 검색어 예: "도움말", "매뉴얼", "방법"
            Menus.Add("공지사항 게시판");     // 검색어 예: "소식", "알림", "뉴스"
            Menus.Add("1:1 고객 문의");      // 검색어 예: "상담", "질문", "센터"
            Menus.Add("프로그램 정보/업데이트"); // 검색어 예: "버전", "패치", "최신"

            // 모델 초기화를 위한 더미 데이터
            TrainingHistories.Add(new MenuData { Context = "비밀번호", Prediction = "비밀번호 변경", Label = true });

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
                PredictionEngine<MenuData, MenuPrediction> predEngine = _mlContext.Model.CreatePredictionEngine<MenuData, MenuPrediction>(_model);

                // 현재 Menus 리스트에 있는 모든 항목에 대해 점수 계산
                var top3 = Menus.Select(m => new {
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
