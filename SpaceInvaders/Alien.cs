using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Alien
    {
        private Position        _pos;

        public Position Position
        {
            get
            {
                return _pos;
            }
        }

        public Alien(Position pos)
        {
            _pos = pos;
        }

        // updates the alien
        public void Update(SpaceInvaders game)
        {
            Position.Y += 1;
        }

        // draws the alien on the screen
        public void Draw(SpaceInvaders game)
        {
            game.Window.DrawText(Position, ConsoleColor.Black, ConsoleColor.Red, "███");
        }
    }
}
