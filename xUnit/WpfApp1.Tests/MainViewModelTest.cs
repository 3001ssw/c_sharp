namespace WpfApp1.Tests
{
    public class MainViewModelTest
    {
        [Fact]
        public void CanExecuteTest()
        {
            var vm = new MainViewModel();

            vm.InputText1 = "10";
            vm.InputText2 = "20";

            Assert.True(vm.SumCommand.CanExecute());
        }

        [Theory]
        [InlineData("", "20")]
        [InlineData("10", "abc")]
        public void Command_ShouldBeDisabled_WhenInputsAreInvalid(string in1, string in2)
        {
            var vm = new MainViewModel();
            vm.InputText1 = in1;
            vm.InputText2 = in2;

            Assert.False(vm.SumCommand.CanExecute());
        }
    }
}