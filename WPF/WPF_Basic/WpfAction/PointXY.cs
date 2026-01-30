using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAction
{
    public class PointXY : BindableBase
    {
        #region fields, properties

        private int x = 0;
        public int X { get => x; set => SetProperty(ref x, value); }

        private int y = 0;
        public int Y { get => y; set => SetProperty(ref y, value); }

        private Action<int, int>? action = null;
        #endregion

        #region functions

        public void MoveLeft()
        {
            X -= 1;
            action?.Invoke(X, Y);
        }

        public void MoveRight()
        {
            X += 1;
            action?.Invoke(X, Y);
        }

        public void MoveTop()
        {
            Y -= 1;
            action?.Invoke(X, Y);
        }

        public void MoveBottom()
        {
            Y += 1;
            action?.Invoke(X, Y);
        }

        #endregion


        #region functions

        public void SetMoveResultAction(Action<int, int> act)
        {
            action = act;
        }

        #endregion



        #region constructor
        public PointXY()
        {

        }
        #endregion

    }
}
