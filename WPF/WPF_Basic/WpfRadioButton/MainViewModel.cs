using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfRadioButton.FruitEnumToBoolConverter;

namespace WpfRadioButton
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties

        private FruitType selectedFruit = FruitType.Apple;
        public FruitType SelectedFruit
        {
            get => selectedFruit;
            set => SetProperty(ref selectedFruit, value);
        }

        #endregion


        #region constructor
        public MainViewModel()
        {

        }
        #endregion
    }
}
