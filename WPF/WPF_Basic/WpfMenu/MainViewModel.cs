using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfMenu.Util;

namespace WpfMenu
{
    public class MainViewModel : Notifier
    {

        private string? _message;
        public string? Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool _isCheckedMenu;
        public bool IsCheckMenu
        {
            get => _isCheckedMenu;
            set
            {
                if (_isCheckedMenu != value)
                {
                    _isCheckedMenu = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<string> _comboItemsSource = new ObservableCollection<string> {
            "Item1", "Item2", "Item3"
        };

        public ObservableCollection<string> ComboItemsSource
        {
            get => _comboItemsSource;
        }

        private string _selectedComboItem = "Item1";
        public string SelectedComboItem
        {
            get => _selectedComboItem;
            set
            {
                if (_selectedComboItem != value)
                {
                    _selectedComboItem = value;
                    OnPropertyChanged();
                    ApplyTheme(_selectedComboItem);
                }
            }
        }

        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExitCommand { get; }

        public ICommand CheckedMenuCommand { get; }

        public MainViewModel()
        {
            OpenCommand = new Command(Open);
            SaveCommand = new Command(Save);
            ExitCommand = new Command(Exit);

            Message = "메뉴를 선택하세요.";

            CheckedMenuCommand = new Command(CommandChecked);
        }

        private void Open() => Message = "Open 클릭됨!";
        private void Save() => Message = "Save 클릭됨!";
        private void Exit() => Application.Current.Shutdown();


        private void CommandChecked()
        {
            Message = "Check 메뉴 클릭됨! : " + IsCheckMenu.ToString();
        }

        private void ApplyTheme(string theme)
        {
            Message = "Radio 메뉴 선택됨! : " + theme;
        }
    }
}
