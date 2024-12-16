public class CStudent
{
    // 필드
    private string strName;
    private int iAge;

    // 프로퍼티
    public string Name
    {
        get { return this.strName; }
        set { this.strName = value; }
    }
    public int Age
    {
        get { return this.iAge; }
        set { this.iAge = value; }
    }

    // 생성자
    public CStudent(string strName, int iAge)
    {
        this.strName = strName;
        this.iAge = iAge;
    }

    // 메소드
    public void WriteStudentInfo()
    {
        string str = string.Format($"이름: {Name}, 나이: {Age}");
        Console.WriteLine(str);
    }
}

class Program
{
    static void Main(string[] args)
    {
        CStudent student1 = new CStudent("홍길동", 10);
        student1.WriteStudentInfo(); // 메소드 호출

        CStudent student2 = new CStudent("김철수", 12);
        student1.WriteStudentInfo(); // 메소드 호출
    }
}