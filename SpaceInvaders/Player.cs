using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Player
    {
        private int _score;
        private Position _pos;

        public Position Position
        {
            get
            {
                return _pos;
            }
        }
        public int Score
        {
            get
            {
                return _score;
            }
        }

        public Player()
        {
            _score = 0;
            _pos = new Position(43, 25);
        }

        public void Shoot()
        {

        }

        public void GoLeft()
        {
            _pos.X -= 1;
        }

        public void GoRight()
        {
            _pos.X += 1;
        }

        public void Update(SpaceInvaders game)
        {

        }

        public void Draw(SpaceInvaders game)
        {
            game.Window.DrawText(Position, ConsoleColor.Black, ConsoleColor.Blue, "███");
        }
    }
}
