using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class ClassPublish : object
    {
        public delegate void DelegatePublish(string strName, string strContext);
        public event DelegatePublish? eventPublish = null;

        private string _name;
        public string Name
        {
            get { return this._name; }
        }

        private string _lastPublish;
        public string LastPublish
        {
            get { return this._lastPublish; }
        }

        public ClassPublish(string name)
        {
            _name = name;
            _lastPublish = "";
        }

        public void Publish()
        {
            DateTime now = DateTime.Now;
            string strNow = now.ToString("[yyyy-MM-dd HH:mm:ss]에 발행.");
            eventPublish?.Invoke(Name, strNow);

            _lastPublish = strNow;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
