using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSrCnn.Models;

namespace WpfSrCnn.Services
{
    public class AnomalyDetectionService
    {
        private readonly MLContext _mlContext = new(seed: 42);

        public List<CpuRecord> Detect(List<float> cpuUsages)
        {
            // 1. 데이터 준비
            var dataPoints = cpuUsages
                .Select(u => new CpuDataPoint { CpuUsage = u })
                .ToList();

            var dataView = _mlContext.Data.LoadFromEnumerable(dataPoints);

            // 2. SrCnn 파이프라인 구성
            //var pipeline = _mlContext.Transforms.DetectAnomalyBySrCnn(
            //    outputColumnName: nameof(CpuPrediction.Prediction),
            //    inputColumnName: nameof(CpuDataPoint.CpuUsage),
            //    windowSize: 24,          // 분석 윈도우 크기
            //    backAddCount: 5,         // 후방 추가 포인트
            //    lookaheadAddCount: 5,    // 전방 추가 포인트
            //    averagingWindowSize: 3,  // 평균 윈도우
            //    judgementWindowSize: 21, // 판단 윈도우
            //    threshold: 0.3           // 이상 판단 임계값 (낮을수록 민감)
            //);
            var pipeline = _mlContext.Transforms.DetectAnomalyBySrCnn(
                outputColumnName: nameof(CpuPrediction.Prediction),
                inputColumnName: nameof(CpuDataPoint.CpuUsage),
                windowSize: 24,
                threshold: 0.3
            );

            // 3. 학습 & 변환
            var transformedData = pipeline.Fit(dataView).Transform(dataView);

            // 4. 결과 추출
            var predictions = _mlContext.Data
                .CreateEnumerable<CpuPrediction>(transformedData, reuseRowObject: false)
                .ToList();

            // 5. 결과 매핑
            var results = new List<CpuRecord>();
            for (int i = 0; i < cpuUsages.Count; i++)
            {
                var pred = predictions[i].Prediction;
                results.Add(new CpuRecord
                {
                    Index = i + 1,
                    CpuUsage = cpuUsages[i],
                    IsAnomaly = pred[0] == 1,
                    AnomalyScore = Math.Round(pred[1], 4),
                    PValue = Math.Round(pred[2], 4)
                });
            }

            return results;
        }

        // 테스트용 샘플 데이터 생성 (정상 구간 + 스파이크 구간 혼합)
        public static List<float> GenerateSampleData()
        {
            var random = new Random(42);
            var data = new List<float>();

            for (int i = 0; i < 100; i++)
            {
                float value = i switch
                {
                    // 정상 구간: 20~40% 사용량
                    < 30 => 20f + (float)random.NextDouble() * 20f,
                    // 스파이크 이상 구간
                    30 or 31 or 32 => 85f + (float)random.NextDouble() * 10f,
                    // 정상 복귀
                    < 60 => 25f + (float)random.NextDouble() * 15f,
                    // 점진적 증가 이상
                    < 70 => 50f + (i - 60) * 3f + (float)random.NextDouble() * 5f,
                    // 정상 복귀
                    < 85 => 22f + (float)random.NextDouble() * 18f,
                    // 또 다른 스파이크
                    85 or 86 => 92f + (float)random.NextDouble() * 5f,
                    // 정상
                    _ => 20f + (float)random.NextDouble() * 20f
                };

                data.Add(Math.Clamp(value, 0f, 100f));
            }

            return data;
        }
    }
}
