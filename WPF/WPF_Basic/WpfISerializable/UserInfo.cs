using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WpfISerializable
{
    [Serializable]
    public class UserInfo : BindableBase
    {
        #region fields, properties
        private string name = "";
        public string Name { get => name; set => SetProperty(ref name, value); }

        private int age = 0;
        [XmlIgnore]
        public int Age { get => age; set => SetProperty(ref age, value); }

        private string gender = "남";
        [JsonIgnore]
        public string Gender { get => gender; set => SetProperty(ref gender, value); }

        #endregion

        public UserInfo()
        {
            
        }
    }
}
