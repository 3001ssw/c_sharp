using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_Notifier
{
    public class HumanViewModel : Notifier
    {
        public Person Humans { get; set; } = new Person(); // ObservableCollection<Human>

        private Human _selectedHuman;
        public Human SelectedHuman
        {
            get => _selectedHuman;
            set
            {
                _selectedHuman = value;
                OnPropertyChanged(nameof(SelectedHuman));
            }
        }

        public HumanViewModel()
        {
            SelectedHuman = Humans.FirstOrDefault(); // 초기 선택값
        }
    }

}
