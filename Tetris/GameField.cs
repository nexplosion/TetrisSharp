using System;
using System.Linq;

namespace Tetris
{
    public class GameField :
#if !WINFORMSGUI
        FieldRendererGUI
#else
        FieldRenderer
#endif

    {
        private static TimeSpan[] _spans = new TimeSpan[] 
        {
            new TimeSpan(0, 0, 0, 0, 1000),
            new TimeSpan(0, 0, 0, 0, 900),
            new TimeSpan(0, 0, 0, 0, 800),
            new TimeSpan(0, 0, 0, 0, 700),
            new TimeSpan(0, 0, 0, 0, 600),
            new TimeSpan(0, 0, 0, 0, 500),
            new TimeSpan(0, 0, 0, 0, 400),
            new TimeSpan(0, 0, 0, 0, 300),
            new TimeSpan(0, 0, 0, 0, 250),
            new TimeSpan(0, 0, 0, 0, 200)
        };

        private TimeSpan _span = _spans.First();
        private Point _glassPosition = new Point { x = 20, y = 1 };

        public override void Init()
        {
            base.Init();
            _glass.Draw(_glassPosition);
        }

        public void Start()
        {
            var quit = false;
            var pause = false;

            while (!quit && _glass.Place())
            {
                _glass.Draw(_glassPosition);
                PrintScores();
                var _gameCycle = true;
                var time = DateTime.Now;
                var down = false;

                while (!quit && _gameCycle)
                {
                    if (down || DateTime.Now - time >= _span)
                    {
                        if (!pause)
                        {
                            _gameCycle = _glass.Move(new Point(0, 1));
                            _glass.Draw(_glassPosition);
                            if (!_gameCycle)
                            {
                                var lNum = _glass.Destroy();
                                _score += (UInt64)(lNum * _level * 10);
                                _lines += lNum;
                                if (_lines >= 50 * _level && _level < _spans.Length)
                                {
                                    _level++;
                                    _span = _spans[_level - 1];
                                }

                                _glass.Draw(_glassPosition);
                            }
                        }
                        down = false;
                        time = DateTime.Now;
                    }
                    else
                    {
                        var action = GetAction();
                        if (action != GameActions.none)
                        {
                            switch (action)
                            {
                                case GameActions.moveLeft:
                                    _glass.Move(new Point(-1, 0));
                                    break;

                                case GameActions.moveRight:
                                    _glass.Move(new Point(1, 0));
                                    break;

                                case GameActions.rotate:
                                    _glass.Rotate();
                                    break;

                                case GameActions.moveDown:
                                    _score += (UInt64)_level;
                                    PrintScores();
                                    down = true;
                                    break;

                                case GameActions.quit:
                                    quit = true;
                                    break;

                                case GameActions.pause:
                                    pause = !pause;
                                    break;
                            }

                            _glass.Draw(_glassPosition, pause);
                        }
                    }
                }
            }
        }

        public GameField() { }
        public GameField(IntPtr hWnd = default(IntPtr))
        {
            this.hWnd = hWnd;
        }
    }
}
