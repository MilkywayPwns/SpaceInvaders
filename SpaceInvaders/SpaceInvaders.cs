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
        public void DrawText(Position pos, ConsoleColor backcolor, ConsoleColor frontcolor, string text)
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

        public void OnConsoleInput(ConsoleKey key)
        {
            // Menu keys
            if (_state == State.Menu)
            {
                if (key == ConsoleKey.S)
                {
                    
                    _state = State.Game;
                }
                else if (key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
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
                // Title
                _window.DrawText(new Position(5, 3), ConsoleColor.Black, ConsoleColor.Green, "Space Invaders!");

                // Buttons
                _window.DrawText(new Position(5, 5), ConsoleColor.Black, ConsoleColor.Green, "[S]tart");
                _window.DrawText(new Position(5, 6), ConsoleColor.Black, ConsoleColor.Green, "[Q]uit");

                // Input line
                _window.DrawText(new Position(0, Console.WindowHeight - 1), ConsoleColor.Black, ConsoleColor.Green, "Please select an option: ");
            }
        }
    }
}
