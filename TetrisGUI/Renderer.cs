using System;
using System.Drawing;

namespace Tetris
{
    public class FieldRenderer
    {
        protected enum GameActions { none, quit, pause, moveLeft, moveRight, moveDown, rotate };

        protected static int _width = 80;
        protected static int _heigth = 30;
        protected int _lines = 0;
        protected int _level = 1;
        protected UInt64 _score;

        protected Graphics g = null;
        protected GameGlass _glass = new GameGlass();
        protected IntPtr hWnd;

        protected virtual void PrintScores()
        {
            Console.SetCursorPosition(50, 2);
            Console.Write($"Level: {_level}");
            Console.SetCursorPosition(50, 4);
            Console.Write($"Lines: {_lines}");
            Console.SetCursorPosition(50, 5);
            Console.Write($"Score: {_score}");
        }

        protected virtual GameActions GetAction()
        {
            var r = GameActions.none;

            if (Console.KeyAvailable)
            {
                var cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        r = GameActions.moveLeft;
                        break;

                    case ConsoleKey.RightArrow:
                        r = GameActions.moveRight;
                        break;

                    case ConsoleKey.UpArrow:
                        r = GameActions.rotate;
                        break;

                    case ConsoleKey.DownArrow:
                        r = GameActions.moveDown;
                        break;

                    case ConsoleKey.Escape:
                        r = GameActions.quit;
                        break;

                    case ConsoleKey.P:
                        r = GameActions.pause;
                        break;
                }
            }

            return r;
        }

        public virtual void Init()
        {
            Console.SetWindowSize(_width, _heigth);
            Console.Clear();
        }
    }

    public class FieldRendererGUI
    {
        protected enum GameActions { none, quit, pause, moveLeft, moveRight, moveDown, rotate };

        protected static int _width = 80;
        protected static int _heigth = 30;
        protected int _lines = 0;
        protected int _level = 1;
        protected UInt64 _score;

        protected IntPtr hWnd;
        protected Graphics g = null;
        protected GameGlass _glass;

        protected virtual void PrintScores()
        {
            /*
            Console.SetCursorPosition(50, 2);
            Console.Write($"Level: {_level}");
            Console.SetCursorPosition(50, 4);
            Console.Write($"Lines: {_lines}");
            Console.SetCursorPosition(50, 5);
            Console.Write($"Score: {_score}");
            */
        }

        protected virtual GameActions GetAction()
        {
            var r = GameActions.none;
            /*
            if (Console.KeyAvailable)
            {
                var cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        r = GameActions.moveLeft;
                        break;

                    case ConsoleKey.RightArrow:
                        r = GameActions.moveRight;
                        break;

                    case ConsoleKey.UpArrow:
                        r = GameActions.rotate;
                        break;

                    case ConsoleKey.DownArrow:
                        r = GameActions.moveDown;
                        break;

                    case ConsoleKey.Escape:
                        r = GameActions.quit;
                        break;

                    case ConsoleKey.P:
                        r = GameActions.pause;
                        break;
                }
            }
            */
            return r;
        }

        public virtual void Init()
        {
            g = Graphics.FromHwnd(hWnd);
            g.Clear(Color.Transparent);
            _glass = new GameGlass(g);
        }

        ~FieldRendererGUI()
        {
            g.Dispose();
        }
    }

    public class GlassRenderer
    {
        protected Graphics g = null;

        protected const int _width = 10;
        protected const int _height = 20;
        protected int[,] _field = new int[_width, _height];

        private string emptyFiller = "  ";
        private string fullFiller = "██";

        public virtual void Draw(Point p, bool paused = false)
        {
            var fieldFiller = $"│{new string(' ', _width * 2)}│";
            var bottomFiller = $"└{new string('─', _width * 2)}┘";

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(p.x, p.y);
            Console.Write(fieldFiller);

            for (int i = 1; i <= _height; i++)
            {

                Console.SetCursorPosition(p.x, p.y + i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("│");
                for (int j = 0; j < _width; j++)
                {
                    Console.Write((Console.ForegroundColor = (ConsoleColor)_field[j, i - 1]) == 0 ? emptyFiller : fullFiller);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("│");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(p.x, p.y + _height + 1);
            Console.Write(bottomFiller);

            if (paused)
            {
                Console.SetCursorPosition(p.x + 8, p.y + 10);
                Console.Write("Pause!");
            }
        }
    }

    public class GlassRendererGUI
    {
        protected Graphics g = null;

        protected const int _width = 10;
        protected const int _height = 20;
        protected int[,] _field = new int[_width, _height];

        private const int _dashSize = 10;
        private string emptyFiller = "  ";
        private string fullFiller = "██";

        public virtual void Draw(Point p, bool paused = false)
        {
            /*
            var fieldFiller = $"│{new string(' ', _width * 2)}│";
            var bottomFiller = $"└{new string('─', _width * 2)}┘";

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(p.x, p.y);
            Console.Write(fieldFiller);
            */

            Pen pen = new Pen(Color.Wheat, 1);
            System.Drawing.Point[] points =
                 {
                     new System.Drawing.Point(p.x * _dashSize, p.y * _dashSize),
                     new System.Drawing.Point(p.x * _dashSize, p.y * _dashSize + (_height + 2) * _dashSize + _dashSize),
                     new System.Drawing.Point(p.x * _dashSize + (_width + 2) * _dashSize + _dashSize, p.y * _dashSize + (_height + 2) * _dashSize + _dashSize),
                     new System.Drawing.Point(p.x * _dashSize + (_width + 2) * _dashSize + _dashSize, p.y * _dashSize)
                 };
            g.DrawLines(pen, points);
            pen.Dispose();
            pen = new Pen(Color.DarkGreen, 1);
            for (int x = 1; x < _width; x++)
            {
                var xCoord = p.x * _dashSize + x * _dashSize + 1;
                var yCoord = p.y * _dashSize + _dashSize;
                g.DrawLine(pen, xCoord, yCoord, xCoord, yCoord + _dashSize * _height + 1);
            }

            for (int i = 1; i <= _height; i++)
            {

                /*
                Console.SetCursorPosition(p.x, p.y + i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("│");
                for (int j = 0; j < _width; j++)
                {
                    Console.Write((Console.ForegroundColor = (ConsoleColor)_field[j, i - 1]) == 0 ? emptyFiller : fullFiller);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("│");
                */
            }

            /*
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(p.x, p.y + _height + 1);
            Console.Write(bottomFiller);
            */

            if (paused)
            {
                /*
                Console.SetCursorPosition(p.x + 8, p.y + 10);
                Console.Write("Pause!");
                */
            }
        }
    }
}
