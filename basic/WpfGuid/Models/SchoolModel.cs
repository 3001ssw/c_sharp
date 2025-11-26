namespace WpfGuid.Models
{
    public partial class SchoolModel : BindableBase
    {
        private string id = "";
        public string Id { get => id; set => SetProperty(ref id, value); }

        private string name = "";
        public string Name { get => name; set => SetProperty(ref name, value); }

        private string address = "";
        public string Address { get => address; set => SetProperty(ref address, value); }

        public SchoolModel()
        {
            Id = Guid.NewGuid().ToString();
            Name = "";
            Address = "";
        }
    }
}
