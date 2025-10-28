using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Util;

namespace WpfTabControl.TabControlItem.ViewModels
{
    public class TabItemBaseViewModel : Notifier
    {
        public string Title { get; set; } = "";   // 탭 제목
        public object Content { get; set; } = null;  // 실제 보여줄 UserControl or ViewModel
    }
}
