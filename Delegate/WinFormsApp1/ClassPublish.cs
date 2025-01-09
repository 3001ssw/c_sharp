using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class ClassPublish
    {
        public delegate void DelegatePublish(string strName, string strContext);
        public event DelegatePublish? eventPublish = null;

        private string _name;
        public string Name
        {
            get { return this._name; }
        }

        public ClassPublish(string name)
        {
            _name = name;
        }

        public void Publish()
        {
            DateTime now = DateTime.Now;
            string strNow = now.ToString("[yyyy-MM-dd HH:mm:ss:fff]에 발행하였습니다.");
            if (eventPublish != null)
                eventPublish(Name, strNow);
        }
    }
}
