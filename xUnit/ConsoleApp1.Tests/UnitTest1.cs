namespace ConsoleApp1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void BasicSumTest()
        {
            // Arrange (준비)
            int a = 7;
            int b = 8;

            // Act (실행)
            int result = MathClass.Sum(a, b);

            // Assert (검증) -> EXPECT_EQ(15, result)와 완벽히 동일합니다.
            Assert.Equal(15, result);
        }

        [Theory]
        [InlineData(10, 5, 2)]   // 10 / 5 = 2
        [InlineData(20, 4, 5)]   // 20 / 4 = 5
        [InlineData(100, 10, 10)] // 100 / 10 = 10
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
                    Assert.Fail($"크래시 검거! 루프: {i}, a: {a}, b: {b} | 원인: {ex.Message}");
                }
            }
        }
    }
}