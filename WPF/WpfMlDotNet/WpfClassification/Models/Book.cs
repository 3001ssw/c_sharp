using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfClassification.Models
{
    public class Book
    {
        public string Synopsis { get; set; } = "";
        public string Genre { get; set; } = "";
    }

    public class PredictBook
    {
        [ColumnName("PredictedGenre")]
        public string PredictedGenre { get; set; } = "";
    }
}
