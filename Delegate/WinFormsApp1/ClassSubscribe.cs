using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class ClassSubscribe : object
    {
        private string _name; // 구독자 이름
        public string Name { get => _name; }


        private StringBuilder strBulder = new StringBuilder(); // 발행 받은 전체 내용
        public string AllContext
        {
            get { return strBulder.ToString(); }
        }

        public ClassSubscribe(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 이벤트 수신 함수
        /// </summary>
        /// <param name="strName">발행자 이름</param>
        /// <param name="strContext">발행 내용</param>
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
