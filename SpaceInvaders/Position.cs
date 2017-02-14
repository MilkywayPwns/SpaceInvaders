using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Position
    {
        public int X
        {
            get
            {
                return X;
            }
            set
            {
                X = value;
            }
        }
        public int Y
        {
            get
            {
                return Y;
            }
            set
            {
                Y = value;
            }
        }

        public Position(int x, int y)
        {
            // set position variables
            X = x;
            Y = y;
        }
    }
}
