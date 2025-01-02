using System.Diagnostics;
using System.IO;
using System.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string _file_name = ".\\test.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] items = {"��", "��", "��", "��"};
            cbCombo.Items.AddRange(items);
            cbCombo.SelectedIndex = 0;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader streamReader = new StreamReader(_file_name, //���
                                                             Encoding.UTF8); // ���ڵ�
                if (streamReader.EndOfStream != true)
                {
                    string? strTemp = null;

                    strTemp = streamReader.ReadLine();
                    if (strTemp != null)
                        tbText.Text = strTemp;

                    strTemp = streamReader.ReadLine();
                    if (strTemp != null)
                        chkCheckOption.Checked = bool.Parse(strTemp);

                    strTemp = streamReader.ReadLine();
                    if (strTemp != null)
                        cbCombo.Text = strTemp;
                    strTemp = streamReader.ReadLine();
                    if (strTemp != null)
                        rdOption1.Checked = bool.Parse(strTemp);

                    strTemp = streamReader.ReadLine();
                    if (strTemp != null)
                        rdOption2.Checked = bool.Parse(strTemp);

                    strTemp = streamReader.ReadLine();
                    if (strTemp != null)
                        rdOption3.Checked = bool.Parse(strTemp);
                }
                streamReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter(_file_name, // ���
                                                         false, //true: �ڿ� �߰�, false: ���� ����,
                                                         Encoding.UTF8); // ���ڵ�
            streamWriter.WriteLine(tbText.Text);
            streamWriter.WriteLine(chkCheckOption.Checked.ToString());
            streamWriter.WriteLine(cbCombo.Text);
            streamWriter.WriteLine(rdOption1.Checked.ToString());
            streamWriter.WriteLine(rdOption2.Checked.ToString());
            streamWriter.WriteLine(rdOption3.Checked.ToString());
            streamWriter.Close();
        }
    }
}
