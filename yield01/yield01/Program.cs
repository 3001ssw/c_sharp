using static System.Runtime.InteropServices.JavaScript.JSType;

namespace yield01
{
    internal class Program
    {
        static IEnumerable<int> GetNumber()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        static IEnumerable<int> GetData(int[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                if (data[i] == -1)
                {
                    Console.WriteLine($"index: {i}, find -1");
                    yield break;
                }

                yield return data[i];
            }
        }


        static void Main(string[] args)
        {
            foreach (int num in GetNumber())
            {
                Console.WriteLine(num);
            }

            int[] data = { 1, 2, 3, 4, 5, 6, 7, -1, 8, 9, 10 };
            foreach (int num in GetData(data))
            {
                Console.WriteLine(num);
            }

            //IEnumerable<int> enumerable = GetNumber();
            //IEnumerator<int> it = enumerable.GetEnumerator();
            //it.MoveNext();
            //int a = it.Current;
            //it.MoveNext();
            //int b = it.Current;
            //it.MoveNext();
            //int c = it.Current;
            //foreach (int num in enumerable)
            //{
            //    Console.WriteLine(num);
            //}
            //
            //foreach (var n in CountTo(100))
            //{
            //    Console.WriteLine($"받음: {n}");
            //}
        }
    }
}
