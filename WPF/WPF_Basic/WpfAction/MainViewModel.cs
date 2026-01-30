using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfAction.DirectEnumToBoolConverter;

namespace WpfAction
{
    public class MainViewModel : BindableBase
    {
        #region fields, properties
        private DirectType selectedDirect = DirectType.Top;
        public DirectType SelectedDirect
        {
            get => selectedDirect;
            set
            {
                SetProperty(ref selectedDirect, value);
                UpdateMoveAction();
            }
        }

        private PointXY point = new PointXY();
        public PointXY Point { get => point; set => SetProperty(ref point, value); }

        private ObservableCollection<MessageViewModel> messages = new ObservableCollection<MessageViewModel>();
        public ObservableCollection<MessageViewModel> Messages { get => messages; set => SetProperty(ref messages, value); }

        private Action? DoMove = null;
        #endregion



        #region commands
        public DelegateCommand MoveCommand { get; private set; }
        #endregion

        #region command methods
        private void OnMove()
        {
            DoMove?.Invoke();
        }

        private bool CanMove()
        {
            return true;
        }

        #endregion

        #region functions
        private void UpdateMoveAction()
        {
            switch (SelectedDirect)
            {
                case DirectType.Top: DoMove = Point.MoveTop; break;
                case DirectType.Left: DoMove = Point.MoveLeft; break;
                case DirectType.Right: DoMove = Point.MoveRight; break;
                case DirectType.Bottom: DoMove = Point.MoveBottom; break;
            }
        }
        private void AddMessage(int x, int y)
        {
            string msg = $"({x}, {y}) 좌표로 이동하였습니다.";
            Messages.Add(new MessageViewModel()
            {
                Message = msg,
            });
        }

        #endregion

        #region constructor
        public MainViewModel()
        {
            MoveCommand = new DelegateCommand(OnMove, CanMove);
            Point.SetMoveResultAction(AddMessage);
            UpdateMoveAction();
        }
        #endregion
    }
}
