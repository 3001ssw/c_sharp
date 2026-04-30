using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSrCnn.Models
{
    // ML.NET 입력 데이터
    public class CpuDataPoint
    {
        [LoadColumn(0)]
        public float CpuUsage { get; set; }
    }

    // ML.NET 출력 데이터
    public class CpuPrediction
    {
        // [0] = 이상 여부(0/1), [1] = 이상 점수, [2] = p-value
        [VectorType(3)]
        public double[] Prediction { get; set; } = [];
    }

    // UI 표시용 모델
    public class CpuRecord
    {
        public int Index { get; set; }
        public float CpuUsage { get; set; }
        public bool IsAnomaly { get; set; }
        public double AnomalyScore { get; set; }
        public double PValue { get; set; }

        // DataGrid 배경색 바인딩용
        public string RowColor => IsAnomaly ? "#FFCCCC" : "White";
        public string StatusText => IsAnomaly ? "⚠️ 이상" : "정상";
    }
}
