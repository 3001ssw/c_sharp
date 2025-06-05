using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            ScanButton.IsEnabled = false;
            ResultsListBox.Items.Clear();

            if (!IPAddress.TryParse(StartIpTextBox.Text.Trim(), out IPAddress startIp) ||
                !IPAddress.TryParse(EndIpTextBox.Text.Trim(), out IPAddress endIp))
            {
                MessageBox.Show("유효한 시작/끝 IP 주소를 입력하세요.");
                ScanButton.IsEnabled = true;
                return;
            }

            var results = await Task.Run(() => ScanIpRange(startIp, endIp));

            foreach (var res in results)
                ResultsListBox.Items.Add(res);

            ScanButton.IsEnabled = true;
        }

        private List<string> ScanIpRange(IPAddress startIp, IPAddress endIp)
        {
            List<string> results = new List<string>();
            string[] oids = new string[]
            {
            "1.3.6.1.2.1.1.1.0", // sysDescr
            //"1.3.6.1.2.1.1.5.0", // sysName
            "1.3.6.1.2.1.2.2.1.6.1", // ifPhysAddress
            //"1.3.6.1.2.1.1.3.0" // sysUpTime
            };
            string community = "public";

            uint start = IpToUint(startIp);
            uint end = IpToUint(endIp);

            Parallel.For((int)start, (int)end + 1, (i) =>
            {
                IPAddress ip = UintToIp((uint)i);
                try
                {
                    UdpTarget target = new UdpTarget(ip, 161, 2000, 1);
                    OctetString communityStr = new OctetString(community);
                    Pdu pdu = new Pdu(PduType.Get);
                    //pdu.VbList.Add(oid);
                    foreach (var oid in oids)
                        pdu.VbList.Add(oid);

                    AgentParameters param = new AgentParameters(communityStr)
                    {
                        Version = SnmpVersion.Ver2
                    };

                    SnmpV2Packet response = (SnmpV2Packet)target.Request(pdu, param);
                    target.Close();

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(ip);
                    if (response != null && response.Pdu.ErrorStatus == 0)
                    {
                        for (int iIndex = 0; iIndex < response.Pdu.VbList.Count; iIndex++)
                        {
                            string oid = response.Pdu.VbList[iIndex].Oid.ToString();
                            string temp = response.Pdu.VbList[iIndex].Value.ToString();

                            stringBuilder.AppendLine();
                            stringBuilder.Append(oid);
                            stringBuilder.Append(temp);
                        }
                    }
                    else
                        stringBuilder.Append("error");

                    string value = stringBuilder.ToString();
                    lock (results)
                        results.Add($"{value}");
                }
                catch
                {
                    // 무응답 무시
                    results.Add($"{ip} -> 응답 없음");
                }
            });

            return results;
        }
        private uint IpToUint(IPAddress ip)
        {
            byte[] bytes = ip.GetAddressBytes();
            Array.Reverse(bytes); // little endian
            return BitConverter.ToUInt32(bytes, 0);
        }

        private IPAddress UintToIp(uint ipUint)
        {
            byte[] bytes = BitConverter.GetBytes(ipUint);
            Array.Reverse(bytes);
            return new IPAddress(bytes);
        }

    }
}
