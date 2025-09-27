namespace WpfTabControl
{
    public class TabItemViewModel
    {
        public string Header { get; set; } = "";   // 탭 제목
        public object Content { get; set; } = null;  // 실제 보여줄 UserControl or ViewModel
    }
}