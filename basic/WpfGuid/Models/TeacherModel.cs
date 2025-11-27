using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfGuid.Models
{
    public class TeacherModel : BindableBase
    {
        public string Id { get; private set; }

        private string name = "";
        public string Name { get => name; set => SetProperty(ref name, value); }

        private int age = 0;
        public int Age { get => age; set => SetProperty(ref age, value); }

        private string gender = "";
        public string Gender { get => gender; set => SetProperty(ref gender, value); }

        private string phoneNumber = "";
        public string PhoneNumber { get => phoneNumber; set => SetProperty(ref phoneNumber, value); }

        private string school = "";
        public string School { get => school; set => SetProperty(ref school, value); }

        public TeacherModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
