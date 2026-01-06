using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfDependencyProperty2
{
    [Serializable]
    public class FileInfo : BindableBase
    {
        #region fields, properties
        private string fileName = "";
        public string FileName { get => fileName; set => SetProperty(ref fileName, value); }

        private string filePath = "";
        public string FilePath { get => filePath; set => SetProperty(ref filePath, value); }
        #endregion


    }
}
