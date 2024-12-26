using partial_class;

class Program
{
    static void Main(string[] args)
    {
        CTestMath math = new CTestMath();
        int iSum = math.sumXnY(5, 4);

        Console.WriteLine(math.LastCalcEn); // 마지막 계산 결과 출력(영어)
        Console.WriteLine(math.LastCalcKor); // 마지막 계산 결과 출력(한국어)
    }
}