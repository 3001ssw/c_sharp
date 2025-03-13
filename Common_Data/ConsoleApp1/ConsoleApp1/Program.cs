using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly string baseUrl = "https://openapi.gg.go.kr/LINEBUSANULUSERCNT";
        private static readonly string apiKey = "key";

        static async Task Main()
        {
            await FetchGyeonggiData();
        }

        static async Task FetchGyeonggiData()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // URL을 동적으로 생성 (직접 문자열을 만들지 않음)
                    var builder = new UriBuilder(baseUrl);
                    var query = HttpUtility.ParseQueryString(string.Empty);
                    query["key"] = apiKey;
                    query["Type"] = "json";
                    query["pIndex"] = "1";
                    query["pSize"] = "1000";

                    query["YY"] = "2024";
                    //query["YY"] = "2025";
                    //query["MT"] = "1";
                    //query["MT"] = "2";
                    query["TM_CD"] = "07"; // 07시~08시
                    //query["TM_CD"] = "08"; // 08시~09시
                    query["REGION_CD"] = "3124000"; // 경기도 화성시
                    query["BUS_ROUTE_DIV"] = "330";
                    query["WKEND_WKDAY_DIV_CD"] = "01"; // 평일
                    builder.Query = query.ToString();

                    // HTTP GET 요청 보내기
                    HttpResponseMessage response = await client.GetAsync(builder.ToString());
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseBody);

                    Console.WriteLine("=== API 응답 데이터 ===");
                    Console.WriteLine(json.ToString());

                    // 특정 데이터 추출
                    //var items = json["PlaceThatDoAT"][1]["row"];
                    //foreach (var item in items)
                    //{
                    //    Console.WriteLine($"시설명: {item["BIZPLC_NM"]}, 주소: {item["REFINE_LOTNO_ADDR"]}");
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"오류 발생: {ex.Message}");
                }
            }
        }
    }
}
