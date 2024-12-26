namespace partial_class
{
    public partial class CTestMath
    {
        public CTestMath() { }

        private string _last_calc_en= ""; // 마지막 계산 영어로 저장
        public string LastCalcEn
        {
            get { return _last_calc_en; }
        }
        private void setLastCalcEn(int x, int y)
        {
            _last_calc_en = string.Format($"{x} plus {y} is {x + y}.");
        }


        private string _last_calc_kor = ""; // 마지막 계산 한국어로 저장
        public string LastCalcKor
        {
            get { return _last_calc_kor; }
        }
        private void setLastCalcKor(int x, int y)
        {
            _last_calc_kor = string.Format($"{x} 더하기 {y} 는 {x+y} 입니다.");
        }

        
    }
}