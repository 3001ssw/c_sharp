using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace WpfTreeView
{
    public class MainWindowViewModel : Notifier
    {
        public List<TreeItemViewModel> Items { get; set; }
        public MainWindowViewModel()
        {

            Items = new List<TreeItemViewModel>()
            {
                new TreeItemViewModel()
                {
                    Header = "과일",
                    Children =
                    {
                        new TreeItemViewModel { Header = "사과" },
                        new TreeItemViewModel { Header = "바나나" },
                        new TreeItemViewModel { Header = "포도" },
                    }
                },
                new TreeItemViewModel
                {
                    Header = "동물",
                    Children =
                    {
                        new TreeItemViewModel { Header = "강아지" },
                        new TreeItemViewModel { Header = "고양이" },
                    }
                }
            };
        }
    }
}