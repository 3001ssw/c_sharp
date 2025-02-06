namespace Stack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CWebBrowser webBrowser = new CWebBrowser();
            webBrowser.DoRun();
        }
    }

    public class CWebBrowser
    {
        private Stack<CWebAddress> _stackCurrent = new Stack<CWebAddress>(); // 현재 보고있는 페이지 관리 스택
        private Stack<CWebAddress> _stackNext = new Stack<CWebAddress>(); // 다음 페이지 관리 스택

        public CWebBrowser()
        {
        }

        public void DoRun()
        {
            _stackCurrent.Clear();
            _stackNext.Clear();
            _stackCurrent.Push(new CWebAddress("https://3001ssw.com")); // 기본 페이지 입력

            Console.WriteLine("브라우저를 시작합니다.");

            while (true)
            {
                Console.WriteLine("==============================================================");
                Console.WriteLine($"현재 페이지: {_stackCurrent.Peek()}");
                Console.WriteLine("(새 주소 입력 - 문자열, 이전페이지 - p, 다음페이지 - n, 종료 - X)");
                string? strRead = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(strRead) == false)
                {
                    strRead = strRead.ToUpper();

                    // 이전 페이지 이동
                    if (strRead.Equals("P"))
                    {
                        if (_stackCurrent.Count <= 1)
                            Console.WriteLine("이전 페이지가 없습니다.");
                        else
                            _stackNext.Push(_stackCurrent.Pop()); // 현재페이지 스택에서 Pop, 다음페이지 스택에 Push
                    }
                    // 다음 페이지
                    else if (strRead.Equals("N"))
                    {
                        if (_stackCurrent.Count == 0 || _stackNext.Count == 0)
                            Console.WriteLine("다음 페이지가 없습니다.");
                        else
                            _stackCurrent.Push(_stackNext.Pop()); // 다음페이지 스택에서 Pop, 현재페이지 스택에 Push
                    }
                    // 종료
                    else if (strRead.Equals("X"))
                        break;
                    // 주소 입력
                    else
                    {
                        strRead = strRead.ToLower();

                        _stackCurrent.Push(new CWebAddress(strRead)); // 주소 입력
                        _stackNext.Clear();
                    }
                }
            }
            _stackCurrent.Clear();
            _stackNext.Clear();
            Console.WriteLine("브라우저를 종료합니다.");
        }
    }

    public class CWebAddress
    {
        private string _address;

        public CWebAddress(string address)
        {
            _address = address;
        }

        public override string ToString()
        {
            return $"현재 주소: {_address}";
        }
    }
}
