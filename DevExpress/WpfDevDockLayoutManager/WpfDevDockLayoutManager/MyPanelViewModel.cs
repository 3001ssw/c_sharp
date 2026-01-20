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

        private Visibility isVisibility = Visibility.Visible;
        public Visibility IsVisibility { get => isVisibility; set => SetValue(ref isVisibility, value); }

        private bool isActive = false;
        public bool IsActive { get => isActive; set => SetValue(ref isActive, value); }

        private bool autoHidden = false;
        public bool AutoHidden { get => autoHidden; set => SetValue(ref autoHidden, value); }

        #endregion

        #region command properties

        public DelegateCommand PanelClosedCommand { get; private set; }

        #endregion

        #region constructor

        public MyPanelViewModel()
        {
            PanelClosedCommand = new DelegateCommand(OnPanelClosed, CanPanelClosed);

            Uri uri = new Uri("pack://application:,,,/DevExpress.Images.v22.2;component/SvgImages/Business Objects/BO_Skull.svg");
            CaptionImage = WpfSvgRenderer.CreateImageSource(uri);
            AutoHidden = true;
        }

        #endregion

        private void OnPanelClosed()
        {
            IsVisibility = Visibility.Collapsed;
        }

        private bool CanPanelClosed()
        {
            return true;
        }
    }
}
