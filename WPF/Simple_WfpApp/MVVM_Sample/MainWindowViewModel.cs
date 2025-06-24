using MVVM_Sample.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MVVM_Sample
{
    public class MainWindowViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public ObservableCollection<PersonModel> People { get; set; }

        public PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }
        public MainWindowViewModel()
        {
            People = new ObservableCollection<PersonModel>();
        }
    }
}
