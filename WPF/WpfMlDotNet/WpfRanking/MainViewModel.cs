using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfRanking
{
    public class MainViewModel : BindableBase
    {
        private MLContext _mlContext;
        private PredictionEngine<SearchData, SearchPrediction> _predictionEngine;

        // 1. UI 바인딩용 속성 (검색어)
        private string _searchQuery = "";
        public string SearchQuery { get => _searchQuery; set => SetProperty(ref _searchQuery, value); }

        // 2. UI 바인딩용 속성 (검색 결과 리스트)
        // ObservableCollection은 항목이 추가/삭제될 때 UI를 자동으로 갱신합니다.
        private ObservableCollection<RankedResult> _searchResults = new ObservableCollection<RankedResult>();
        public ObservableCollection<RankedResult> SearchResults { get => _searchResults; set => SetProperty(ref _searchResults, value); }

        // 3. UI 바인딩용 커맨드 (검색 버튼 클릭 동작)
        public ICommand SearchCommand { get; }

        public MainViewModel()
        {
            SearchCommand = new DelegateCommand(ExecuteSearch);
            SearchResults = new ObservableCollection<RankedResult>();
            InitializeMLModel();
        }

        private void InitializeMLModel()
        {
            _mlContext = new MLContext(seed: 0);

            // 1. 파일 대신 메모리에서 즉시 사용할 가상의 학습 데이터 생성
            // (이 데이터는 모델이 "어떤 특성이 순위에 유리한지" 학습하는 데 사용됩니다)
            var trainDataList = new List<SearchData>{
        // 검색 세션 1 (GroupId = 1)
        new SearchData { Relevance = 2, GroupId = 1, KeywordMatchCount = 10, DocumentRecency = 0.9f },
        new SearchData { Relevance = 1, GroupId = 1, KeywordMatchCount = 3,  DocumentRecency = 0.5f },
        new SearchData { Relevance = 0, GroupId = 1, KeywordMatchCount = 0,  DocumentRecency = 0.1f },

        // 검색 세션 2 (GroupId = 2) - 추가됨
        new SearchData { Relevance = 2, GroupId = 2, KeywordMatchCount = 8,  DocumentRecency = 0.8f },
        new SearchData { Relevance = 0, GroupId = 2, KeywordMatchCount = 1,  DocumentRecency = 0.2f },

        // 검색 세션 3 (GroupId = 3) - 추가됨
        new SearchData { Relevance = 1, GroupId = 3, KeywordMatchCount = 5,  DocumentRecency = 0.6f },
        new SearchData { Relevance = 0, GroupId = 3, KeywordMatchCount = 0,  DocumentRecency = 0.1f }
    };
            var trainingDataView = _mlContext.Data.LoadFromEnumerable(trainDataList);

            // 2. 학습 파이프라인 구성
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("GroupId")
                .Append(_mlContext.Transforms.Concatenate("Features", "KeywordMatchCount", "DocumentRecency"))
                .Append(_mlContext.Ranking.Trainers.FastTree(
                    labelColumnName: "Relevance",
                    rowGroupColumnName: "GroupId",
                    featureColumnName: "Features"));

            // 3. 모델 학습 진행 (.zip 파일 로드 대신 즉시 학습)
            ITransformer trainedModel = pipeline.Fit(trainingDataView);

            // 4. 예측 엔진 생성
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SearchData, SearchPrediction>(trainedModel);
        }

        // 검색 커맨드 실행 로직
        private void ExecuteSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;
            SearchResults.Clear();
            uint currentSessionGroupId = (uint)DateTime.Now.Ticks;
            List<Document> candidateDocs = GetCandidateDocumentsFromDB(SearchQuery);

            var searchDataList = new List<SearchData>();
            foreach (var doc in candidateDocs)
            {
                searchDataList.Add(new SearchData
                {
                    GroupId = currentSessionGroupId,
                    DocumentId = doc.Id,
                    DocumentTitle = doc.Title,
                    KeywordMatchCount = doc.Title.Split(new[] { SearchQuery }, StringSplitOptions.None).Length - 1,
                    DocumentRecency = CalculateRecencyScore(doc.CreatedAt)
                });
            }

            var resultsList = new List<RankedResult>();
            foreach (var item in searchDataList)
            {
                // 실제 모델 적용 시 아래 주석 해제
                // var prediction = _predictionEngine.Predict(item);
                // float score = prediction.Score;

                float score = 0; // 임시 점수

                resultsList.Add(new RankedResult
                {
                    Title = item.DocumentTitle,
                    RankScore = score
                });
            }

            // 점수 기준 내림차순 정렬 후 ObservableCollection에 할당하여 UI 갱신
            var sortedResults = resultsList.OrderByDescending(r => r.RankScore).ToList();
            SearchResults = new ObservableCollection<RankedResult>(sortedResults);
        }

        // 가상 데이터 제공 메서드
        private List<Document> GetCandidateDocumentsFromDB(string query)
        {
            var now = DateTime.Now;
            return new List<Document>
    {
        // --- WPF 관련 문서 (검색어 적중률 높음) ---
        new Document { Id = 1, Title = "WPF 데이터 바인딩 기초와 활용 가이드", CreatedAt = now.AddDays(-5) },
        new Document { Id = 2, Title = "WPF MVVM 패턴 완벽 이해하기: Binding부터 Command까지", CreatedAt = now.AddDays(-15) },
        new Document { Id = 3, Title = "[실무] WPF 복잡한 데이터 바인딩 최적화 팁", CreatedAt = now.AddDays(-2) },
        new Document { Id = 4, Title = "WPF 바인딩 에러 해결하는 10가지 방법", CreatedAt = now.AddDays(-45) },
        new Document { Id = 5, Title = "WpfBinding 입문자용 튜토리얼 (2025)", CreatedAt = now.AddDays(-1) },
        new Document { Id = 6, Title = "WPF에서 다중 바인딩(MultiBinding) 사용하는 법", CreatedAt = now.AddDays(-120) },
        new Document { Id = 7, Title = "WPF 스타일과 템플릿에서의 데이터 바인딩 전략", CreatedAt = now.AddDays(-30) },
        new Document { Id = 8, Title = "WPF .NET 8 업데이트와 바인딩 성능 향상점", CreatedAt = now.AddDays(-200) },
        new Document { Id = 9, Title = "WPF 3D 그래픽스 기초 (데이터 바인딩 포함)", CreatedAt = now.AddDays(-10) },
        new Document { Id = 10, Title = "초보자를 위한 WPF와 C# 바인딩 연결하기", CreatedAt = now.AddDays(-3) },

        // --- C# / .NET 관련 문서 (검색어 적중률 중간) ---
        new Document { Id = 11, Title = "C# 12 신기능 요약 및 WPF 적용 사례", CreatedAt = now.AddDays(-60) },
        new Document { Id = 12, Title = ".NET Core에서 WPF 앱 만들기 (바인딩 중심)", CreatedAt = now.AddDays(-90) },
        new Document { Id = 13, Title = "C# 비동기 프로그래밍과 WPF UI 바인딩 업데이트", CreatedAt = now.AddDays(-8) },
        new Document { Id = 14, Title = "Entity Framework Core와 WPF 데이터 바인딩 연동", CreatedAt = now.AddDays(-25) },
        new Document { Id = 15, Title = "C# 인터페이스 설계 원칙과 WPF 활용", CreatedAt = now.AddDays(-300) },
        new Document { Id = 16, Title = "Visual Studio 2022에서 WPF 바인딩 디버깅하기", CreatedAt = now.AddDays(-14) },
        new Document { Id = 17, Title = "C# 대규모 프로젝트 구조 설계 (WPF/MVVM)", CreatedAt = now.AddDays(-400) },
        new Document { Id = 18, Title = "LINQ를 활용한 WPF 리스트 바인딩 데이터 처리", CreatedAt = now.AddDays(-20) },
        new Document { Id = 19, Title = "WPF 성능 프로파일링: 바인딩 병목 지점 찾기", CreatedAt = now.AddDays(-55) },
        new Document { Id = 20, Title = "C# 델리게이트와 이벤트, 그리고 WPF 바인딩", CreatedAt = now.AddDays(-70) },

        // --- 기타 / 라이브러리 / 잡지 (검색어 적중률 낮음) ---
        new Document { Id = 21, Title = "Prism 라이브러리를 이용한 WPF 개발 가이드", CreatedAt = now.AddDays(-150) },
        new Document { Id = 22, Title = "DevExpress WPF 컨트롤 바인딩 심화 학습", CreatedAt = now.AddDays(-5) },
        new Document { Id = 23, Title = "Material Design In XAML 사용법 정리", CreatedAt = now.AddDays(-12) },
        new Document { Id = 24, Title = "오늘의 개발자 뉴스: WPF의 미래는?", CreatedAt = now.AddSeconds(-30) }, // 극최신
        new Document { Id = 25, Title = "Unity 2D 게임 개발 기초 (C# 활용)", CreatedAt = now.AddDays(-500) }, // 아주 오래됨
        new Document { Id = 26, Title = "Git 사용법 정리: 커밋부터 푸시까지", CreatedAt = now.AddDays(-100) },
        new Document { Id = 27, Title = "SQL 기초 쿼리문 연습하기", CreatedAt = now.AddDays(-80) },
        new Document { Id = 28, Title = "Python 머신러닝 입문: 데이터 분석 기초", CreatedAt = now.AddDays(-180) },
        new Document { Id = 29, Title = "Effective C# (도서 요약) - WPF 섹션 포함", CreatedAt = now.AddDays(-730) }, // 2년전
        new Document { Id = 30, Title = "공지사항: 이번 주 시스템 점검 안내", CreatedAt = now.AddHours(-1) }
    };
        }

        private float CalculateRecencyScore(DateTime createdAt)
        {
            // 1. 현재 시간과 문서 생성일의 차이(일수)를 구합니다.
            TimeSpan age = DateTime.Now - createdAt;
            double daysOld = age.TotalDays;

            // 2. 기준을 정합니다. (예: 365일이 넘으면 0점 처리)
            const double maxDays = 365.0;

            if (daysOld <= 0) return 1.0f; // 방금 쓴 글은 만점
            if (daysOld >= maxDays) return 0.0f; // 1년 넘은 글은 최저점

            // 3. 최신일수록 1.0에 가깝고, 오래될수록 0.0에 가깝게 선형 계산
            // 예: 100일 된 글 -> 1.0 - (100 / 365) = 약 0.72
            float score = (float)(1.0 - (daysOld / maxDays));

            return score;
        }
    }
}