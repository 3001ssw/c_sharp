namespace WpfApp1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CanExecuteTest()
        {
            // 1. Arrange
            var vm = new MainViewModel();

            // 2. Act
            vm.InputText1 = "10";
            vm.InputText2 = "20";

            // 3. Assert
            // CanExecute()가 true를 반환해야 버튼이 활성화됩니다.
            Assert.True(vm.ShowInputTextCommand.CanExecute());
        }

        [Theory]
        [InlineData("", "20")]   // 하나가 비었을 때
        [InlineData("10", "abc")] // 숫자가 아닐 때
        public void Command_ShouldBeDisabled_WhenInputsAreInvalid(string in1, string in2)
        {
            var vm = new MainViewModel();
            vm.InputText1 = in1;
            vm.InputText2 = in2;

            Assert.False(vm.ShowInputTextCommand.CanExecute());
        }
    }

}