namespace ConsoleApp1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void BasicSumTest()
        {
            int a = 7;
            int b = 8;

            int result = MathClass.Sum(a, b);

            Assert.Equal(15, result);
        }

        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(20, 4, 5)]
        [InlineData(100, 10, 10)]
        public void DivisionTest(int a, int b, int expected)
        {
            Assert.Equal(expected, MathClass.Division(a, b));
        }

        [Fact]
        public void DivisionRandomTest()
        {
            Random rand = new Random();

            for (int i = 0; i < 10000000; i++)
            {
                int a = rand.Next(int.MinValue, int.MaxValue) % 100;
                int b = rand.Next(int.MinValue, int.MaxValue) % 100;

                try
                {
                    MathClass.Division(a, b);
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Assert.Fail a: {a}, b: {b} | ¿øÀÎ: {ex.Message}");
                }
            }
        }
    }
}