using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 선택한 publish 정보 보여주는 함수
        /// </summary>
        public void InvalidatePublish()
        {
            if (0 < lvPublish.SelectedItems.Count)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < lvPublish.SelectedItems.Count; i++)
                {
                    ListViewItemObject lvItem = (ListViewItemObject)lvPublish.SelectedItems[i];
                    ClassPublish clsPub = (ClassPublish)lvItem.Obj;
                    stringBuilder.AppendLine($"{clsPub.Name}: {clsPub.LastPublish}");
                }
                tbPublish.Text = stringBuilder.ToString();
            }
        }

        /// <summary>
        /// 선택한 subscribe 정보 보여주는 함수
        /// </summary>
        public void InvalidateSubscribe()
        {
            if (0 < lvSubscribe.SelectedItems.Count)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < lvSubscribe.SelectedItems.Count; i++)
                {
                    ListViewItemObject lvItem = (ListViewItemObject)lvSubscribe.SelectedItems[i];
                    ClassSubscribe clsSubs = (ClassSubscribe)lvItem.Obj;
                    stringBuilder.AppendLine($"==== {clsSubs.Name}====");
                    stringBuilder.AppendLine($"{clsSubs.AllContext}");
                }
                tbSubcribe.Text = stringBuilder.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // publish
            lvPublish.Scrollable = true; // 스크롤 표시
            lvPublish.FullRowSelect = true; // 한줄 전체 선택

            lvPublish.Columns.Add("Name", 200);

            lvPublish.Items.Add(new ListViewItemObject(new ClassPublish("뉴스")));
            lvPublish.Items.Add(new ListViewItemObject(new ClassPublish("스포츠")));
            lvPublish.Items.Add(new ListViewItemObject(new ClassPublish("정치")));

            // subscribe
            lvSubscribe.Scrollable = true; // 스크롤 표시
            lvSubscribe.FullRowSelect = true; // 한줄 전체 선택

            lvSubscribe.Columns.Add("Name", 200);

            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("구독자 1")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("구독자 2")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("구독자 3")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("구독자 4")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("구독자 5")));
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (0 < lvPublish.SelectedItems.Count)
            {
                for (int i = 0; i < lvPublish.SelectedItems.Count; i++)
                {
                    ListViewItemObject lvItem = (ListViewItemObject)lvPublish.SelectedItems[i];
                    ClassPublish clsPub = (ClassPublish)lvItem.Obj;
                    clsPub.Publish();
                }
            }
            InvalidatePublish();
            InvalidateSubscribe();
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            if (0 < lvSubscribe.SelectedItems.Count && 0 < lvPublish.SelectedItems.Count)
            {
                for (int iSubsItem = 0; iSubsItem < lvSubscribe.SelectedItems.Count; iSubsItem++)
                {
                    ListViewItemObject lvItemSubs = (ListViewItemObject)lvSubscribe.SelectedItems[iSubsItem];
                    ClassSubscribe clsSubs = (ClassSubscribe)lvItemSubs.Obj;
                    for (int iPubItem = 0; iPubItem < lvPublish.SelectedItems.Count; iPubItem++)
                    {
                        ListViewItemObject lvItem = (ListViewItemObject)lvPublish.SelectedItems[iPubItem];
                        ClassPublish clsPub = (ClassPublish)lvItem.Obj;

                        clsPub.eventPublish += clsSubs.GetPublish;
                    }
                }
            }
            InvalidatePublish();
            InvalidateSubscribe();
        }

        private void btnCancelSubscribe_Click(object sender, EventArgs e)
        {
            if (0 < lvSubscribe.SelectedItems.Count && 0 < lvPublish.SelectedItems.Count)
            {
                for (int iSubsItem = 0; iSubsItem < lvSubscribe.SelectedItems.Count; iSubsItem++)
                {
                    ListViewItemObject lvItemSubs = (ListViewItemObject)lvSubscribe.SelectedItems[iSubsItem];
                    ClassSubscribe clsSubs = (ClassSubscribe)lvItemSubs.Obj;
                    for (int iPubItem = 0; iPubItem < lvPublish.SelectedItems.Count; iPubItem++)
                    {
                        ListViewItemObject lvItem = (ListViewItemObject)lvPublish.SelectedItems[iPubItem];
                        ClassPublish clsPub = (ClassPublish)lvItem.Obj;

                        clsPub.eventPublish -= clsSubs.GetPublish;
                    }
                }
            }
        }

        private void lvPublish_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvalidatePublish();
        }

        private void lvSubscribe_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvalidateSubscribe();
        }
    }
    public class ListViewItemObject : ListViewItem
    {
        private object _obj; // ClassPublish, ClassSubscribe 저장
        public object Obj
        {
            get { return _obj; }
        }

        public ListViewItemObject(Object obj)
        {
            _obj = obj;
            Text = _obj.ToString();
        }
    }
}