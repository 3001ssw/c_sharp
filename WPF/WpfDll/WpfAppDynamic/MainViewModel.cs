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
            // DLL 파일을 런타임에 메모리로 로드합니다.
            Assembly assembly = Assembly.LoadFrom("MathLibrary.dll");
            if (assembly == null)
                return;

            // DLL 내부의 특정 클래스(네임스페이스 포함) 정보를 가져옵니다.
            Type? type = assembly.GetType("MathLibrary.Calculator");
            if (type == null)
                return;

            // 찾은 클래스 타입을 바탕으로 실제 객체(인스턴스)를 생성합니다.
            dynamic? instance = Activator.CreateInstance(type);
            if (instance == null)
                return;

            // 1. dynamic 방식: 컴파일러가 형 검사를 유예하며, 런타임에 메서드를 찾아 실행합니다.
            int result = instance.Add(50, 60);
            MessageBox.Show($"dynamic 호출 결과: {result}");

            // 리플렉션을 통해 "Add"라는 이름의 메서드 메타데이터를 추출합니다.
            MethodInfo? method = type.GetMethod("Add");
            if (method == null)
                return;

            // 2. Invoke 방식: 메서드 정보를 통해 파라미터를 배열 형태로 전달하여 호출합니다.
            object[] parameters = { 30, 40 };
            result = (int)method.Invoke(instance, parameters);
            MessageBox.Show($"Invoke 호출 결과: {result}");

            // 3. Delegate 방식: 메서드 정보를 특정 대리자(AddDelegate) 형식으로 변환하여 연결합니다.
            // 이후 호출부터는 리플렉션 비용 없이 직접 호출과 유사한 속도로 작동합니다.
            AddDelegate addFunc = (AddDelegate)Delegate.CreateDelegate(typeof(AddDelegate), instance, method);
            if (addFunc == null)
                return;

            result = addFunc(50, 60);
            MessageBox.Show($"대리자 호출 결과: {result}");
        }
    }
}
