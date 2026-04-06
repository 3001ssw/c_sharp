using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfContextualBandits
{
    public class MenuData
    {
        [LoadColumn(0)] public string Context { get; set; }   // 현재 입력 중인 텍스트
        [LoadColumn(1)] public string Prediction { get; set; } // AI가 제안하는 후보
        [LoadColumn(2)] public bool Label { get; set; }        // 보상 여부 (맞으면 true)
    }

    public class MenuPrediction
    {
        [ColumnName("Score")] public float Score { get; set; } // 추천 강도
    }
}
