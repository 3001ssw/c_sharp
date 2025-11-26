namespace _guid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string strGuid = Guid.NewGuid().ToString();
            Console.WriteLine($"GUID: {strGuid}");

            if (Guid.TryParse(strGuid, out Guid guid))
            {
                Console.WriteLine($"GUID: {guid}");
            }
        }
    }
}
