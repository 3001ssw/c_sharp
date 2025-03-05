using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridView_List
{
    class CPerson
    {
        private string m_strName;
        private int m_iAge;

        public CPerson(string strName, int iAge = 0)
        {
            m_strName = strName;
            m_iAge = iAge;
        }

        public string NAME { set => m_strName = value; get => m_strName; }
        public int AGE { set => m_iAge = value; get => m_iAge; }
    }
}
