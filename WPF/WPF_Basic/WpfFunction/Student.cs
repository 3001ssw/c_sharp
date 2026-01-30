using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFunction
{
    public class Student : BindableBase
    {
        private Guid id = Guid.NewGuid();
        public Guid ID { get => id; set => SetProperty(ref id, value); }

        private string name = "";
        public string Name { get => name; set => SetProperty(ref name, value); }

        private int score = 0;
        public int Score { get => score; set => SetProperty(ref score, value); }

        public Student()
        {
            MakeRandomNameNScore();
        }

        private void MakeRandomNameNScore()
        {
            Random rand = new Random();

            string[] _lastNames = { "김", "이", "박", "최", "정", "강", "조", "윤", "장", "임", "한", "오", "서" }; // 성
            string[] _nameChars = {"서", "준", "도", "윤", "하", "예", "지", "민", "현", "우", "은", "빈", "나",
                "건", "유", "진", "슬", "율", "주", "연", "아", "태", "정", "훈", "승", "채", "가", "선", "리", "안" };// 이름

            string lastName = _lastNames[rand.Next(0, _lastNames.Length)]; // 성
            string char1 = _nameChars[rand.Next(0, _nameChars.Length)]; // 이름1
            string char2 = _nameChars[rand.Next(0, _nameChars.Length)]; // 이름2

            Name = $"{lastName}{char1}{char2}";
            Score = rand.Next(0, 101);
        }
    }
}
