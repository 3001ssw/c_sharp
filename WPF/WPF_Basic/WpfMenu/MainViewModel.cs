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
        /// <summary>
        /// 메시지
        /// </summary>
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

        public MainViewModel()
        {
            // 메시지
            Message = "메뉴를 선택하세요.";

            // 기본 메뉴
            OpenCommand = new Command(Open);
            SaveCommand = new Command(Save);
            ExitCommand = new Command(Exit);

            // 체크박스 메뉴
            CheckedMenuCommand = new Command(CommandChecked);

            // MyMenuItem
            ButtonCommand = new Command(OnButtonClicked);
            _sliderValue = 50;  // 기본값
        }

        /// <summary>
        /// 기본 메뉴
        /// </summary>
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ExitCommand { get; }

        private void Open() => Message = "Open 클릭됨!";
        private void Save() => Message = "Save 클릭됨!";
        private void Exit() => Application.Current.Shutdown();

        /// <summary>
        /// 체크 박스 메뉴
        /// </summary>
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
        public ICommand CheckedMenuCommand { get; }

        private void CommandChecked() => Message = "Check 메뉴 클릭됨! : " + IsCheckMenu.ToString();
        
        /// <summary>
        /// 라디오 메뉴
        /// </summary>
        public ObservableCollection<string> _comboItemsSource = new ObservableCollection<string>
        {
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

        private void ApplyTheme(string theme) => Message = "Radio 메뉴 선택됨! : " + theme;

        /// <summary>
        /// MyMenuItem
        /// </summary>
        public ICommand ButtonCommand { get; }

        private double _sliderValue;
        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    OnPropertyChanged(nameof(SliderValue));
                    Message = $"SliderValue={SliderValue}";
                }
            }
        }

        private void OnButtonClicked() => Message = "Button clicked!";
    }
}
