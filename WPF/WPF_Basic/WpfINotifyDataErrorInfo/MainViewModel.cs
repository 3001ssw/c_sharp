using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfINotifyDataErrorInfo
{
    public class MainViewModel : BindableBase, INotifyDataErrorInfo
    {
        private string inputText1 = "";
        public string InputText1
        {
            get => inputText1;
            set
            {
                SetProperty(ref inputText1, value);
                Validate();
            }
        }
        private string inputText2 = "";
        public string InputText2
        {
            get => inputText2;
            set
            {
                SetProperty(ref inputText2, value);
                Validate();
            }
        }

        // 오류를 저장하는 사전
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        private void Validate([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null)
                return;

            ClearErrors(propertyName);

            if (propertyName == nameof(InputText1))
            {
                if (string.IsNullOrEmpty(InputText1) || !InputText1.Any(char.IsDigit))
                    AddError(propertyName, "문자열에는 숫자가 하나 이상 포함되어야 합니다.");
            }
            else if (propertyName == nameof(InputText2))
            {
                if (string.IsNullOrEmpty(InputText2) || !InputText2.All(char.IsLetter))
                    AddError(propertyName, "문자만 포함되어야 합니다.");
            }
        }

        // --- 오류 관리 함수들 ---
        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            _errors[propertyName].Add(error);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        // INotifyDataErrorInfo 구현부
        public IEnumerable GetErrors(string? propertyName)
            => (propertyName != null && _errors.ContainsKey(propertyName)) ? _errors[propertyName] : null;

        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
