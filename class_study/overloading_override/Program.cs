class ClassMath
{
    // 매개 변수 개수 2개
    public virtual void Sum(int x, int y)
    {
        Console.WriteLine($"{x} + {y} = {x+y}");
    }

    // 매개 변수 개수 3개
    public void Sum(int x, int y, int z)
    {
        Console.WriteLine($"{x} + {y} + {z} = {x + y + z}");
    }

    // 매개 변수 타입 double
    public void Sum(double x, double y)
    {
        Console.WriteLine($"{x} + {y} = {x + y}");
    }
}
class ClassMathKorean : ClassMath
{
    // 함수 오버라이딩
    public override void Sum(int x, int y)
    {
        Console.WriteLine($"{x} 더하기 {y} 는 {x+y} 입니다.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ClassMath math = new ClassMath();
        math.Sum(1, 2);
        math.Sum(1, 2, 3);
        math.Sum(1.1, 2.2);

        ClassMathKorean math_kor = new ClassMathKorean();
        math_kor.Sum(1, 2);
    }
}