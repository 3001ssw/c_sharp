using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using WpfTreeView.ViewModels;
using WpfTreeView.Views;

namespace WpfTreeView
{
    public class MainWindowViewModel : Notifier
    {
        public ObservableCollection<BaseViewModel> Items { get; set; } = new ObservableCollection<BaseViewModel>();
        public MainWindowViewModel()
        {
            FolderViewModel root = new FolderViewModel()
            {
                Name = "root",
                Children =
                {
                    new FolderViewModel() { Name = "A", },
                    new FolderViewModel()
                    {
                        Name = "B",
                        Children =
                        {
                            new FileViewModel()
                            {
                                Name = "f2",
                                FileSize = 200,
                            }
                        },
                    },
                    new FolderViewModel() { Name = "C", },
                    new FileViewModel()
                    {
                        Name = "f1",
                        FileSize = 100,
                    }
                },
            };

            Items.Add(root);

        }
    }
}