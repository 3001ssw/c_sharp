using animals;

namespace class_basic
{
    public partial class Form1 : Form
    {
        public CAnimal? selectedAnimal = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listAnimal.Items.Clear();
            listAnimal.Items.Add(new AnimalListViewItem(new CPerson()));
            listAnimal.Items.Add(new AnimalListViewItem(new CDog()));
            listAnimal.Items.Add(new AnimalListViewItem(new CBird()));
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(listAnimal) && listAnimal.SelectedItem != null)
            {
                AnimalListViewItem item = (AnimalListViewItem)listAnimal.SelectedItem;
                CAnimal anim = item.Animal;
                selectedAnimal = anim;

                string str = "selected: " + anim.Info() + Environment.NewLine;
                txtShow.AppendText(str);
            }
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            if (selectedAnimal != null)
            {
                string str = selectedAnimal.Sound() + Environment.NewLine;
                txtShow.AppendText(str);
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (selectedAnimal != null)
            {
                string str = selectedAnimal.Move() + Environment.NewLine;
                txtShow.AppendText(str);
            }
        }

        private void btnEat_Click(object sender, EventArgs e)
        {
            if (selectedAnimal != null)
            {
                string str = selectedAnimal.Eat() + Environment.NewLine;
                txtShow.AppendText(str);
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (selectedAnimal != null)
            {
                string str = selectedAnimal.Info() + Environment.NewLine;
                txtShow.AppendText(str);
            }
        }
    }

    public class AnimalListViewItem : ListViewItem
    {
        private CAnimal animal;

        public CAnimal Animal
        {
            get { return animal; } set { animal = value; }
        }

        public AnimalListViewItem(CAnimal anim)
        {
            this.animal = anim;
        }

        public override string ToString()
        {
            return animal.Info();
        }
    }
}
