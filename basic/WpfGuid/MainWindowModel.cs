using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using WpfGuid.Models;

namespace WpfGuid
{
    public partial class MainWindowModel : BindableBase
    {
        public ObservableCollection<SchoolModel> SchoolModels { get; set; } = new ObservableCollection<SchoolModel>();
        public ObservableCollection<TeacherModel> TeacherModels { get; set; } = new ObservableCollection<TeacherModel>();

        public MainWindowModel()
        {
        }

        public void SaveData()
        {
            // 현재 실행 경로 가져오기
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.Combine(baseDir, "Data");

            // 폴더가 없으면 생성
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string schoolFilePath = Path.Combine(folderPath, "school.json");
            string teacherFilePath = Path.Combine(folderPath, "teacher.json");

            // Serialize
            string jsonSchool = JsonSerializer.Serialize(SchoolModels);
            File.WriteAllText(schoolFilePath, jsonSchool); // 파일 저장

            string jsonTeacher = JsonSerializer.Serialize(TeacherModels);
            File.WriteAllText(teacherFilePath, jsonSchool); // 파일 저장
        }

        public void LoadData()
        {
            // 현재 실행 경로 가져오기
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.Combine(baseDir, "Data");

            // 폴더가 없으면 Load 중지
            if (!Directory.Exists(folderPath))
                return;
            string schoolFilePath = Path.Combine(folderPath, "school.json");
            if (Path.Exists(schoolFilePath))
            {
                string json = File.ReadAllText(schoolFilePath);
                SchoolModels = JsonSerializer.Deserialize<ObservableCollection<SchoolModel>>(json);

            }

            string teacherFilePath = Path.Combine(folderPath, "teacher.json");
            if (Path.Exists(teacherFilePath))
            {
                string json= File.ReadAllText(teacherFilePath);
                TeacherModels = JsonSerializer.Deserialize<ObservableCollection<TeacherModel>>(json);

            }
        }
    }
}
