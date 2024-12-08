namespace Enum_basic
{
    public enum EN_WEEK
    {
        MON,
        TUE,
        WED,
        THUR,
        FRI,
        SAT,
        SUN,
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (EN_WEEK enWeek = EN_WEEK.MON; enWeek <= EN_WEEK.SUN; enWeek++)
            {
                lbWeek.Items.Add(new ListWeekItem(enWeek));
            }
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
        public ListWeekItem(EN_WEEK enWeek)
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
            string strToString = string.Format("enum: {0}, Disp: {1}", Week, Disp);
            return strToString;
        }
    }
}
