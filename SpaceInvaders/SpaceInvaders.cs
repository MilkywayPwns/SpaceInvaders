using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpaceInvaders
{
    class GameWindow
    {
        public GameWindow()
        {

        }

        public void Clear()
        {
            Console.SetWindowSize(145, 40);
            Console.Clear();
        }
        public void ClearGame()
        {
            Console.SetWindowSize(145, 40);
            DrawBox(new Position(6, 4), new Position(98, 32), ConsoleColor.Black, ConsoleColor.Black);

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
            GameOver = 2,
        }

        private List<Alien>     _aliens;
        private GameState       _state;
        private GameWindow      _window;
        private Player          _player;
        private int             _maxaliens;
        private Mutex           _mutex;

        public Mutex Mutex
        {
            get
            {
                return _mutex;
            }
        }

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
        public List<Alien> Aliens
        {
            get
            {
                return _aliens;
            }
        }

        public SpaceInvaders()
        {
            _mutex = new Mutex();
            _aliens = new List<Alien>();
            _window = new GameWindow();
            _player = new Player();
            _state = 0;
            _maxaliens = 5;

            Window.Clear();
        }

        // Updates the game
        public void Update()
        {
            // If state is game
            if (State == GameState.Game)
            {
                // Update player
                Player.Update(this);
            }
            // If state is menu
            /*else if (State == GameState.Menu)
            {

            }
            // if state is game over
            else if (State == GameState.GameOver)
            {

            }*/
        }

        public void AlienThread()
        {
            while (true)
            {
                Thread.Sleep(1000);

                Mutex.WaitOne();
                foreach (Alien alien in Aliens)
                {
                    alien.Update(this);
                }
                Mutex.ReleaseMutex();
            }
        }

        public void Initialize()
        {
            // spawn aliens
            AddAliens();

            // start alien thread
            // Thread alienthread = new Thread(AlienThread);
            // alienthread.Start();
        }

        public void OnConsoleInput(ConsoleKey key)
        {
            // Menu keys
            if (State == GameState.Menu)
            {
                if (key == ConsoleKey.S)
                {
                    Window.Clear();
                    State = GameState.Game;
                    Initialize();

                    // Draw game box
                    Window.DrawBox(new Position(5, 3), new Position(100, 34), ConsoleColor.Black, ConsoleColor.Green);

                    // Draw info box
                    Window.DrawBox(new Position(106, 3), new Position(30, 6), ConsoleColor.Black, ConsoleColor.Green);

                    // Info text
                    Window.DrawText(new Position(107, 4), ConsoleColor.Black, ConsoleColor.Green, "Keys:");
                    Window.DrawText(new Position(107, 5), ConsoleColor.Black, ConsoleColor.Green, "[A]     - Left");
                    Window.DrawText(new Position(107, 6), ConsoleColor.Black, ConsoleColor.Green, "[D]     - Right");
                    Window.DrawText(new Position(107, 7), ConsoleColor.Black, ConsoleColor.Green, "[Space] - Shoot");
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

        public void AddAliens()
        {
            Mutex.WaitOne();
            Aliens.Add(new Alien(new Random().Next(10, 90)));
            Aliens.Add(new Alien(new Random().Next(10, 90)));
            Aliens.Add(new Alien(new Random().Next(10, 90)));
            Aliens.Add(new Alien(new Random().Next(10, 90)));
            Aliens.Add(new Alien(new Random().Next(10, 90)));
            Mutex.ReleaseMutex();
        }

        // Checks if there are enough aliens, if not, adds aliens.
        public void CheckAliens()
        {
            Mutex.WaitOne();
            int alienCount = Aliens.Count;
            for (int i = alienCount; i < _maxaliens; i++)
            {
                int alien_x = new Random().Next(10, 90);
                Aliens.Add(new Alien(alien_x));
            }
            Mutex.ReleaseMutex();
        }

        // Draws the game
        public void Draw()
        {
            // clear console window
            // Window.Clear();

            // If state is game
            if (State == GameState.Game)
            {
                Window.ClearGame();

                // Draw player
                Player.Draw(this);

                // Draw all aliens
                Mutex.WaitOne();
                foreach (Alien alien in _aliens)
                {
                    alien.Draw(this);
                }
                Mutex.ReleaseMutex();
            }
            else if (State == GameState.Menu)
            {
                // Space Invaders
                Window.DrawText(new Position(1, 1), ConsoleColor.Black, ConsoleColor.Green, " ________  ________  ________  ________  _______           ___  ________   ___      ___ ________  ________  _______   ________  ________      ");
                Window.DrawText(new Position(1, 2), ConsoleColor.Black, ConsoleColor.Green,  "|\\   ____\\|\\   __  \\|\\   __  \\|\\   ____\\|\\  ___ \\         |\\  \\|\\   ___  \\|\\  \\    /  /|\\   __  \\|\\   ___ \\|\\  ___ \\ |\\   __  \\|\\   ____\\     ");
                Window.DrawText(new Position(1, 3), ConsoleColor.Black, ConsoleColor.Green, "\\ \\  \\___|\\ \\  \\|\\  \\ \\  \\|\\  \\ \\  \\___|\\ \\   __/|        \\ \\  \\ \\  \\\\ \\  \\ \\  \\  /  / | \\  \\|\\  \\ \\  \\_|\\ \\ \\   __/|\\ \\  \\|\\  \\ \\  \\___|_    ");
                Window.DrawText(new Position(1, 4), ConsoleColor.Black, ConsoleColor.Green, " \\ \\_____  \\ \\   ____\\ \\   __  \\ \\  \\    \\ \\  \\_|/__       \\ \\  \\ \\  \\\\ \\  \\ \\  \\/  / / \\ \\   __  \\ \\  \\ \\\\ \\ \\  \\_|/_\\ \\   _  _\\ \\_____  \\   ");
                Window.DrawText(new Position(1, 5), ConsoleColor.Black, ConsoleColor.Green, "  \\|____|\\  \\ \\  \\___|\\ \\  \\ \\  \\ \\  \\____\\ \\  \\_|\\ \\       \\ \\  \\ \\  \\\\ \\  \\ \\    / /   \\ \\  \\ \\  \\ \\  \\_\\\\ \\ \\  \\_|\\ \\ \\  \\\\  \\\\|____|\\  \\  ");
                Window.DrawText(new Position(1, 6), ConsoleColor.Black, ConsoleColor.Green, "    ____\\_\\  \\ \\__\\    \\ \\__\\ \\__\\ \\_______\\ \\_______\\       \\ \\__\\ \\__\\\\ \\__\\ \\__/ /     \\ \\__\\ \\__\\ \\_______\\ \\_______\\ \\__\\\\ _\\ ____\\_\\  \\ ");
                Window.DrawText(new Position(1, 7), ConsoleColor.Black, ConsoleColor.Green, "   |\\_________\\|__|     \\|__|\\|__|\\|_______|\\|_______|        \\|__|\\|__| \\|__|\\|__|/       \\|__|\\|__|\\|_______|\\|_______|\\|__|\\|__|\\_________\\");
                Window.DrawText(new Position(1, 8), ConsoleColor.Black, ConsoleColor.Green, "   \\|_________|                                                                                                                   \\|_________|");

                // Credits
                Window.DrawText(new Position(5, 11), ConsoleColor.Black, ConsoleColor.Green, "Credits: Micky Langeveld & Robin de Bruin");

                // Actions
                Window.DrawText(new Position(5, 13), ConsoleColor.Black, ConsoleColor.Green, "- [S]tart");
                Window.DrawText(new Position(5, 14), ConsoleColor.Black, ConsoleColor.Green, "- [Q]uit");

                // Input line
                Window.DrawText(new Position(0, Console.WindowHeight - 1), ConsoleColor.Black, ConsoleColor.Green, "Please select an option: ");
            }
            // game over state
            else if (State == GameState.GameOver)
            {
                // Game Over
                Window.DrawText(new Position(1, 1), ConsoleColor.Black, ConsoleColor.Red, " ________  ________  _____ ______   _______           ________  ___      ___ _______   ________     ");
                Window.DrawText(new Position(1, 2), ConsoleColor.Black, ConsoleColor.Red, "|\\   ____\\|\\   __  \\|\\   _ \\  _   \\|\\  ___ \\         |\\   __  \\|\\  \\    /  /|\\  ___ \\ |\\   __  \\    ");
                Window.DrawText(new Position(1, 3), ConsoleColor.Black, ConsoleColor.Red, "\\ \\  \\___|\\ \\  \\|\\  \\ \\  \\\\\\__\\ \\  \\ \\   __/|        \\ \\  \\|\\  \\ \\  \\  /  / | \\   __/|\\ \\  \\|\\  \\   ");
                Window.DrawText(new Position(1, 4), ConsoleColor.Black, ConsoleColor.Red, " \\ \\  \\  __\\ \\   __  \\ \\  \\\\|__| \\  \\ \\  \\_|/__       \\ \\  \\\\\\  \\ \\  \\/  / / \\ \\  \\_|/_\\ \\   _  _\\  ");
                Window.DrawText(new Position(1, 5), ConsoleColor.Black, ConsoleColor.Red, "  \\ \\  \\|\\  \\ \\  \\ \\  \\ \\  \\    \\ \\  \\ \\  \\_|\\ \\       \\ \\  \\\\\\  \\ \\    / /   \\ \\  \\_|\\ \\ \\  \\\\  \\| ");
                Window.DrawText(new Position(1, 6), ConsoleColor.Black, ConsoleColor.Red, "   \\ \\_______\\ \\__\\ \\__\\ \\__\\    \\ \\__\\ \\_______\\       \\ \\_______\\ \\__/ /     \\ \\_______\\ \\__\\\\ _\\ ");
                Window.DrawText(new Position(1, 7), ConsoleColor.Black, ConsoleColor.Red, "    \\|_______|\\|__|\\|__|\\|__|     \\|__|\\|_______|        \\|_______|\\|__|/       \\|_______|\\|__|\\|__|");
            }
        }
    }
}
