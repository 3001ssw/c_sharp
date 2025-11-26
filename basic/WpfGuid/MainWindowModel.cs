using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using WpfGuid.Models;

namespace WpfGuid
{
    public partial class MainWindowModel : BindableBase
    {
        public ObservableCollection<SchoolModel> SchoolModels { get; set; } = new ObservableCollection<SchoolModel>();

        public MainWindowModel()
        {
        }

        public void SaveData()
        {
            // Serialize
            string json = JsonSerializer.Serialize(SchoolModels);

            // 현재 실행 경로 가져오기
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.Combine(baseDir, "Data");

            // 폴더가 없으면 생성
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, "school.json");

            // 파일 저장
            File.WriteAllText(filePath, json);
        }

        public void LoadData()
        {
            // 현재 실행 경로 가져오기
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.Combine(baseDir, "Data");

            // 폴더가 없으면 Load 중지
            if (!Directory.Exists(folderPath))
                return;
            string filePath = Path.Combine(folderPath, "school.json");
            if (!Path.Exists(filePath))
                return;

            string json = File.ReadAllText(filePath);

            // Deserialize
            SchoolModels = JsonSerializer.Deserialize<ObservableCollection<SchoolModel>>(json);

        }
    }
}
