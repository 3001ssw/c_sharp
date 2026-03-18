using System;
using Microsoft.ML.Data;

namespace WpfRanking
{
    // ML.NET 학습 및 예측용 데이터
    public class SearchData
    {
        [LoadColumn(0)] public float Relevance;         // 정답 (0: 무관, 1: 보통, 2: 매우 관련됨)
        [LoadColumn(1)] public uint GroupId;            // 검색어 ID (같은 검색어는 같은 ID)
        [LoadColumn(2)] public float KeywordMatchCount; // 특성 : 키워드 일치 횟수
        [LoadColumn(3)] public float DocumentRecency;   // 특성 : 문서 최신성
        [LoadColumn(4)] public float Price;   // 특성 : 가격
        public int DocumentId;
        public string DocumentTitle;// 결과 확인용 문서 이름 (학습에는 미사용)
    }

    public class SearchPrediction
    {
        public float Score;
    }

    // 데이터베이스 원본 가상 구조
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // UI 리스트 바인딩용 결과 구조
    public class RankedResult
    {
        public string Title { get; set; }
        public float RankScore { get; set; }
    }
}