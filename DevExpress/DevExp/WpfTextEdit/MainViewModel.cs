using DevExpress.Mvvm;
using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTextEdit
{
    class MainViewModel : ViewModelBase
    {
        // TextEdit
        public string UserName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                UpdateResult();
            }
        }

        // MemoEdit
        public string Memo
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                UpdateResult();
            }
        }

        // PasswordBoxEdit
        public string Password
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                UpdateResult();
            }
        }
        public DelegateCommand<EditValueChangedEventArgs> PasswordChangedCommand { get; private set; }

        // ButtonEdit
        public string ButtonEditText
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                UpdateResult();
            }
        }

        // 결과 표시
        public string ResultText
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        // ButtonEdit 커맨드
        public DelegateCommand DefaultButtonClickCommand { get; private set; }
        public DelegateCommand ButtonClickCommand { get; private set; }
        public DelegateCommand ButtonClickInitCommand { get; private set; }

        public MainViewModel()
        {
            PasswordChangedCommand = new DelegateCommand<EditValueChangedEventArgs>(PasswordChanged);
            DefaultButtonClickCommand = new DelegateCommand(DefaultButtonClick);
            ButtonClickCommand = new DelegateCommand(OnButtonClick);
            ButtonClickInitCommand = new DelegateCommand(OnButtonClickInit, CanButtonClickInit);

            ResultText = "입력하면 여기에 표시돼요";
        }

        private void PasswordChanged(EditValueChangedEventArgs args)
        {
            if (args != null)
            {
                Password = args.NewValue?.ToString();
                UpdateResult();
            }
        }

        private void DefaultButtonClick()
        {
            ButtonEditText = "기본 버튼이 클릭됐어요!";
        }

        private void OnButtonClick()
        {
            ButtonEditText = "버튼이 클릭됐어요!";
        }
        private void OnButtonClickInit()
        {
            ButtonEditText = "";
        }

        private bool CanButtonClickInit()
        {
            return true;
        }

        private void UpdateResult()
        {
            ResultText = $"이름: {UserName}\n메모: {Memo}\n비밀번호 길이: {Password?.Length ?? 0}자리";
        }
    }
}
