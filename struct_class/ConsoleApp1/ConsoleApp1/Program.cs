public struct Point_struct
{
    public int X;
    public int Y;

    public Point_struct()
    {
        X = 0;
        Y = 0;
    }

    public Point_struct(int x, int y)
    {
        X = x;
        Y = y;
    }

    public string toString()
    {
        string str = $"({X}, {Y})";
        return str;
    }
}

public class Point_class
{
    public int X;
    public int Y;

    public Point_class()
    {
        X = 0;
        Y = 0;
    }

    public Point_class(int x, int y) 
    {
        X = x;
        Y = y;
    }

    public string toString()
    {
        string str = $"({X}, {Y})";
        return str;
    }
}

class Program
{
    static void Main(string[] args)
    {
        ////////////////////////////////////////// 선언
        // 구조체
        Point_struct st1;
        st1.X = 1;
        st1.Y = 2;
        Point_struct st2 = new Point_struct();
        Point_struct st3 = new Point_struct(3, 4);

        Console.WriteLine("st1 :" + st1.toString());
        Console.WriteLine("st2 :" + st2.toString());
        Console.WriteLine("st3 :" + st3.toString());
        Console.WriteLine("");

        // 클래스
        //Point_class cls1; // 사용 불가능
        Point_class cls1 = new Point_class();
        Point_class cls2 = new Point_class(3, 4);
        Point_class cls3 = new Point_class(5, 6);

        Console.WriteLine("cls1 :" + cls1.toString());
        Console.WriteLine("cls2 :" + cls2.toString());
        Console.WriteLine("cls3 :" + cls3.toString());
        Console.WriteLine("");
        //////////////////////////////////////////

        ////////////////////////////////////////// 복사
        // 구조체
        Point_struct st4 = st1; // 값 복사
        Console.WriteLine("변경 전");
        Console.WriteLine("st1 :" + st1.toString());
        Console.WriteLine("st4 :" + st4.toString());
        Console.WriteLine("");

        st4.X = 30; // st4의 X만 변경 됨
        Console.WriteLine("변경 후");
        Console.WriteLine("st1 :" + st1.toString());
        Console.WriteLine("st4 :" + st4.toString());
        Console.WriteLine("");

        // 클래스
        Point_class cls4 = cls1; // 참조 복사
        Console.WriteLine("변경 전");
        Console.WriteLine("cls1 :" + cls1.toString());
        Console.WriteLine("cls4 :" + cls4.toString());
        Console.WriteLine("");

        cls4.X = 30; // cls1의 X도 변경 됨
        Console.WriteLine("변경 후");
        Console.WriteLine("cls1 :" + cls1.toString());
        Console.WriteLine("cls4 :" + cls4.toString());
        //////////////////////////////////////////
    }
}