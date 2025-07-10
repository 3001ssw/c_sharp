using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.Services
{
    public interface IDialogService
    {
        bool? ShowDialog(object viewModel);
        void CloseDialog(bool result = false);
    }
}
