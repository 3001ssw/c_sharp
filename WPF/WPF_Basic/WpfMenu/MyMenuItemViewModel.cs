using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMenu.Util;

namespace WpfMenu
{
    public class MyMenuItemViewModel : Notifier
    {
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
                }
            }
        }

        public MyMenuItemViewModel()
        {
            ButtonCommand = new Command(OnButtonClicked);
            SliderValue = 50;  // 기본값
        }

        private void OnButtonClicked()
        {
            // 버튼 클릭 시 동작 작성
            System.Windows.MessageBox.Show($"Button clicked! SliderValue={SliderValue}");
        }

    }
}
