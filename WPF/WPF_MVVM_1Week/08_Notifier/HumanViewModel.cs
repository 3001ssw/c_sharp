using System.Collections.Generic;
using System.Linq;

namespace _08_Notifier
{
    public class HumanViewModel : Notifier
    {
        public Person Person { get; set; } = new Person(); // ObservableCollection<Human>

        public HumanViewModel()
        {
            SelectedHuman = Person.FirstOrDefault(); // 초기 선택값
        }

        // 리스트에서 선택했을 때 전달?되는 객체
        private Human _selectedHuman;
        public Human SelectedHuman
        {
            get
            {
                return _selectedHuman;
            }
            set
            {
                _selectedHuman = value;
                OnPropertyChanged(nameof(SelectedHuman));
            }
        }

        private string _searchHuman;
        public string SearchHuman
        {
            get
            {
                return _searchHuman;
            }
            set
            {
                _searchHuman = value;
                OnPropertyChanged(nameof(SearchHuman));
                OnSearchInputChanged();
            }
        }

        // 검색 텍스트 변경했을 때, 해당 문자열로 찾은 객체 리스트
        private IEnumerable<Human> _findHuman;

        public IEnumerable<Human> FindHuman
        {
            get { return _findHuman; }
            set
            {
                _findHuman = value;
                OnPropertyChanged("FindHuman");
            }
        }

        // 검색 텍스트 변경되면 발생하는 이벤트?
        private void OnSearchInputChanged()
        {
            SelectedHuman = null;

            FindHuman = Person.FindHuman(SearchHuman);
        }
    }

}
