using OllamaSharp;
using OllamaSharp.Models; // 추가적인 모델 클래스 사용을 위해 필요할 수 있음

// 1. 클라이언트 설정
var uri = new Uri("http://localhost:11434");
var ollama = new OllamaApiClient(uri);

// 2. 모델 설정 (설치된 모델명으로 변경)
// 2. 모델 설정 부분을 이렇게 바꿔보세요
ollama.SelectedModel = "llama3:latest";

Console.WriteLine($"--- Ollama 연결 완료: {ollama.SelectedModel} ---");

while (true)
{
    Console.Write("\n나: ");
    string userPrompt = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(userPrompt)) continue;

    Console.Write("Ollama: ");

    try
    {
        // 수정된 부분: AnswerAsync -> GenerateAsync (또는 ChatAsync)
        // 스트리밍 출력을 위해 순회하며 Response 속성을 출력합니다.
        await foreach (var stream in ollama.GenerateAsync(userPrompt))
        {
            Console.Write(stream.Response);
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[에러] {ex.Message}");
    }
}