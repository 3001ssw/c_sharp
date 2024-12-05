namespace Enum_basic
{
    public enum EN_WEEK
    {
        UNKNOWN = -1,
        MON = 0,
        TUE,
        WED,
        THUR,
        FRI,
        SAT,
        SUN,

        COUNT,
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.SUN));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.MON));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.MON));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.MON));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.SUN));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.THUR));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.SUN));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.FRI));
            lbWeek.Items.Add(new ListWeekItem(EN_WEEK.SUN));
        }

        private void lbWeek_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(lbWeek))
            {
                tbSelect.Text = lbWeek.Items[lbWeek.SelectedIndex].ToString();
            }
        }
    }

    public class ListWeekItem : ListViewItem
    {
        public ListWeekItem(EN_WEEK enWeek = EN_WEEK.UNKNOWN)
        {
            Week = enWeek;
        }

        private EN_WEEK enWeek;
        public EN_WEEK Week
        {
            set
            {
                enWeek = value;
                switch (enWeek)
                {
                    case EN_WEEK.SUN:
                        strDisp = "sunday";
                        break;
                    case EN_WEEK.MON:
                        strDisp = "monday";
                        break;
                    case EN_WEEK.TUE:
                        strDisp = "tuesday";
                        break;
                    case EN_WEEK.WED:
                        strDisp = "wednesday";
                        break;
                    case EN_WEEK.THUR:
                        strDisp = "thursday";
                        break;
                    case EN_WEEK.FRI:
                        strDisp = "friday";
                        break;
                    case EN_WEEK.SAT:
                        strDisp = "saturday";
                        break;
                    default:
                        strDisp = "UNKNOWN";
                        break;
                }

            }
            get { return enWeek; }
        }
        private string strDisp = "";

        public string Disp
        {
            get
            {
                return strDisp;
            }
        }

        public override string ToString()
        {
            string strToString = string.Format("{0}: {1}", Week, Disp);
            return strToString;
        }
    }
}
