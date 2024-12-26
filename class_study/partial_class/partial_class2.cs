namespace partial_class
{
    public partial class CTestMath
    {
        public int sumXnY(int x, int y)
        {
            setLastCalcEn(x, y);
            setLastCalcKor(x, y);

            return x + y;
        }
    }
}
