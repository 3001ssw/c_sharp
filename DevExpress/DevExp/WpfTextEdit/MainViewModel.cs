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
            set { SetValue(value); }
        }

        // MemoEdit
        public string Memo
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        // PasswordBoxEdit
        public string Password
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        // ButtonEdit
        public string ButtonEditText
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        // BrowsePathEdit
        public string BrowsePath
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        // DateEdit
        public DateTime? SelectedDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        // FontEdit
        public string SelectedFont
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        // SpinEdit
        public decimal SpinValue
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        // PasswordBoxEdit 커맨드
        public DelegateCommand<EditValueChangedEventArgs> PasswordChangedCommand { get; private set; }

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

            SelectedDate = DateTime.Today;
            SpinValue = 0;
        }

        private void PasswordChanged(EditValueChangedEventArgs args)
        {
            if (args != null)
            {
                Password = args.NewValue?.ToString();
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
    }
}
