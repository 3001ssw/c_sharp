using System;

namespace List
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<CPerson> listPerson = new List<CPerson>(); // List 생성

            // 추가
            listPerson.Add(new CPerson("김철수", 10));
            listPerson.Add(new CPerson("박민수", 15));
            listPerson.Add(new CPerson("이아영", 12));
            listPerson.Add(new CPerson("홍길동", 14));

            CPerson[] people = { new CPerson("최민수", 20), new CPerson("손흥민", 30), new CPerson("양은호", 10) };
            listPerson.AddRange(people);

            // 조회
            foreach (CPerson item in listPerson)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();

            // 총 개수
            int iCount = listPerson.Count();
            Console.WriteLine($"총 개수: {iCount}");
            Console.WriteLine();

            // 찾기
            int iIndex = listPerson.FindIndex(person => person.NAME == "이아영");
            Console.WriteLine($"Find Index: {iIndex}");
            Console.WriteLine();

            // 인덱스로 요소 가져오기
            try
            {
                CPerson cFind1 = listPerson[10]; // 인덱스 범위 밖이면 ArgumentOutOfRangeException 발생
                //CPerson cFind1 = listPerson[iIndex]; // 인덱스 범위 밖이면 ArgumentOutOfRangeException 발생
                Console.WriteLine($"Index: {iIndex}, Find: [{cFind1.ToString()}]");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();

            CPerson cFind2 = listPerson.ElementAt(iIndex);
            Console.WriteLine($"Index: {iIndex}, Find: [{cFind2.ToString()}]");
            Console.WriteLine();

            // 요소로 인덱스 가져오기
            int iIndexByElement = listPerson.IndexOf(cFind2);
            Console.WriteLine($"element: [{cFind2.ToString()}], index: {iIndexByElement}");
            Console.WriteLine();

            // 정렬
            Console.WriteLine("======= 이름으로 정렬 =======");
            listPerson.Sort((x, y) => x.NAME.CompareTo(y.NAME)); // 이름 기준
            foreach (CPerson item in listPerson)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("============================");
            Console.WriteLine();

            Console.WriteLine("======= 나이로 정렬 ========");
            listPerson.Sort((x, y) => x.AGE.CompareTo(y.AGE)); // 나이 기준
            foreach (CPerson item in listPerson)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("============================");

            // 삭제
            listPerson.Clear();
        }
    }

    class CPerson
    {
        string m_strName; // 이름
        public string NAME
        {
            get
            {
                return m_strName;
            }
        }

        int m_iAge; // 나이
        public int AGE
        {
            get
            {
                return m_iAge;
            }
        }

        public CPerson(string strName, int iAge)
        {
            m_strName = strName;
            m_iAge = iAge;
        }

        public override string ToString()
        {
            return $"name: {NAME}, age: {AGE}";
        }
    }
}
