using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class SpaceInvaders
    {
        private List<Alien> _aliens;

        public SpaceInvaders()
        {
            _aliens = new List<Alien>();
        }

        // Updates the game
        public void Update()
        {
            // Update all aliens
            foreach (Alien alien in _aliens)
            {
                alien.Update();
            }
        }

        // Draws the game
        public void Draw()
        {
            // Draw all aliens
            foreach (Alien alien in _aliens)
            {
                alien.Draw();
            }
        }
    }
}
