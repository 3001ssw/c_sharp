using partial_class;

class Program
{
    static void Main(string[] args)
    {
        CTestMath math = new CTestMath();
        int iSum = math.sumXnY(5, 4);

        Console.WriteLine(math.LastCalcEn);
        Console.WriteLine(math.LastCalcKor);
    }
}