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
        /// ������ publish ���� �����ִ� �Լ�
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
        /// ������ subscribe ���� �����ִ� �Լ�
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
            lvPublish.Scrollable = true; // ��ũ�� ǥ��
            lvPublish.FullRowSelect = true; // ���� ��ü ����

            lvPublish.Columns.Add("Name", 200);

            lvPublish.Items.Add(new ListViewItemObject(new ClassPublish("����")));
            lvPublish.Items.Add(new ListViewItemObject(new ClassPublish("������")));
            lvPublish.Items.Add(new ListViewItemObject(new ClassPublish("��ġ")));

            // subscribe
            lvSubscribe.Scrollable = true; // ��ũ�� ǥ��
            lvSubscribe.FullRowSelect = true; // ���� ��ü ����

            lvSubscribe.Columns.Add("Name", 200);

            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("������ 1")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("������ 2")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("������ 3")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("������ 4")));
            lvSubscribe.Items.Add(new ListViewItemObject(new ClassSubscribe("������ 5")));
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
        private object _obj; // ClassPublish, ClassSubscribe ����
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