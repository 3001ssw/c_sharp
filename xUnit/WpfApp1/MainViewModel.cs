using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MainViewModel : BindableBase
    {
        private string inputText1 = "";
        public string InputText1 { get => inputText1; set => SetProperty(ref inputText1, value); }

        private string intputText2 = "";
        public string InputText2 { get => intputText2; set => SetProperty(ref intputText2, value); }

        private string outputText = "";
        public string OutputText { get => outputText; set => SetProperty(ref outputText, value); }

        public DelegateCommand ShowInputTextCommand { get; private set; }

        public MainViewModel()
        {
            ShowInputTextCommand = new DelegateCommand(OnShowInputText, CanShowInputText).ObservesProperty(() => InputText1).ObservesProperty(() => InputText2);
        }
        private void OnShowInputText()
        {
            int.TryParse(InputText1, out int res1);
            int.TryParse(InputText2, out int res2);
            int res = res1 + res2;
            OutputText = res.ToString();
            InputText1 = "";
            InputText2 = "";
        }

        private bool CanShowInputText()
        {
            if (string.IsNullOrEmpty(InputText1) || string.IsNullOrEmpty(InputText2))
                return false;

            if (int.TryParse(InputText1, out int res1) == false || int.TryParse(InputText2, out int res2) == false)
                return false;

            return true;
        }
    }
}
