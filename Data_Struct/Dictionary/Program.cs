namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Dictionary 생성
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // 데이터 추가
            dict.Add("사과", "Apple");
            dict.Add("바나나", "Banana");
            dict.Add("체리", "Cherry");
            dict.Add("포도", "Grape");
            dict.Add("오렌지", "Orange");
            dict.Add("복숭아", "Peach");
            dict.Add("자두", "Plum");
            dict.Add("딸기", "Strawberry");
            dict.Add("레몬", "Lemon");
            dict.Add("멜론", "Melon");

            // 모든 데이터 조회
            foreach (KeyValuePair<string, string> pair in dict)
            {
                Console.WriteLine($"key: {pair.Key}, value: {pair.Value}");
            }

            // Count 속성 사용
            Console.WriteLine("==================================");
            Console.WriteLine($"Dictionary에 저장된 항목 개수: {dict.Count}");

            // 데이터 조회 (인덱서 사용)
            Console.WriteLine("==================================");
            Console.WriteLine($"딸기는 영어로? : {dict["딸기"]}");
            //Console.WriteLine($"수박은 영어로? : {dict["수박"]}"); // 수박은 KeyNotFoundException 발생

            // ContainsKey 확인
            Console.WriteLine("==================================");
            if (dict.ContainsKey("수박"))
                Console.WriteLine($"수박은 영어로? : {dict["수박"]}");
            else
                Console.WriteLine($"수박은 사전에 없는 단어입니다.");

            // TryGetValue 사용
            Console.WriteLine("==================================");
            if (dict.TryGetValue("수박", out string? value))
                Console.WriteLine($"수박은 영어로? : {value}");
            else
                Console.WriteLine($"수박은 사전에 없는 단어입니다.");

            // ContainsValue 확인
            Console.WriteLine("==================================");
            if (dict.ContainsValue("Apple"))
                Console.WriteLine($"Apple 사전에 있는 단어입니다.");
            else
                Console.WriteLine($"Apple 사전에 없는 단어입니다.");

            // 모든 키 출력
            Console.WriteLine("============== Keys ==============");
            foreach (string key in dict.Keys)
            {
                Console.WriteLine(key);
            }

            // 모든 값 출력
            Console.WriteLine("============== Values ==============");
            foreach (string val in dict.Values)
            {
                Console.WriteLine(val);
            }

            // 데이터 삭제
            dict.Remove("딸기");
            Console.WriteLine("==================================");
            Console.WriteLine($"딸기 삭제한 후 Count: {dict.Count}");

            // Clear로 모든 데이터 제거
            dict.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine($"Clear 이후 Count: {dict.Count}");
        }
    }
}
