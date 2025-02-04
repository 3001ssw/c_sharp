using System.Collections.Concurrent;
using System.Xml.Linq;

namespace Queue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<CPrintDocument> queuePrint = new Queue<CPrintDocument>(); // Queue 생성
            //ConcurrentQueue<CPerson> queue = new ConcurrentQueue<CPerson>(); // Queue 생성

            Console.WriteLine("프린터 시작");
            Console.WriteLine();

            // 요소 추가
            Console.WriteLine("프린터 대기열에 문서 입력 중");
            queuePrint.Enqueue(new CPrintDocument("보고서.pdf"));
            queuePrint.Enqueue(new CPrintDocument("사진.jpg"));
            queuePrint.Enqueue(new CPrintDocument("계약서.docx"));
            queuePrint.Enqueue(new CPrintDocument("정리.xlsx"));
            Console.WriteLine("대기열에 입력이 완료되었습니다.");
            Console.WriteLine();

            // 조회
            Console.WriteLine("프린터 전체 문서 조회");
            foreach (CPrintDocument person in queuePrint)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine("문서 조회가 완료되었습니다.");
            Console.WriteLine();

            // 제거 없이 요소 반환
            Console.WriteLine($"첫 번째 문서 확인: {queuePrint.Peek()}");
            Console.WriteLine();

            // 요소 제거 및 반환
            Console.WriteLine($"첫번째 문서 출력 중 ...: {queuePrint.Dequeue()}");
            Console.WriteLine("출력이 완료 되었습니다.");
            Console.WriteLine($"두번째 문서 출력 중 ...: {queuePrint.Dequeue()}");
            Console.WriteLine("출력이 완료 되었습니다.");
            Console.WriteLine();

            // 현재 큐의 요소 개수 확인
            Console.WriteLine($"남은 문서 수: {queuePrint.Count}");
            Console.WriteLine();

            // 요소 존재 유무 확인
            CPrintDocument clsContain = queuePrint.Peek();
            bool bContain = queuePrint.Contains(clsContain);
            Console.WriteLine($"{clsContain} 문서 존재 유무 : {bContain}");
            Console.WriteLine();

            // 큐의 요소를 새 배열에 복사
            CPrintDocument[] arr = queuePrint.ToArray();
            foreach (CPrintDocument person in arr)
            {
                Console.WriteLine(person.ToString());
            }
            Console.WriteLine();

            // 모든 요소 삭제
            queuePrint.Clear();
            Console.WriteLine("모든 프린트 작업 문서 삭제" + Environment.NewLine);
        }
    }

    class CPrintDocument
    {
        private string m_strDocument;
        public string DOCUMENT
        {
            get
            {
                return m_strDocument;
            }
        }

        public CPrintDocument(string strName)
        {
            m_strDocument = strName;
        }

        public override string ToString()
        {
            return $"document name[{DOCUMENT}]";
        }
    }
}