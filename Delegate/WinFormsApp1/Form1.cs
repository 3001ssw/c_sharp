using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        SubForm? _sub = null;
        ClassPublish pubNews = new ClassPublish("뉴스");
        ClassSubscribe subsNews1 = new ClassSubscribe();
        ClassSubscribe subsNews2 = new ClassSubscribe();
        ClassSubscribe subsNews3 = new ClassSubscribe();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pubNews.eventPublish += subsNews1.GetPublish;
            pubNews.eventPublish += subsNews2.GetPublish;
            pubNews.eventPublish += subsNews3.GetPublish;
        }

        private void btnSubForm_Click(object sender, EventArgs e)
        {
            //SubForm sub = new SubForm();
            //sub.Show(); // 계속 생김
            //if (_sub != null)
            //{
            //    _sub.Dispose();
            //    _sub = null;
            //}
            //
            //_sub = new SubForm();
            //_sub.eventOkButtonClicked += subFormOkButtonClicked;
            //_sub.Show();

            pubNews.Publish();
            Debug.WriteLine(subsNews1.LastContext);
            Debug.WriteLine(subsNews2.LastContext);
            Debug.WriteLine(subsNews3.LastContext);
        }

        private void subFormOkButtonClicked(int iSelect)
        {
            tbSelectSubForm.AppendText(iSelect.ToString() + Environment.NewLine);
        }

    }
}
