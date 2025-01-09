using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class ClassSubscribe
    {
        private StringBuilder strBulder = new StringBuilder();
        public string LastContext
        {
            get { return strBulder.ToString(); }
        }

        public ClassSubscribe()
        {
        }

        public void GetPublish(string strName, string strContext)
        {
            string strGet = string.Format($"[{strName}] 에서 [{strContext}]를 받았습니다.");
            strBulder.AppendLine(strGet);
        }
    }
}
