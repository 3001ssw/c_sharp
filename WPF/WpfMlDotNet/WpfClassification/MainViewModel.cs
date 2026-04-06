using Microsoft.ML;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClassification.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WpfClassification
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        PredictionEngine<Book, PredictBook>? predictionEngine = null;

        private ObservableCollection<Book> books = new ObservableCollection<Book>();
        public ObservableCollection<Book> Books { get => books; set => SetProperty(ref books, value); }

        private string synopsis = "";
        public string Synopsis { get => synopsis; set => SetProperty(ref synopsis, value); }

        private string predictedGenre = "";
        public string PredictedGenre { get => predictedGenre; set => SetProperty(ref predictedGenre, value); }

        #endregion

        #region commands

        public DelegateCommand LoadModelFileCommand { get; private set; }
        public DelegateCommand SaveModelFileCommand { get; private set; }
        public DelegateCommand ClassificationCommand { get; private set; }

        private void OnLoadModelFile()
        {
            MLContext mlContext = new MLContext();

            ITransformer model = mlContext.Model.Load("BookModel.zip", out DataViewSchema modelInputSchema);

            predictionEngine = mlContext.Model.CreatePredictionEngine<Book, PredictBook>(model);
        }

        private bool CanLoadModelFile()
        {
            return true;
        }

        private async void OnSaveModelFile()
        {
            await Task.Run(() =>
            {
                MLContext mlContext = new MLContext();

                IDataView dataView = mlContext.Data.LoadFromEnumerable(Books);

                // 정답(장르)을 컴퓨터가 아는 숫자로 변환
                // 텍스트(줄거리)를 수학적 특징으로 변환
                // 분류 알고리즘(엔진) 장착
                // 다시 숫자를 글자(장르)로 변환

                var pipeline = mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Genre", outputColumnName: "Label")
                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Synopsis", outputColumnName: "Features"))
                    .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(inputColumnName: "PredictedLabel", outputColumnName: "PredictedGenre"));

                //var pipeline = mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Genre", outputColumnName: "Label")
                //  .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Synopsis", outputColumnName: "Features"))
                //  .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                //  .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

                //var pipeline = mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Genre", outputColumnName: "Label") // 정답(장르)을 컴퓨터가 아는 숫자로 변환
                //    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Synopsis", outputColumnName: "Features")) // 텍스트(줄거리)를 수학적 특징으로 변환
                //    .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features")) // 분류 알고리즘(엔진) 장착
                //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedGenre")) // 다시 숫자를 글자(장르)로 변환
                //    .Append(mlContext.Transforms.Conversion.MapKeyToValue(inputColumnName: "PredictedLabel", outputColumnName: "PredictedGenre"));

                ITransformer model = pipeline.Fit(dataView);

                mlContext.Model.Save(model, dataView.Schema, "BookModel.zip");

                predictionEngine = mlContext.Model.CreatePredictionEngine<Book, PredictBook>(model);
            });
        }

        private bool CanSaveModelFile()
        {
            return true;
        }

        private void OnClassification()
        {
            if (predictionEngine == null)
                return;

            Book book = new Book { Synopsis = Synopsis };
            PredictBook predicGenre = predictionEngine.Predict(book);
            PredictedGenre = predicGenre.PredictedGenre;
        }

        private bool CanClassification()
        {
            return true;
        }

        #endregion


        public MainViewModel()
        {
            LoadModelFileCommand = new DelegateCommand(OnLoadModelFile, CanLoadModelFile);
            SaveModelFileCommand = new DelegateCommand(OnSaveModelFile, CanSaveModelFile);
            ClassificationCommand = new DelegateCommand(OnClassification, CanClassification);

            MakeBookInfo();
        }
        private void MakeBookInfo()
        {
            Books.Clear();

            // --- IT/프로그래밍 (15권) ---
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "애자일 소프트웨어 장인 정신과 읽기 좋은 코드를 작성하는 원칙과 실전 가이드" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "코드의 구조를 개선하고 유지보수성을 높이는 기존 코드 개선의 기술" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "객체지향 소프트웨어 설계에서 재사용 가능한 23가지 핵심 디자인 패턴" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "C# 기초 문법부터 최신 닷넷 플랫폼까지 다루는 입문 및 활용서" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "C++의 메모리 구조부터 객체지향 프로그래밍의 핵심까지 다루는 실전서" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "데이터 바인딩과 디자인 패턴을 활용한 윈도우 데스크톱 애플리케이션 개발" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "유니티 엔진을 활용하여 스프라이트, 애니메이션, 물리 엔진을 다루는 2D 게임 제작" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "네트워크 통신의 기본 원리와 C/C++ 기반의 서버 클라이언트 통신 구현" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "람다, 스트림, 함수형 프로그래밍 등 최신 자바의 핵심 기능 마스터" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "사이킷런을 활용한 데이터 분석과 머신러닝 알고리즘 실전 구현" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "소프트웨어의 복잡성을 다루기 위한 도메인 중심의 아키텍처 설계" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "인지과학을 통해 알아보는 코드를 읽고 이해하는 프로그래머의 사고방식" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "효율적이고 책임감 있는 소프트웨어 개발자가 되기 위한 철학과 접근법" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "버전 관리 시스템 깃(Git)의 원리와 깃허브(GitHub) 협업 실무 가이드" });
            Books.Add(new Book { Genre = "IT/프로그래밍", Synopsis = "컨테이너 가상화 기술과 오케스트레이션을 활용한 인프라 관리" });

            // --- 판타지 (15권) ---
            Books.Add(new Book { Genre = "판타지", Synopsis = "이마에 번개 흉터가 있는 소년이 호그와트 마법 학교에 입학하여 겪는 첫 모험" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "세상을 지배할 절대반지를 파괴하기 위해 구성된 아홉 명의 원정대 이야기" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "신비한 옷장을 통해 얼어붙은 세계 나니아로 들어간 네 남매의 모험" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "일곱 왕국의 철왕좌를 차지하기 위한 귀족 가문들의 암투와 전쟁" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "평온을 사랑하는 호빗 빌보 배긴스가 드워프들과 함께 떠나는 스마우그의 보물 찾기" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "괴물 사냥꾼 게롤트와 멸망한 왕국의 공주 시리가 엮어가는 운명의 대서사시" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "오만한 소년 마법사 새매가 자신이 부른 그림자 괴물과 맞서 싸우며 성장하는 이야기" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "전설적인 마법사 콰오스가 자신의 과거와 진실을 회고하며 들려주는 모험담" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "자신이 포세이돈의 아들임을 알게 된 소년이 도둑맞은 제우스의 번개를 찾는 여정" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "진실을 알려주는 황금나침반을 지닌 소녀 라라가 북극으로 떠나는 신비한 여정" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "네 개의 종족이 살아가는 세계에서 왕을 구출하기 위해 뭉친 수색대의 이야기" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "가문의 몰락 후 가보인 겨울검을 쥐고 살아남기 위해 떠도는 소년 보리스의 생존기" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "드래곤과 인간을 이어주는 존재와 함께 국왕의 몸값을 구하러 떠나는 일행의 모험" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "마법 세계의 신비한 동물들을 연구하고 보호하는 마법학자의 뉴욕 방문기" });
            Books.Add(new Book { Genre = "판타지", Synopsis = "가상현실 게임 로열 로드에서 돈을 벌기 위해 조각사라는 직업을 선택한 소년의 이야기" });

            // --- SF (10권) ---
            Books.Add(new Book { Genre = "SF", Synopsis = "우주에서 가장 비싼 물질 스파이스가 생산되는 사막 행성 아라키스를 둘러싼 권력 투쟁" });
            Books.Add(new Book { Genre = "SF", Synopsis = "화성 탐사 중 고립된 우주비행사가 과학적 지식을 총동원하여 생존해 나가는 이야기" });
            Books.Add(new Book { Genre = "SF", Synopsis = "은하 제국의 멸망을 예측한 수학자가 인류의 지식을 보존하기 위해 세운 재단의 기록" });
            Books.Add(new Book { Genre = "SF", Synopsis = "외계 종족의 침공에 대비해 우주 함대 사령관으로 길러지는 천재 소년의 훈련과 전투" });
            Books.Add(new Book { Genre = "SF", Synopsis = "기억을 잃은 과학자가 멸망 위기의 지구를 구하기 위해 우주선에서 외계인과 협력하는 이야기" });
            Books.Add(new Book { Genre = "SF", Synopsis = "핵전쟁 이후의 지구에서 도망친 인조인간들을 사냥하는 현상금 사냥꾼의 고뇌" });
            Books.Add(new Book { Genre = "SF", Synopsis = "세 개의 태양을 가진 외계 문명과 접촉하게 된 인류의 위기와 거대한 우주적 스케일의 서사" });
            Books.Add(new Book { Genre = "SF", Synopsis = "인간이 공장에서 부화하고 감정이 통제되는 완벽해 보이지만 끔찍한 미래 사회" });
            Books.Add(new Book { Genre = "SF", Synopsis = "압도적인 외계 지성체가 지구에 강림하여 평화를 가져오지만 인류의 진화가 촉발되는 이야기" });
            Books.Add(new Book { Genre = "SF", Synopsis = "우주와 미래를 배경으로 인간의 소외와 연결, 그리움에 대해 다룬 단편 소설집" });

            // --- 과학 (15권) ---
            Books.Add(new Book { Genre = "과학", Synopsis = "우주의 기원과 진화, 인류의 역사와 과학의 발전을 탐구하는 천문학의 고전" });
            Books.Add(new Book { Genre = "과학", Synopsis = "생물학적 진화의 주체를 개체가 아닌 유전자의 관점에서 설명하는 진화생물학 서적" });
            Books.Add(new Book { Genre = "과학", Synopsis = "인지혁명부터 과학혁명까지, 호모 사피엔스가 어떻게 지구의 지배자가 되었는지에 대한 탐구" });
            Books.Add(new Book { Genre = "과학", Synopsis = "우주의 기원, 블랙홀, 시간 여행 등 현대 물리학의 핵심 주제를 대중적으로 풀어낸 책" });
            Books.Add(new Book { Genre = "과학", Synopsis = "빛, 시공간, 원자 등 물리학의 기본 개념을 통해 인간과 세계의 관계를 통찰하는 책" });
            Books.Add(new Book { Genre = "과학", Synopsis = "열역학 제2법칙을 통해 현대 물질문명의 한계와 환경 문제를 경고하는 철학적 과학서" });
            Books.Add(new Book { Genre = "과학", Synopsis = "양자역학의 창시자가 원자물리학의 발전 과정과 과학자들의 철학적 대화를 기록한 책" });
            Books.Add(new Book { Genre = "과학", Synopsis = "무분별한 살충제 사용이 생태계에 미치는 파괴적인 영향을 고발한 환경 과학의 명저" });
            Books.Add(new Book { Genre = "과학", Synopsis = "우주의 모든 힘을 하나로 설명하려는 끈 이론과 숨겨진 차원에 대한 물리학 교양서" });
            Books.Add(new Book { Genre = "과학", Synopsis = "다양한 뇌 신경 질환을 앓는 환자들의 임상 사례를 통해 인간 뇌의 신비를 탐구한 책" });
            Books.Add(new Book { Genre = "과학", Synopsis = "보이저 1호가 찍은 지구 사진을 통해 우주 속 인류의 위치와 태양계 탐사의 미래를 논한 책" });
            Books.Add(new Book { Genre = "과학", Synopsis = "시각실인증 등 기이한 뇌 손상을 겪는 환자들의 따뜻하고 감동적인 치유 기록" });
            Books.Add(new Book { Genre = "과학", Synopsis = "수학이 절대적인 진리라는 믿음이 어떻게 붕괴되었는지 수학사를 통해 설명하는 책" });
            Books.Add(new Book { Genre = "과학", Synopsis = "양자역학적 관점에서 생명 현상과 유전의 본질을 설명하여 분자생물학에 영향을 준 책" });
            Books.Add(new Book { Genre = "과학", Synopsis = "분류학자의 삶을 추적하며 과학적 질서와 삶의 의미, 그리고 상실에 대해 쓴 경이로운 에세이" });

            // --- 소설/문학 (15권) ---
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "모든 일상이 빅 브라더에게 감시당하는 전체주의 디스토피아 사회의 억압과 저항" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "1920년대 미국 재즈 시대를 배경으로 한 부와 사랑, 그리고 아메리칸드림의 타락" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "1930년대 미국 남부의 인종 차별과 편견에 맞서 백인 변호사가 흑인을 변호하는 이야기" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "망망대해에서 거대한 청새치와 사투를 벌이는 늙은 어부의 불굴의 의지와 인간의 존엄" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "19세기 영국을 배경으로 남녀가 서로의 첫인상과 오해를 극복하고 사랑에 빠지는 과정" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "주인공 싱클레어가 데미안을 만나 선과 악의 이분법을 넘어 자아를 찾아가는 성장 소설" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "학교에서 퇴학당한 소년 콜필드가 허위와 위선으로 가득 찬 어른들의 세계를 방황하는 이야기" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "어머니의 장례식 후 살인을 저지르고도 무관심한 주인공을 통해 세상의 부조리를 그린 소설" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "어느 날 아침 거대한 벌레로 변해버린 샐러리맨을 통해 현대인의 소외와 단절을 표현한 작품" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "1980년 광주 민주화 운동 당시 희생된 소년과 살아남은 자들의 아픔을 다룬 문학" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "인간을 내쫓고 농장을 차지한 동물들이 부패한 권력으로 타락해가는 과정을 그린 풍자 소설" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "소행성 B612호에서 온 어린 왕자가 여행을 통해 진정한 관계와 사랑의 의미를 깨닫는 동화" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "잠들어야만 입장할 수 있는 상점가에서 다양한 꿈을 사고파는 사람들의 힐링 판타지" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "뇌의 편도체가 작아 감정을 느끼지 못하는 소년이 타인과 교감하며 성장하는 이야기" });
            Books.Add(new Book { Genre = "소설/문학", Synopsis = "비어있는 오래된 잡화점에 숨어든 도둑들이 과거로부터 온 고민 상담 편지에 답장을 보내는 이야기" });

            // --- 역사 (15권) ---
            Books.Add(new Book { Genre = "역사", Synopsis = "무기, 병균, 금속이 어떻게 대륙별 문명의 발달 속도와 인류의 불평등을 초래했는지 분석" });
            Books.Add(new Book { Genre = "역사", Synopsis = "작은 도시국가 로마가 지중해를 제패하고 대제국을 건설했다가 멸망하기까지의 거대한 통사" });
            Books.Add(new Book { Genre = "역사", Synopsis = "태조부터 순종까지 조선 27대 왕들의 핵심적인 역사적 사건을 알기 쉽게 풀어낸 책" });
            Books.Add(new Book { Genre = "역사", Synopsis = "혼란스러운 이탈리아를 통일하기 위해 강력한 군주가 가져야 할 현실적인 권력 유지의 기술" });
            Books.Add(new Book { Genre = "역사", Synopsis = "고대 아테네와 스파르타 간의 27년 전쟁을 철저한 사실주의에 입각하여 기록한 역사서" });
            Books.Add(new Book { Genre = "역사", Synopsis = "중국 춘추전국시대부터 한나라까지, 역사에 족적을 남긴 영웅과 지식인들의 파란만장한 인물사" });
            Books.Add(new Book { Genre = "역사", Synopsis = "대한민국 임시정부를 이끈 독립운동가 백범 김구 선생의 파란만장한 생애와 민족 독립의 염원" });
            Books.Add(new Book { Genre = "역사", Synopsis = "제2차 세계대전 당시 미국 인류학자가 일본인의 모순된 행동 양식과 문화를 해부한 책" });
            Books.Add(new Book { Genre = "역사", Synopsis = "임진왜란 7년 동안 충무공 이순신 장군이 전장에서 직접 기록한 진중 일기와 고뇌" });
            Books.Add(new Book { Genre = "역사", Synopsis = "인도의 독립운동가가 옥중에서 딸에게 세계사의 중요한 흐름을 편지 형식으로 들려준 역사서" });
            Books.Add(new Book { Genre = "역사", Synopsis = "19세기부터 현재까지 문학, 음악, 사상 등 유럽 대륙 문화의 방대한 흐름을 통찰한 책" });
            Books.Add(new Book { Genre = "역사", Synopsis = "선사 시대부터 현대에 이르기까지 우리 민족의 굵직한 역사적 궤적을 하나의 흐름으로 정리" });
            Books.Add(new Book { Genre = "역사", Synopsis = "중세 시대 기독교와 이슬람 세력이 성지 예루살렘을 두고 벌인 200년간의 십자군 전쟁사" });
            Books.Add(new Book { Genre = "역사", Synopsis = "서양 역사와 예술의 뿌리가 된 올림포스 신들과 영웅들의 매혹적인 신화와 전설" });
            Books.Add(new Book { Genre = "역사", Synopsis = "건국부터 현대까지 초강대국 미국의 역사를 결정지은 100가지 주요 사건 요약" });

            // --- 자기계발/경제 (15권) ---
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "목표를 이루기 위해 극적인 변화 대신 매일 1%씩 좋아지는 시스템과 습관을 만드는 방법" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "돈을 위해 일하지 말고 돈이 나를 위해 일하게 만드는 금융 지식과 투자 마인드셋" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "타인의 마음을 얻고 원만한 대인관계를 유지하기 위한 대화법과 설득의 본질" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "세계적인 성공을 거둔 인물들의 아침 루틴, 습관, 마인드 등 성공 비밀을 분석한 책" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "모든 것에 긍정적으로 대하라는 강박을 버리고 내 삶에서 진짜 중요한 것에만 집중하는 법" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "성공한 사업가가 들려주는 돈을 대하는 태도, 자본주의 생존법, 그리고 경제적 독립의 원칙" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "인간의 판단과 결정이 직관적인 시스템과 논리적인 시스템에 의해 어떻게 지배받는지 분석" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "해결되지 않는 문제를 안고 고도의 집중 상태에 도달하여 잠재력을 극대화하는 학습법" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "아침의 첫 1시간을 활용하여 명상, 독서, 운동으로 삶을 기적처럼 변화시키는 습관" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "인간의 본성과 유전자의 명령을 역행하여 자유와 경제적 성공을 쟁취하는 7단계 모델" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "천재적인 재능보다 끝까지 포기하지 않는 열정과 끈기가 성공을 결정짓는다는 심리학적 분석" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "강압적인 명령 대신 사람들의 자연스러운 행동 변화를 유도하는 부드러운 개입의 힘" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "평범한 저축과 투자라는 서행차선을 벗어나 폭발적으로 부를 창출하는 비즈니스 시스템 구축" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "보통 사람의 범주를 뛰어넘는 성공을 거둔 인물들의 1만 시간의 법칙과 사회적 배경 분석" });
            Books.Add(new Book { Genre = "자기계발/경제", Synopsis = "수많은 일을 하려는 욕심을 버리고 자신에게 가장 중요한 단 하나의 일에만 집중하는 기술" });
        }
    }
}
