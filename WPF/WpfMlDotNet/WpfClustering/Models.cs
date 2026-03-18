using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClustering
{
    public class QuestionData
    {
        public string Text { get; set; }
    }

    // 2. 출력(예측) 결과 구조
    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint SelectedClusterId { get; set; } // 할당된 그룹 번호

        [ColumnName("Score")]
        public float[] Distances { get; set; } // 각 그룹 중심과의 거리
    }
}
