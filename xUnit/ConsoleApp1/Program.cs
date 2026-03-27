namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    Console.WriteLine($"Division: {i}/{j} = {MathClass.Division(i, j)}");
                }
            }
        }
    }
}
