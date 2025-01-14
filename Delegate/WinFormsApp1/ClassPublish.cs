using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class ClassPublish : object
    {
        public delegate void DelegatePublish(string strName, string strContext); // 발행 델리게이트
        public event DelegatePublish? eventPublish = null; // 발행 이벤트

        private string _name; // 발행자 이름
        public string Name
        {
            get { return this._name; }
        }

        private string _lastPublish; // 마지막 발행 내용
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
            eventPublish?.Invoke(Name, strNow); // 발행 이벤트 호출

            _lastPublish = strNow;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
