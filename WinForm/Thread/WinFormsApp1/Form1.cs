
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<CTest> listThread = new List<CTest>();
        StringBuilder sb = new StringBuilder();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (CTest thread in listThread)
            {
                thread.StopThread();
            }
        }

        private void btnThreadStart_Click(object sender, EventArgs e)
        {
            int iCount = (int)numThreadCount.Value;
            for (int i = 0; i < iCount; i++)
            {
                CTest test = new CTest(i);
                test.OnThreadMessage += OnThreadMessage;
                test.StartThread();
                listThread.Add(test);
            }
        }

        private void OnThreadMessage(object sender, string message)
        {
            CTest? cTest = sender as CTest;
            if (cTest != null)
            {
                if (this.InvokeRequired)
                {
                    string strMessage = string.Format($"{cTest.ToString()} : {message}");
                    //sb.AppendLine(strMessage);
                    this.Invoke(new Action(delegate ()
                    {
                        string strAppend = string.Format($"{strMessage}" + Environment.NewLine);
                        tbOutput.AppendText(strAppend);
                    }));
                }
            }
        }
    }
}
