using DevExpress.Mvvm;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfDevDockLayoutManager
{
    public class MyPanelViewModel : ViewModelBase, IMVVMDockingProperties
    {
        #region fields, properties
        private string caption = "";
        public string Caption { get => caption; set => SetValue(ref caption, value); }

        private ImageSource captionImage;
        public ImageSource CaptionImage { get => captionImage; set => SetValue(ref captionImage, value); }

        private string targetName = "LeftGroup";
        public string TargetName
        {
            get => targetName;
            set => SetValue(ref targetName, value);
        }

        private bool isClosed = false;
        public bool IsClosed { get => isClosed; set => SetValue(ref isClosed, value); }

        private Visibility _isPanelVisible = Visibility.Visible;
        public Visibility IsPanelVisible { get => _isPanelVisible; set => SetValue(ref _isPanelVisible, value); }
        #endregion

        public DelegateCommand PanelClosedCommand { get; private set; }

        public virtual IDocumentManagerService DocumentManagerService { get; set; }

        public MyPanelViewModel()
        {
            PanelClosedCommand = new DelegateCommand(OnPanelClosed, CanPanelClosed);

            Uri uri = new Uri("pack://application:,,,/DevExpress.Images.v22.2;component/SvgImages/Business Objects/BO_Skull.svg");
            CaptionImage = WpfSvgRenderer.CreateImageSource(uri);

        }

        private void OnPanelClosed()
        {
            IsPanelVisible = Visibility.Collapsed;
        }

        private bool CanPanelClosed()
        {
            return true;
        }
    }
}
