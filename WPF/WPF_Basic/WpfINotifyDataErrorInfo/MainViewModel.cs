using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfINotifyDataErrorInfo
{
    public class Person : BindableBase, INotifyDataErrorInfo
    {
        private string name = "";
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                ValidateProperty();
            }
        }

        private int age = 0;
        public int Age
        {
            get => age;
            set
            {
                SetProperty(ref age, value);
                ValidateProperty();
            }
        }

        public Person()
        {
            ValidateProperty(null);
        }

        private readonly Dictionary<string, List<string>> dictErrors = new Dictionary<string, List<string>>();
        private void ValidateProperty([CallerMemberName] string? propertyName = null)
        {
            // null인 경우엔 속성 모두 검증하기 위해 모두 지우기
            if (propertyName == null)
                dictErrors.Clear();
            else
            {
                // 이름이 있으면 해당 속성만 지우기
                if (dictErrors.ContainsKey(propertyName))
                    dictErrors.Remove(propertyName);
            }

            // 속성 검증
            if (propertyName == null || propertyName == nameof(Name))
            {
                List<string> message = new List<string>();
                if (string.IsNullOrEmpty(Name))
                    message.Add("빈 문자열은 입력될 수 없습니다.");
                if (0 < message.Count)
                    dictErrors[nameof(Name)] = new List<string>(message);
            }
            if (propertyName == null || propertyName == nameof(Age))
            {
                List<string> message = new List<string>();
                if (string.IsNullOrEmpty(Age.ToString()))
                    message.Add("빈 문자열은 입력될 수 없습니다.");
                if (Age < 0)
                    message.Add("나이는 0살보다 작을 수 없습니다.");
                if (150 < Age)
                    message.Add("나이는 150살보다 클 수 없습니다.");
                if (0 < message.Count)
                    dictErrors[nameof(Age)] = new List<string>(message);
            }

            // 에러가 change 됐다는 이벤트 발생
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public bool HasErrors => 0 < dictErrors.Count;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return Enumerable.Empty<string>();

            if (dictErrors.ContainsKey(propertyName))
                return dictErrors[propertyName];

            return Enumerable.Empty<string>();
        }
    }


    public class MainViewModel : BindableBase, INotifyDataErrorInfo
    {
        private string inputText = "";
        public string InputText
        {
            get => inputText;
            set
            {
                SetProperty(ref inputText, value);
                //ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(InputText)));
                ValidateProperty();
            }
        }

        private string inputText1 = "";
        public string InputText1
        {
            get => inputText1;
            set
            {
                SetProperty(ref inputText1, value);
                ValidateProperty();
            }
        }

        private string inputText2 = "";
        public string InputText2
        {
            get => inputText2;
            set
            {
                SetProperty(ref inputText2, value);
                ValidateProperty();
            }
        }

        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();

        public MainViewModel()
        {
            ValidateProperty(null);
        }

        //// 현재 ViewModel에 하나라도 오류가 있는지 여부를 반환하는 bool 프로퍼티입니다.
        //public bool HasErrors => throw new NotImplementedException();
        //// 프로퍼티의 에러 상태가 바뀌었을 때 UI에게 알려주는 이벤트입니다.
        //public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        //// 해당 프로퍼티에 대한 에러(문자열 또는 객체)를 반환합니다.
        //public IEnumerable GetErrors(string? propertyName)
        //{
        //    throw new NotImplementedException();
        //}

        // 속성 1개 인 경우
        //public bool HasErrors => InputText.Any(char.IsDigit);

        //public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        //public IEnumerable GetErrors(string? propertyName)
        //{
        //    if (propertyName == null || propertyName == nameof(InputText))
        //    {
        //        if (InputText.Any(char.IsDigit))
        //            return new string[] { "숫자는 입력할 수 없습니다." };
        //    }
        //
        //    return Enumerable.Empty<string>();
        //}


        /// <summary>
        /// key(string): 속성, value(List<string>): 오류
        /// </summary>
        private readonly Dictionary<string, List<string>> dictErrors = new Dictionary<string, List<string>>();
        private void ValidateProperty([CallerMemberName] string? propertyName = null)
        {
            // null인 경우엔 속성 모두 검증하기 위해 모두 지우기
            if (propertyName == null)
                dictErrors.Clear();
            else
            {
                // 이름이 있으면 해당 속성만 지우기
                if (dictErrors.ContainsKey(propertyName))
                    dictErrors.Remove(propertyName);
            }

            // 속성 검증
            if (propertyName == null || propertyName == nameof(InputText))
            {
                if (InputText.Any(char.IsDigit))
                {
                    dictErrors[nameof(InputText)] = new List<string>();
                    dictErrors[nameof(InputText)].Add("숫자는 입력할 수 없습니다.");
                }
            }
            if (propertyName == null || propertyName == nameof(InputText1))
            {
                List<string> message = new List<string>();
                if (string.IsNullOrEmpty(InputText1))
                    message.Add("빈 문자열은 입력될 수 없습니다.");
                if (!InputText1.Any(char.IsDigit))
                    message.Add("숫자가 하나 이상 입력되어야 합니다.");
                if (0 < message.Count)
                    dictErrors[nameof(InputText1)] = new List<string>(message);
            }
            if (propertyName == null || propertyName == nameof(InputText2))
            {
                List<string> message = new List<string>();
                if (string.IsNullOrEmpty(InputText2))
                    message.Add("빈 문자열은 입력될 수 없습니다.");
                if (!InputText2.All(char.IsDigit))
                    message.Add("숫자만 입력되어야 합니다.");
                if (0 < message.Count)
                    dictErrors[nameof(InputText2)] = new List<string>(message);
            }
        
            // 에러가 change 됐다는 이벤트 발생
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }


        public bool HasErrors => 0 < dictErrors.Count;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
                return Enumerable.Empty<string>();
        
            if (dictErrors.ContainsKey(propertyName))
               return dictErrors[propertyName];
        
            return Enumerable.Empty<string>();
        }
    }
}
