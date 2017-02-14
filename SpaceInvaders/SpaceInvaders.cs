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
            Console.SetWindowSize(120, 30);
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
        public void DrawBox(Position pos, Position size, ConsoleColor innercolor, ConsoleColor bordercolor)
        {
            Console.BackgroundColor = innercolor;
            Console.ForegroundColor = bordercolor;

            for (int y = pos.Y; y < pos.Y + size.Y; y++)
            {
                Console.SetCursorPosition(pos.X, y);

                if (y == pos.Y || y == (pos.Y + size.Y) - 1)
                {
                    for (int x = pos.X; x < pos.X + size.X; x++)
                        Console.Write("█");
                }
                else
                {
                    Console.Write("█");
                    Console.SetCursorPosition((pos.X + size.X) - 1, y);
                    Console.Write("█");
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class SpaceInvaders
    {
        public enum GameState
        {
            Menu = 0,
            Game = 1,
        }

        private List<Alien>     _aliens;
        private GameState       _state;
        private GameWindow      _window;
        private Player          _player;

        public GameWindow Window
        {
            get
            {
                return _window;
            }
        }
        public GameState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        public Player Player
        {
            get
            {
                return _player;
            }
        }

        public SpaceInvaders()
        {
            _aliens = new List<Alien>();
            _window = new GameWindow();
            _player = new Player();
            _state = 0;
        }

        // Updates the game
        public void Update()
        {
            // If state is game
            if (State == GameState.Game)
            {
                // Update player
                Player.Update(this);

                // Update all aliens
                foreach (Alien alien in _aliens)
                {
                    alien.Update(this);
                }
            }
            // If state is menu
            else if (State == GameState.Menu)
            {

            }
        }

        public void OnConsoleInput(ConsoleKey key)
        {
            // Menu keys
            if (State == GameState.Menu)
            {
                if (key == ConsoleKey.S)
                {
                    State = GameState.Game;
                }
                else if (key == ConsoleKey.Q)
                {
                    Environment.Exit(0);
                }
            }
            // Game keys
            else if (State == GameState.Game)
            {
                if (key == ConsoleKey.Spacebar)
                {
                    Player.Shoot();
                }
                else if (key == ConsoleKey.A)
                {
                    Player.GoLeft();
                }
                else if (key == ConsoleKey.D)
                {
                    Player.GoRight();
                }
            }
        }

        // Draws the game
        public void Draw()
        {
            // clear console window
            Window.Clear();

            // If state is game
            if (State == GameState.Game)
            {
                // Draw game box
                Window.DrawBox(new Position(5, 3), new Position(80, 24), ConsoleColor.Black, ConsoleColor.Green);

                // Draw info box
                Window.DrawBox(new Position(86, 3), new Position(30, 6), ConsoleColor.Black, ConsoleColor.Green);

                // Info text
                Window.DrawText(new Position(87, 4), ConsoleColor.Black, ConsoleColor.Green, "Keys:");
                Window.DrawText(new Position(87, 5), ConsoleColor.Black, ConsoleColor.Green, "[A]     - Left");
                Window.DrawText(new Position(87, 6), ConsoleColor.Black, ConsoleColor.Green, "[D]     - Right");
                Window.DrawText(new Position(87, 7), ConsoleColor.Black, ConsoleColor.Green, "[Space] - Shoot");

                // Draw player
                Player.Draw(this);

                // Draw all aliens
                foreach (Alien alien in _aliens)
                {
                    alien.Draw(this);
                }
            }
            else if (State == GameState.Menu)
            {
                // Title
                Window.DrawText(new Position(5, 3), ConsoleColor.Black, ConsoleColor.Green, "Space Invaders!");
                Window.DrawText(new Position(5, 4), ConsoleColor.Black, ConsoleColor.Green, "Credits: Micky Langeveld & Robin de Bruin");

                // Buttons
                Window.DrawText(new Position(5, 6), ConsoleColor.Black, ConsoleColor.Green, "- [S]tart");
                Window.DrawText(new Position(5, 7), ConsoleColor.Black, ConsoleColor.Green, "- [Q]uit");

                // Input line
                Window.DrawText(new Position(0, Console.WindowHeight - 1), ConsoleColor.Black, ConsoleColor.Green, "Please select an option: ");
            }
        }
    }
}
