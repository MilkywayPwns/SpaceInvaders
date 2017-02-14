using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Alien
    {
        private Position        _pos;
        private ConsoleColor    _color;

        public Alien(Position pos, ConsoleColor color)
        {
            _pos = pos;
            _color = color;
        }
    }
}
