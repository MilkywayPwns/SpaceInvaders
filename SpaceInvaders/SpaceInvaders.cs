using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class SpaceInvaders
    {
        public enum State
        {
            Menu = 0,
            Game = 1,
        }

        private List<Alien>     _aliens;
        private State           _state;

        public SpaceInvaders()
        {
            _aliens = new List<Alien>();
        }

        // Updates the game
        public void Update()
        {
            // If state is game
            if (_state == State.Game)
            {
                // Update all aliens
                foreach (Alien alien in _aliens)
                {
                    alien.Update();
                }
            }
            // If state is menu
            else if (_state == State.Menu)
            {

            }
        }

        // Draws the game
        public void Draw()
        {
            // If state is game
            if (_state == State.Game)
            {
                // Draw all aliens
                foreach (Alien alien in _aliens)
                {
                    alien.Draw();
                }
            }
            else if (_state == State.Menu)
            {
                // Draw menu
            }
        }
    }
}
