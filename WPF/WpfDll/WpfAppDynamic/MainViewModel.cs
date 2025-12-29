using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppDynamic
{
    public class MainViewModel : BindableBase
    {
        public DelegateCommand AddCommand { get; private set; }
        delegate int AddDelegate(int a, int b);

        public MainViewModel()
        {
            AddCommand = new DelegateCommand(OnAdd);
        }

        private void OnAdd()
        {
            // 로드 및 타입 추출 (기본 과정은 동일)
            Assembly assembly = Assembly.LoadFrom("MathLibrary.dll");
            if (assembly == null)
                return;

            Type? type = assembly.GetType("MathLibrary.Calculator");
            if (type == null)
                return;

            object? instance = Activator.CreateInstance(type);
            if (instance == null)
                return;

            MethodInfo? method = type.GetMethod("Add");
            if (method == null)
                return;

            // 2. MethodInfo를 대리자로 변환 (이 부분이 핵심!)
            // CreateDelegate를 사용하면 Invoke보다 훨씬 빠릅니다.
            AddDelegate addFunc = (AddDelegate)Delegate.CreateDelegate(typeof(AddDelegate), instance, method);
            if (addFunc == null)
                return;

            // 3. 이제 일반 함수처럼 호출 가능
            int result = addFunc(50, 60);
            MessageBox.Show($"대리자 호출 결과: {result}");
        }
    }
}
