using System;

using System.Collections.Generic;
using System.Diagnostics;
using SnmpSharpNet;

namespace ConsoleApp1
{
    class LldpNeighborsSample
    {
        // ===== 기본 SNMP 세팅 =====
        static SnmpV2Packet Bulk(UdpTarget target, AgentParameters param, Pdu pdu)
            => (SnmpV2Packet)target.Request(pdu, param);

        static IEnumerable<Vb> WalkColumn(UdpTarget target, AgentParameters param, string columnOid, int maxReps = 50)
        {
            var baseOid = new Oid(columnOid);
            var nextOid = new Oid(columnOid);
            Debug.WriteLine($"column: {columnOid}");
            while (true)
            {
                var pdu = new Pdu(PduType.GetBulk) { NonRepeaters = 0, MaxRepetitions = maxReps };
                Debug.WriteLine($"nextOid: {nextOid}");
                pdu.VbList.Add(nextOid);

                var resp = Bulk(target, param, pdu);
                if (resp == null || resp.Pdu.ErrorStatus != 0 || resp.Pdu.VbList.Count == 0)
                    yield break;

                bool any = false;
                foreach (Vb vb in resp.Pdu.VbList)
                {
                    // 다른 subtree로 넘어가면 종료
                    if (!vb.Oid.ToString().StartsWith(baseOid.ToString() + "."))
                        yield break;

                    any = true;
                    yield return vb;
                    nextOid = vb.Oid;
                }
                if (!any)
                    yield break;

                // nextOid를 한 스텝 증가시켜 다음부터 이어감
                nextOid = nextOid + 1;
            }
        }

        static string ToAsciiString(AsnType v)
        {
            try
            {
                // LLDP 문자열은 OCTET STRING이 많음
                return v.ToString();
            }
            catch { return ""; }
        }

        public static void Main()
        {
            string agent = "192.168.10.181";
            string community = "public";
            int port = 161;
            using var target = new UdpTarget((System.Net.IPAddress)new IpAddress(agent), port, 3000, 1);
            var param = new AgentParameters(new OctetString(community)) { Version = SnmpVersion.Ver2 };

            // ---- 1) 로컬 포트 번호(lldpLocPortNum) → 로컬 포트 설명(lldpLocPortDesc) 매핑
            // key: lldpLocPortNum(int), val: desc(string)
            var localPortDesc = new Dictionary<int, string>();
            foreach (var vb in WalkColumn(target, param, "1.0.8802.1.1.2.1.3.7.1.4")) // lldpLocPortDesc
            {
                // OID 끝자리가 lldpLocPortNum
                int lpNum = -1;
                try
                { lpNum = int.Parse(vb.Oid[vb.Oid.Length - 1].ToString()); }
                catch { }
                if (lpNum >= 0)
                    localPortDesc[lpNum] = ToAsciiString(vb.Value);
            }

            // ---- 2) 원격(이웃) 테이블 각 컬럼을 Walk해서 묶기
            // remoteKey = "timeMark.localPortNum.remIndex" 문자열 키로 묶어 합치기
            var remSysName = new Dictionary<string, string>();
            var remPortDesc = new Dictionary<string, string>();
            var remChassisId = new Dictionary<string, string>();
            var remPortId = new Dictionary<string, string>();

            // 공통 파서: OID에서 인덱스 3개 추출하여 "t.l.r" 키 생성
            Func<Oid, string> keyFromRemIndex = (oid) =>
            {
                // lldpRemEntry index = timeMark, localPortNum, remIndex (끝 3개)
                int c = oid.Length;
                if (c < 3)
                    return null;
                var t = oid[c - 3].ToString();
                var lp = oid[c - 2].ToString();
                var r = oid[c - 1].ToString();
                return $"{t}.{lp}.{r}";
            };

            foreach (var vb in WalkColumn(target, param, "1.0.8802.1.1.2.1.4.1.1.9")) // lldpRemSysName
            {
                var k = keyFromRemIndex(vb.Oid);
                if (k != null)
                    remSysName[k] = ToAsciiString(vb.Value);
            }
            foreach (var vb in WalkColumn(target, param, "1.0.8802.1.1.2.1.4.1.1.8")) // lldpRemPortDesc
            {
                var k = keyFromRemIndex(vb.Oid);
                if (k != null)
                    remPortDesc[k] = ToAsciiString(vb.Value);
            }
            foreach (var vb in WalkColumn(target, param, "1.0.8802.1.1.2.1.4.1.1.5")) // lldpRemChassisId
            {
                var k = keyFromRemIndex(vb.Oid);
                if (k != null)
                    remChassisId[k] = vb.Value.ToString(); // 보통 MAC(바이너리) but ToString()으로 표시
            }
            foreach (var vb in WalkColumn(target, param, "1.0.8802.1.1.2.1.4.1.1.7")) // lldpRemPortId
            {
                var k = keyFromRemIndex(vb.Oid);
                if (k != null)
                    remPortId[k] = ToAsciiString(vb.Value);
            }

            // ---- 3) 합쳐서 보기 좋게 출력 (로컬 포트명 보강)
            Console.WriteLine("== LLDP Neighbors ==");
            foreach (var k in remSysName.Keys)
            {
                // 키에서 로컬 포트 번호 꺼내 로컬 포트명 매핑
                var parts = k.Split('.');
                int lpNum = (parts.Length == 3 && int.TryParse(parts[1], out var tmp)) ? tmp : -1;

                localPortDesc.TryGetValue(lpNum, out var lpDesc);
                remSysName.TryGetValue(k, out var sysname);
                remPortDesc.TryGetValue(k, out var rPortDesc);
                remPortId.TryGetValue(k, out var rPortId);
                remChassisId.TryGetValue(k, out var chassis);

                Console.WriteLine($"LocalPortNum={lpNum} ({lpDesc})  <--->  {sysname}");
                Console.WriteLine($"  RemotePortDesc: {rPortDesc}");
                Console.WriteLine($"  RemotePortId  : {rPortId}");
                Console.WriteLine($"  ChassisId     : {chassis}");
                Console.WriteLine();
            }
        }
    }

}