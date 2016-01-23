using System;
using System.Collections.Generic;

namespace Tetris
{
    public class GameGlass : GlassRenderer
    {
        private Random _rand = new Random();
        private Type[] _figures = new Type[] { typeof(FigureI), typeof(FigureO), typeof(FigureL), typeof(FigureJ), typeof(FigureS), typeof(FigureZ), typeof(FigureT) };

        private Figure _figure;

        public GameGlass()
        {
            Init();
            InitFigure();
        }

        public void InitFigure()
        {
            _figure = (Figure) Activator.CreateInstance(_figures[_rand.Next(_figures.Length)]);
            _figure.position.x = (_width / 2) - (_figure.FigureWidht() / 2);
            _figure.color = _rand.Next(14) + 1;
        }

        public void PlaceFigure(bool erase)
        {
            foreach(var p in _figure.GetFigure())
                _field[_figure.position.x + p.x, _figure.position.y + p.y] = erase ? 0 : _figure.color;
        }

        private bool CanPlaceFigureToField(Point delta)
        {
            var result = true;
            var f = _figure.GetFigure();
            for (int i = 0; result && i < f.Length; i++)
            {
                var pX = _figure.position.x + f[i].x + delta.x;
                var pY = _figure.position.y + f[i].y + delta.y;

                result = pX < _width && pX >= 0 && pY < _height && pY >=0 && _field[pX, pY] == 0;
            }

            return result;
        }

        public bool Place()
        {
            InitFigure();
            var result = CanPlaceFigureToField(new Point(0, 0));
            if (result)
                PlaceFigure(false);

            return result;
        }

        public void Rotate()
        {
            PlaceFigure(true);
            _figure.Rotate();
            var result = CanPlaceFigureToField(new Point(0, 0));
            if (!result)
                _figure.DeclineRotate();

            PlaceFigure(false);
        }

        public bool Move(Point delta)
        {
            PlaceFigure(true);
            var result = CanPlaceFigureToField(delta);
            if (result)
            {
                _figure.position.x += delta.x;
                _figure.position.y += delta.y;
                PlaceFigure(false);
            }
            else
            {
                PlaceFigure(false);
            }

            return result;
        }

        public virtual void Init()
        {
            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                    _field[i, j] = 0;
        }

        public int Destroy()
        {
            var r = new List<int>();


            for (int j = 0; j < _height; j++)
            {
                var full = true;
                for (int i = 0; full && i < _width; i++)
                    full = _field[i, j] != 0;
                if (full)
                    r.Add(j);
            }

            foreach (var i in r)
            {
                for (int j = i; j >= 0; j--)
                    for (int k = 0; k < _width; k++)
                        _field[k, j] = j > 0 ? _field[k, j - 1] : 0;
            }

            return r.Count;
        }
    }
}
