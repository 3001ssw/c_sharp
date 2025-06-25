using MVVM_Sample.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Sample.ViewModel
{
    public class PersonViewModel
    {
        public PersonModel Person { get; set; }
        public string PersonName
        {
            get { return Person?.Name; }
        }
        public string GenderDisp
        {
            get
            {
                return Person?.Gender == 0 ? "남자" : "여자";
            }
        }
        public PersonViewModel()
        {
            Person = new PersonModel("김", 0, 0);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
