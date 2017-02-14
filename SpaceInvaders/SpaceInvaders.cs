using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class GameWindow
    {
        public GameWindow()
        {

        }

        public void Clear()
        {
            Console.Clear();
        }
        public void Draw(Position pos, ConsoleColor backcolor, ConsoleColor frontcolor, string text)
        {
            Console.BackgroundColor = backcolor;
            Console.ForegroundColor = frontcolor;

            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write(text);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class SpaceInvaders
    {
        public enum State
        {
            Menu = 0,
            Game = 1,
        }

        private List<Alien>     _aliens;
        private State           _state;
        private GameWindow      _window;

        public SpaceInvaders()
        {
            _aliens = new List<Alien>();
            _window = new GameWindow();
            _state = 0;
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
            // clear console window
            _window.Clear();

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
                _window.Draw(new Position(15, 15), ConsoleColor.Black, ConsoleColor.White, "Space Invaders!");
            }
        }
    }
}
