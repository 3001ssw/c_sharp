using System.Collections;

namespace ConsoleApp1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void BasicSumTest()
        {
            int a = 7;
            int b = 8;

            int result = MathClass.Sum(a, b) + 1;

            Assert.Equal(15, result);
            //Assert.Equal(16, result);
            //Assert.True(15 == result);
            //Assert.False(16 == result);

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
                    Assert.Fail($"Assert.Fail a: {a}, b: {b} | 원인: {ex.Message}"); // 한번 에러나면 끝
                }
            }
        }


        [Theory]
        [InlineData(10, 5, 2)]
        [InlineData(20, 4, 5)]
        [InlineData(100, 10, 10)]
        [InlineData(0, 0, 0)]
        public void DivisionTest(int a, int b, int expected)
        {
            Assert.Equal(expected, MathClass.Division(a, b));
        }



        #region MemberData

        public static IEnumerable<object[]> DivData()
        {
            List<object[]> ret = new List<object[]>();

            Random rand = new Random();

            for (int i = 0; i < 1000; i++)
            {
                int input1 = rand.Next(int.MinValue, int.MaxValue) % 100;
                int input2 = rand.Next(int.MinValue, int.MaxValue) % 100;
                object[] obj = new object[] { input1, input2 };
                ret.Add(obj);
            }
            return ret;
        }

        [Theory]
        [MemberData(nameof(DivData))]
        public void DivTest3(int input1, int input2)
        {
            int div = MathClass.Division(input1, input2);
        }
        #endregion
    }
}