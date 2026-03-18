using Microsoft.ML;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClustering
{
    public class MainViewModel : BindableBase
    {
        public DelegateCommand ClusteringCommand { get; private set; }
        private void OnClustering()
        {
            var mlContext = new MLContext(seed: 0);

            // 3. 100개의 예시 데이터 생성 (C#, WPF, ASP.NET 혼합)
            var questions = GenerateSampleQuestions();
            var dataView = mlContext.Data.LoadFromEnumerable(questions);

            // 4. 학습 파이프라인 구성
            // 텍스트를 숫자로 변환(FeaturizeText) -> K-Means 알고리즘 적용
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(QuestionData.Text))
                .Append(mlContext.Clustering.Trainers.KMeans(featureColumnName: "Features", numberOfClusters: 5));

            // 5. 모델 학습 (군집 찾기)
            var model = pipeline.Fit(dataView);

            // 6. 결과 확인용 데이터 생성
            var predictor = mlContext.Model.CreatePredictionEngine<QuestionData, ClusterPrediction>(model);

            Debug.WriteLine("=== 질문 클러스터링 결과 (3개 그룹) ===");

            var clusteredResults = questions.Select(q => new
            {
                Question = q.Text,
                ClusterId = predictor.Predict(q).SelectedClusterId
            }).GroupBy(x => x.ClusterId).OrderBy(x => x.Key);

            foreach (var group in clusteredResults)
            {
                Debug.WriteLine($"[그룹 {group.Key}]");
                foreach (var item in group.Take(5)) // 각 그룹당 5개만 출력
                {
                    Debug.WriteLine($" - {item.Question}");
                }
                Debug.WriteLine($" ...외 {group.Count() - 5}개 질문");
            }
        }

        private bool CanClustering()
        {
            return true;
        }

        #region constructor
        public MainViewModel()
        {
            ClusteringCommand = new DelegateCommand(OnClustering, CanClustering);

        }
        #endregion
        private static List<QuestionData> GenerateSampleQuestions()
        {
            return new List<QuestionData>
    {
        // [IT / 과학 / 테크] - 키워드: AI, 반도체, 우주, 스마트폰, 플랫폼
        new QuestionData { Text = "엔비디아, 시가총액 1위 탈환... AI 반도체 독주 체제 굳히나" },
        new QuestionData { Text = "오픈AI '소라' 공개, 영상 제작 업계 '충격'... 일자리 위협 현실로?" },
        new QuestionData { Text = "삼성전자, 2나노 공정 양산 계획 발표... 파운드리 역전 노린다" },
        new QuestionData { Text = "스페이스X, 화성 탐사선 '스타십' 4차 시험 발사 성공" },
        new QuestionData { Text = "애플, WWDC에서 '애플 인텔리전스' 발표... 아이폰의 진화" },
        new QuestionData { Text = "네이버-소프트뱅크 '라인야후' 사태 진정국면... 지분 매각은?" },
        new QuestionData { Text = "구글 '제미나이' 업데이트, 한국어 지원 대폭 강화" },
        new QuestionData { Text = "양자 컴퓨터 상용화 성큼... 국내 연구진 핵심 소자 개발" },
        new QuestionData { Text = "카카오, AI 서비스 '카나나' 공개... 대화형 AI 시장 도전" },
        new QuestionData { Text = "메타, 저가형 VR 기기 '퀘스트 3S' 출시... 대중화 이끄나" },
        new QuestionData { Text = "누리호 4차 발사 준비 착착... 민간 주도 우주 개발 가속도" },
        new QuestionData { Text = "틱톡, 미국 내 금지법 위헌 소송 제기... 법적 공방 장기화" },
        new QuestionData { Text = "국내 연구진, 꿈의 배터리 '전고체 전지' 효율 2배 높였다" },
        new QuestionData { Text = "테슬라, 완전자율주행(FSD) 구독 서비스 한국 상륙 임박" },
        new QuestionData { Text = "유튜브 프리미엄 가격 인상... '디지털 스트레스' 호소하는 이용자들" },
        new QuestionData { Text = "인텔, '루나 레이크' CPU 공개... AI PC 시장 선점 노린다" },
        new QuestionData { Text = "다이슨, 공기청정 헤드폰 출시... 웨어러블 가전의 진화" },
        new QuestionData { Text = "클라우드 보안 사고 비상... '제로 트러스트' 보안 체계 주목" },
        new QuestionData { Text = "당근마켓, 지역 기반 구인구직 서비스 대폭 강화" },
        new QuestionData { Text = "닌텐도 스위치 2, 내년 초 출시 확정... 게임 시장 판도 바뀔까" },
        new QuestionData { Text = "넷플릭스, 게임 사업 확장... '오징어 게임' IP 활용작 공개" },
        new QuestionData { Text = "LG전자, 스마트 가전 전용 AI 칩 'DQ-C' 자체 개발" },
        new QuestionData { Text = "국내 통신 3사, 6G 표준화 주도권 확보 위해 뭉쳤다" },
        new QuestionData { Text = "바이오 테크의 역습... 유전자 가위 기술로 암 정복 도전" },
        new QuestionData { Text = "비트코인 ETF 승인 이후 자금 유입 가속화... 10만 달러 가나" },

        // [경제 / 금융 / 부동산] - 키워드: 금리, 물가, 환율, 증시, 아파트
        new QuestionData { Text = "한국은행, 기준금리 13연속 동결... 가계부채 관리에 무게" },
        new QuestionData { Text = "미 연준(Fed), 9월 금리 인하 시그널... 시장은 이미 선반영" },
        new QuestionData { Text = "코스피, '삼천피' 탈환 실패... 외인 순매도에 2,700선 턱걸이" },
        new QuestionData { Text = "소비자물가 상승률 2%대 진입... 하지만 장바구니 체감은 여전히 '고물가'" },
        new QuestionData { Text = "서울 아파트값 15주 연속 상승... 강남·마용성 위주 신고가 속출" },
        new QuestionData { Text = "원·달러 환율 1,380원대 고착화... 수출 기업 '비상'" },
        new QuestionData { Text = "신생아 특례대출 흥행... 주택 담보대출 잔액 사상 최대 기록" },
        new QuestionData { Text = "상속세 개편안 발표... 자녀 공제액 5천만 원에서 5억 원으로 상향" },
        new QuestionData { Text = "대형 마트 공휴일 의무휴업 폐지 확산... 전통시장과의 상생은?" },
        new QuestionData { Text = "배달 플랫폼 수수료 논쟁 격화... 자영업자 '수수료 상한제' 요구" },
        new QuestionData { Text = "공모주 청약 열풍... 따따블 대박 꿈꾸는 개미들" },
        new QuestionData { Text = "전세 사기 피해자 지원 특별법 국회 통과... 실질적 구제책 될까" },
        new QuestionData { Text = "국내 상장사 2분기 실적 발표... 반도체 호실적에 웃고 고물가에 울고" },
        new QuestionData { Text = "전기차 캐즘(일시적 수요 정체)에 배터리 업계 감산 돌입" },
        new QuestionData { Text = "종합부동산세 폐지 논란... 세수 부족 우려와 조세 형평성 사이" },
        new QuestionData { Text = "청년도약계좌 가입자 100만 명 돌파... 목돈 마련 열기 뜨겁다" },
        new QuestionData { Text = "금값 사상 최고치 경신... '안전 자산' 선호 심리 강화" },
        new QuestionData { Text = "PF 대출 부실 경고등... 건설업계 연쇄 도산 공포 확산" },
        new QuestionData { Text = "일본 엔저 현상 지속... '엔테크' 열풍과 여행객 폭증" },
        new QuestionData { Text = "국민연금 개혁안 지연... '더 내고 더 받기' 합의점 찾기 난항" },
        new QuestionData { Text = "K-푸드 수출 역대 최대... 라면·김치 전 세계 입맛 잡았다" },
        new QuestionData { Text = "가상자산 이용자 보호법 시행... 거래소 규제 대폭 강화" },
        new QuestionData { Text = "수도권 광역급행철도(GTX)-A 개통... 인근 부동산 가격 들썩" },
        new QuestionData { Text = "은행권 대출 금리 인상... '영끌족' 이자 부담에 밤잠 설친다" },
        new QuestionData { Text = "스타벅스, 커피 가격 일부 인상... 고물가에 프랜차이즈 도미노 인상" },

        // [정치 / 사회 / 교육] - 키워드: 국회, 정부, 법안, 복지, 날씨
        new QuestionData { Text = "여야, 민생법안 처리 합의... '구하라법'·'간호법' 본회의 통과" },
        new QuestionData { Text = "정부, 의대 정원 확대 추진... 의료계 반발 속 대화 국면 진입" },
        new QuestionData { Text = "대통령실, 저출생대응수석비서관실 신설... 인구 위기 총력전" },
        new QuestionData { Text = "서울시, 대중교통 무제한 '기후동행카드' 공식 서비스 시작" },
        new QuestionData { Text = "수능 난이도 조절 비상... '킬러 문항' 배제하고 변별력 확보할까" },
        new QuestionData { Text = "전국 낮 기온 35도 웃도는 폭염... 온열 질환 예방 주의보" },
        new QuestionData { Text = "딥페이크 성범죄와의 전쟁... 정부, 처벌 수위 대폭 강화" },
        new QuestionData { Text = "경찰, 마약 범죄 집중 단속... 일상 속 파고든 마약류 차단 총력" },
        new QuestionData { Text = "초저출생 쇼크... 초등학교 입학 아동 수 사상 첫 30만 명대 붕괴" },
        new QuestionData { Text = "국회의원 특권 폐지 논란... 불체포 특권 포기 서약 잇따라" },
        new QuestionData { Text = "고령화 사회 진입에 따른 '노인 연령 상향' 논의 본격화" },
        new QuestionData { Text = "제주 산간 집중호우... 기상청 '강한 비바람 주의' 당부" },
        new QuestionData { Text = "교권 보호 4법 시행... '정당한 교육활동' 보호 장치 마련" },
        new QuestionData { Text = "무료 급식소 후원 끊겨 '위기'... 고물가 속 취약계층 고통 가중" },
        new QuestionData { Text = "디지털 교과서 도입 논란... '학습 효과' vs '스마트폰 중독' 우려" },
        new QuestionData { Text = "강남 학원가 '마약 음료' 사건 일당 실형 확정" },
        new QuestionData { Text = "지하철 무임승차 손실 보전... 지자체와 정부 갈등 평행선" },
        new QuestionData { Text = "청년 일자리 예산 확대... 민간 기업 채용 연계 강화" },
        new QuestionData { Text = "환경부, 일회용 컵 보증금제 전국 확대 무기한 유예" },
        new QuestionData { Text = "전세 사기 특별법 개정안... 피해자 선구제 후회수 핵심" },
        new QuestionData { Text = "군 장병 봉급 인상... 병장 기준 150만 원 시대 열려" },
        new QuestionData { Text = "층간소음 기준 강화... 시공 후 성능 확인 의무화" },
        new QuestionData { Text = "국립중앙박물관 '이건희 컬렉션' 특별전 연일 문전성시" },
        new QuestionData { Text = "가정폭력 피해자 보호 시설 확충... 지원 체계 촘촘하게" },
        new QuestionData { Text = "도로 위 암살자 '블랙아이스' 사고... 겨울철 안전운전 수칙" },

        // [스포츠 / 연예 / 문화] - 키워드: 손흥민, BTS, 금메달, 우승, 개봉
        new QuestionData { Text = "손흥민, 토트넘 통산 400경기 출전 금자탑... 살아있는 전설" },
        new QuestionData { Text = "방탄소년단(BTS) 맏형 진 전역... 1,000명 팬과 허그회 개최" },
        new QuestionData { Text = "영화 '파묘' 천만 관객 돌파... K-오컬트의 새로운 역사" },
        new QuestionData { Text = "파리 올림픽 개막... 대한민국 선수단 금메달 목표 순항" },
        new QuestionData { Text = "황희찬, 프리미어리그 두 자릿수 득점 달성... '코리안 가이'의 위력" },
        new QuestionData { Text = "뉴진스, 빌보드 차트 장기 집권... 전 세계가 'Ditto' 열풍" },
        new QuestionData { Text = "기아 타이거즈, 프로야구 정규 시즌 우승... V12 향해 진격" },
        new QuestionData { Text = "이정후, 메이저리그 첫 홈런 작렬... 샌프란시스코의 신성" },
        new QuestionData { Text = "블랙핑크 리사, 솔로 신곡 'ROCKSTAR' 전 세계 동시 공개" },
        new QuestionData { Text = "류현진, KBO 복귀 후 첫 완봉승... '코리안 몬스터'의 귀환" },
        new QuestionData { Text = "넷플릭스 '오징어 게임' 시즌2 예고편 공개... 전 세계 팬들 열광" },
        new QuestionData { Text = "안세영, 배드민턴 세계 랭킹 1위 수성... 올림픽 금메달 기대감" },
        new QuestionData { Text = "에스파, 'Supernova' 롱런 인기... '쇠맛' 감성 통했다" },
        new QuestionData { Text = "봉준호 차기작 '미키 17', 로버트 패틴슨 주연 예고편 공개" },
        new QuestionData { Text = "김하성, 샌디에이고의 '어썸 킴'... 수비 부문 골드글러브 후보" },
        new QuestionData { Text = "임영웅, 서울월드컵경기장 콘서트 성료... 10만 영웅시대 결집" },
        new QuestionData { Text = "K-콘텐츠 수출액 가전·이차전지 넘었다... 효자 산업 등극" },
        new QuestionData { Text = "배우 이정재, 에미상 남우주연상 수상 후 '스타워즈' 시리즈 합류" },
        new QuestionData { Text = "대한민국 축구 대표팀, 아시안컵 우승 도전... 중동 돌풍 잠재울까" },
        new QuestionData { Text = "아이브(IVE), 일본 도쿄돔 공연 전석 매진... 글로벌 대세 입증" },
        new QuestionData { Text = "한국 영화 '범죄도시4', 마석도의 핵펀치 다시 통했다... 박스오피스 1위" },
        new QuestionData { Text = "페이커(Faker) 이상혁, 전설의 전당 헌액... e스포츠 역사가 되다" },
        new QuestionData { Text = "뮤지컬 '레미제라블' 한국 공연 10주년 기념 공연 화제" },
        new QuestionData { Text = "박찬욱 감독 신작 '도끼' 제작 확정... 초호화 캐스팅 눈길" },
        new QuestionData { Text = "조성진 피아노 리사이틀, 티켓 오픈과 동시에 전석 매진" }
    };
        }

        private string GetRandomElement(params string[] items) => items[new Random().Next(items.Length)];
    }
}
