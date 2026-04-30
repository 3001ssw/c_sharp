using Microsoft.SemanticKernel;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // 커널 생성 및 AI 서비스 구성
            var kernel = Kernel.Builder
                .WithAzureChatCompletionService(
                    "gpt-4",
                    "YOUR_AZURE_ENDPOINT",
                    "YOUR_AZURE_API_KEY")
                .Build();

            // 시맨틱 스킬 생성
            var summarizeFunction = kernel.CreateSemanticFunction(
                "{{$input}}\n\n위 텍스트를 한 문장으로 요약해주세요.",
                maxTokens: 100);

            // 스킬 실행
            string text = "긴 문서 내용...";
            var result = await summarizeFunction.InvokeAsync(text);

            Console.WriteLine(result);
        }
    }
}
