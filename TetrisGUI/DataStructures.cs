namespace Tetris
{
    public struct Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public abstract class Figure
    {
        public Point position = new Point(0, 0);
        public int color = 1;

        protected int _rotation = 0;
        protected Point[] _figure;
        protected Point[] _currentFigure;
        protected int _oldRotation;
        protected Point[] _oldFigure;

        protected virtual void SaveFigureState()
        {
            _oldRotation = _rotation;
            _oldFigure = _currentFigure;
        }
        protected virtual void RestoreFigureState()
        {
            _rotation = _oldRotation;
            _currentFigure = _oldFigure;
        }
        protected virtual void Init()
        {
            _currentFigure = new Point[_figure.Length];
            _figure.CopyTo(_currentFigure, 0);
        }

        public abstract void Rotate();
        public virtual void DeclineRotate()
        {
            RestoreFigureState();
        }
        public virtual Point[] GetFigure()
        {
            return _currentFigure;
        }
        public virtual int FigureHeight()
        {
            int maxY = 0;

            foreach (var p in _currentFigure)
                maxY = p.y + 1 > maxY ? p.y + 1 : maxY;

            return maxY;
        }
        public virtual int FigureWidht()
        {
            int maxX = 0;
            foreach (var p in _currentFigure)
                maxX = p.x + 1 > maxX ? p.x + 1 : maxX;

            return maxX;
        }
    }

    public class FigureO : Figure
    {
        public FigureO()
        {
            _figure = new Point[] { new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(1, 1) };
            Init();
        }

        public override void Rotate()
        {
        }

        public override void DeclineRotate()
        {
        }
    }

    public class FigureL : Figure
    {
        public FigureL()
        {
            _figure = new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(2, 0) };
            Init();
        }

        public override void Rotate()
        {
            SaveFigureState();
            _rotation = (_rotation + 1) % 4;

            switch (_rotation)
            {
                case 0:
                    _currentFigure = new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(2, 0) };
                    break;

                case 1:
                    _currentFigure = new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(1, 2) };
                    break;

                case 2:
                    _currentFigure = new Point[] { new Point(0, 1), new Point(0, 0), new Point(1, 0), new Point(2, 0) };
                    break;

                case 3:
                    _currentFigure = new Point[] { new Point(0, 0), new Point(0, 1), new Point(0, 2), new Point(1, 2) };
                    break;
            }
        }
    }

    public class FigureJ : Figure
    {
        public FigureJ()
        {
            _figure = new Point[] { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(2, 1) };
            Init();
        }

        public override void Rotate()
        {
            SaveFigureState();
            _rotation = (_rotation + 1) % 4;

            switch (_rotation)
            {
                case 0:
                    _currentFigure = new Point[] { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(2, 1) };
                    break;

                case 1:
                    _currentFigure = new Point[] { new Point(1, 0), new Point(0, 0), new Point(0, 1), new Point(0, 2) };
                    break;

                case 2:
                    _currentFigure = new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
                    break;

                case 3:
                    _currentFigure = new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(0, 2) };
                    break;
            }
        }
    }

    public class FigureS : Figure
    {
        public FigureS()
        {
            _figure = new Point[] { new Point(0, 1), new Point(1, 1), new Point(1, 0), new Point(2, 0) };
            Init();
        }

        public override void Rotate()
        {
            SaveFigureState();
            _rotation = (_rotation + 1) % 2;

            switch (_rotation)
            {
                case 0:
                    _currentFigure = new Point[] { new Point(0, 1), new Point(1, 1), new Point(1, 0), new Point(2, 0) };
                    break;

                case 1:
                    _currentFigure = new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(1, 2) };
                    break;
            }
        }
    }

    public class FigureZ : Figure
    {
        public FigureZ()
        {
            _figure = new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(2, 1) };
            Init();
        }

        public override void Rotate()
        {
            SaveFigureState();
            _rotation = (_rotation + 1) % 2;

            switch (_rotation)
            {
                case 0:
                    _currentFigure = new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(2, 1) };
                    break;

                case 1:
                    _currentFigure = new Point[] { new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(0, 2) };
                    break;
            }
        }
    }

    public class FigureT : Figure
    {
        public FigureT()
        {
            _figure = new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
            Init();
        }

        public override void Rotate()
        {
            SaveFigureState();
            _rotation = (_rotation + 1) % 4;

            switch (_rotation)
            {
                case 0:
                    _currentFigure = new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) };
                    break;

                case 1:
                    _currentFigure = new Point[] { new Point(0, 1), new Point(1, 0), new Point(1, 1), new Point(1, 2) };
                    break;

                case 2:
                    _currentFigure = new Point[] { new Point(1, 1), new Point(0, 0), new Point(1, 0), new Point(2, 0) };
                    break;

                case 3:
                    _currentFigure = new Point[] { new Point(1, 1), new Point(0, 0), new Point(0, 1), new Point(0, 2) };
                    break;
            }
        }
    }

    public class FigureI : Figure
    {
        public FigureI()
        {
            _figure = new Point[] { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) };
            Init();
        }

        public override void Rotate()
        {
            SaveFigureState();
            _rotation = (_rotation + 1) % 2;

            _currentFigure = new Point[_figure.Length];
            for (int i = 0; i < _figure.Length; i++)
                _currentFigure[i] = _rotation == 0 ? _figure[i] : new Point(_figure[i].y, _figure[i].x);
        }
    }
}
