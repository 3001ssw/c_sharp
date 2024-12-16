using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace animals
{
    public class CAnimal
    {
        public CAnimal()
        {
            Console.WriteLine("생성");
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

        public virtual string Info()
        {
            return "동물";
        }
    }
    public class CPerson : CAnimal
    {

        public CPerson()
        {
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

        public override string Info()
        {
            return "사람";
        }
    }
    public class CDog : CAnimal
    {

        public CDog()
        {
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

        public override string Info()
        {
            return "개";
        }
    }
    public class CBird : CAnimal
    {

        public CBird()
        {
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

        public override string Info()
        {
            return "새";
        }
    }
}
