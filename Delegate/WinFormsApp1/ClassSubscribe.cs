using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class ClassSubscribe : object
    {
        private string _name;
        public string Name { get => _name; }


        private StringBuilder strBulder = new StringBuilder();
        public string AllContext
        {
            get { return strBulder.ToString(); }
        }

        public ClassSubscribe(string name)
        {
            _name = name;
        }

        public void GetPublish(string strName, string strContext)
        {
            string strGet = string.Format($"[{strName}] 에서 [{strContext}]를 수신.");
            strBulder.AppendLine(strGet);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
