using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace animals
{
    public class CAnimal
    {
        protected string strName;

        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        public CAnimal()
        {
            strName = "동물";
        }

        public virtual string Sound()
        {
            return "소리";
        }

        public virtual string Move()
        {
            return "이동";
        }

        public virtual string Eat()
        {
            return "먹기";
        }

        public string Info()
        {
            return strName;
        }
    }
    public class CPerson : CAnimal
    {

        public CPerson()
        {
            strName = "사람";
        }

        public override string Sound()
        {
            return "안녕하세요";
        }

        public override string Move()
        {
            return "두발로 이동";
        }

        public override string Eat()
        {
            return "고기를 구워서 먹기";
        }
    }
    public class CDog : CAnimal
    {

        public CDog()
        {
            strName = "강아지";
        }

        public override string Sound()
        {
            return "멍멍";
        }

        public override string Move()
        {
            return "네발로 이동";
        }

        public override string Eat()
        {
            return "사료 먹기";
        }
    }
    public class CBird : CAnimal
    {

        public CBird()
        {
            strName = "새";
        }

        public override string Sound()
        {
            return "짹짹";
        }

        public override string Move()
        {
            return "날개로 이동";
        }

        public override string Eat()
        {
            return "벌레 먹기";
        }
    }
}
