using System.Windows.Automation.Provider;

namespace WpfApp1.Tests;

public class MathClassTest
{
    [Fact]
    public void SumTest1()
    {
        Random rand = new Random();

        for (int i = 0; i < 10000000; i++)
        {
            int input1 = rand.Next(int.MinValue, int.MaxValue) % 100;
            int input2 = rand.Next(int.MinValue, int.MaxValue) % 100;

            try
            {
                MathClass.Sum(input1, input2);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Assert.Fail intput1: {input1}, intput2: {input2} | 원인: {ex.Message}");
            }
        }
    }

    [Fact]
    public void DivTest1()
    {
        Random rand = new Random();

        for (int i = 0; i < 10000000; i++)
        {
            int input1 = rand.Next(int.MinValue, int.MaxValue) % 100;
            int input2 = rand.Next(int.MinValue, int.MaxValue) % 100;

            MathClass.Division(input1, input2);
            //try
            //{
            //    MathClass.Division(input1, input2);
            //}
            //catch (Exception ex)
            //{
            //    Assert.Fail($"Assert.Fail intput1: {input1}, intput2: {input2} | 원인: {ex.Message}");
            //}
        }
    }

    [Theory]
    [InlineData(10, 20)]
    [InlineData(30, 40)]
    [InlineData(10, 0)]
    [InlineData(0, 12)]
    public void DivTest2(int in1, int in2)
    {
        MathClass.Division(in1, in2);
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
