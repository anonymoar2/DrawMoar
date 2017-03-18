using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

namespace DrawMoar
{
    /// <summary>
    /// Класс реализующий DrawingVisual и отвечающий за рисование
    /// </summary>
    public class VisualHost : FrameworkElement
    {
        public new bool IsFocused { get; set; }

        // Коллекция для хранения DrawingVisual
        private VisualCollection _visuals;

        public VisualCollection GetVisuals()
        {
            return _visuals;
        }

        private List<Point> clickPoints = new List<Point>();

        private Point prevPt;

        private int draggingPoint = -1;
        //Свойства для хранения состояния о корневом экземпляре коллекции _visuals
        private Brush FillBrush { get; set; }
        private Point Position { get; set; }
        public Size SpaceSize { get; private set; }

        public VisualHost()
        {
            _visuals = new VisualCollection(this);
            _visuals.Add(ClearVisualSpace());

            this.MouseLeftButtonUp += new MouseButtonEventHandler(VisualHost_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(VisualHost_MouseLeftButtonDown);
            this.MouseMove += new MouseEventHandler(VisualHost_MouseMove);
            this.MouseRightButtonDown += new MouseButtonEventHandler(VisualHost_RightButtonDown);
        }

        /// <summary>
        /// Создание корневого элемента VisualCollection
        /// </summary>
        /// <param name="borderBrush">Цвет границ</param>
        /// <param name="backgroundBrush">Заливка</param>
        /// <param name="position">Начальное положение холста</param>
        /// <param name="size">Размер холста</param>
        /// <returns></returns>
        private DrawingVisual CreateDrawingVisualSpace(Brush borderBrush, Brush backgroundBrush, Point position, Size size)
        {
            FillBrush = backgroundBrush;
            Position = position;
            SpaceSize = size;

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Rect rect = new Rect(Position, SpaceSize);
                Pen pen = new Pen(borderBrush, 1);

                drawingContext.DrawRectangle(FillBrush, pen, rect);
            }

            return drawingVisual;
        }


        private DrawingVisual ClearVisualSpace()
        {
            return CreateDrawingVisualSpace(Brushes.Silver, Brushes.Transparent, new Point(0, 0), GlobalState.canvSize);
        }
        #region работа c _visuals
        public void FocusSpace()
        {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.DimGray, FillBrush, Position, SpaceSize);
        }

        public void UnFocusSpace()
        {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.Silver, FillBrush, Position, SpaceSize);
        }

        public void ChangeFill(Brush backgroundBrush)
        {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.DimGray, backgroundBrush, Position, SpaceSize);
        }

        public void ChangeSize(Size newSize)
        {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.DimGray, FillBrush, Position, newSize);
        }

        public void HideWorkSpace()
        {
            _visuals[0] = null;
        }

        public void RestoreWorkSpace()
        {
            if (_visuals[0] == null)
                _visuals[0] = CreateDrawingVisualSpace(Brushes.Silver, FillBrush, Position, SpaceSize);
        }
        #endregion

        /// <summary>
        /// Определения состояния для начального рисования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VisualHost_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GlobalState.PressLeftButton = true;
            if (GlobalState.CurrentTool == Instrument.Light)
            {
                prevPt = e.GetPosition((UIElement)sender);
                clickPoints.Add(prevPt);
                DrawPoint(prevPt);
            }
            if (GlobalState.CurrentTool == Instrument.Arrow)
            {
                Point pt;
                for (int i = 0; i < clickPoints.Count; i++)
                {
                    pt = clickPoints[i];
                    if ((Math.Abs(pt.X - e.GetPosition((UIElement)sender).X )< 15) &&
                       (Math.Abs(pt.Y - e.GetPosition((UIElement)sender).Y) < 15))
                    {
                        draggingPoint = i;
                    }
                }
            }
            VisualHost_MouseMove(sender, e);
        }


        /// <summary>
        /// Окончание рисования и определение конечных координат инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VisualHost_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (GlobalState.CurrentTool != Instrument.Light) GlobalState.PressLeftButton = false;
            draggingPoint = -1;

        }


        void VisualHost_RightButtonDown(object sender, MouseButtonEventArgs e)
        {
            GlobalState.PressRightButton = true;
            var count = _visuals.Count;
            _visuals.RemoveAt(count - 1);
            GlobalState.PressLeftButton = false;
        }


        private void DrawLine(Point newPt)
        {

            //if (GlobalState.lightVector.active) {

            //if (!GlobalState.lightVector.IsEmpty()) {

            if (!GlobalState.PressLeftButton) return;
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                var count = _visuals.Count;
                _visuals.RemoveAt(count - 1);
                drawingContext.DrawLine(new Pen(Brushes.Black, GlobalState.BrushSize.Width), prevPt, newPt);
                _visuals.Add(drawingVisual);
                //}
                //}
            }
        }


        /// <summary>
        /// Метода для рисования точек кистью
        /// </summary>
        /// <param name="pt"></param>
        private void DrawPoint(Point pt)
        {
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawEllipse(new SolidColorBrush(Colors.Black), null, pt, GlobalState.BrushSize.Width, GlobalState.BrushSize.Height);
            }
            _visuals.Add(drawingVisual);
        }

        private void DragPoint(Point pt)
        {
            if (!GlobalState.PressLeftButton) return;
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                var count = _visuals.Count;
                _visuals.RemoveAt(count - 1);
                drawingContext.DrawEllipse(new SolidColorBrush(Colors.Black), null, pt, GlobalState.BrushSize.Width, GlobalState.BrushSize.Height);
                _visuals.Add(drawingVisual);
            }
        }

        private void ConnectAllPoints()
        {
            for (int i = 0; i < clickPoints.Count - 1; i++)
            {
                var drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawLine(new Pen(Brushes.Black, GlobalState.BrushSize.Width), clickPoints[i], clickPoints[i + 1]);
                    _visuals.Add(drawingVisual);
                }
            }
        }


        /// <summary>
        /// Событие для определения координат рисования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VisualHost_MouseMove(object sender, MouseEventArgs e)
        {
            switch (GlobalState.CurrentTool)
            {
                case Instrument.Light:
                    DrawLine(e.GetPosition((UIElement)sender));
                    break;

                case Instrument.Brush:

                    if (GlobalState.PressLeftButton)
                    {
                        Point pt = e.GetPosition((UIElement)sender);

                        if (pt.X >= (Position.X + GlobalState.BrushSize.Width / 2)
                            && pt.Y >= (Position.Y + GlobalState.BrushSize.Height / 2)
                            && pt.X <= (Position.X + SpaceSize.Width - GlobalState.BrushSize.Width / 2)
                            && pt.Y <= (Position.Y + SpaceSize.Height - GlobalState.BrushSize.Height / 2))
                        {

                            DrawPoint(pt);
                        }
                    }
                    break;

                case Instrument.Arrow:
                    if (!GlobalState.PressLeftButton) return;
                    if (draggingPoint != -1)
                    {
                        DragPoint(e.GetPosition((UIElement)sender));
                        clickPoints[draggingPoint] = e.GetPosition((UIElement)sender);
                        _visuals.Clear();
                        ConnectAllPoints();
                    }
                    break;
                //case Instrument.Light:

                //    // Скорее всего делаю это неправильно, по сути нужно просто клик отслеживать и его координаты
                //    if (GlobalState.PressLeftButton) {
                //        Point pt = e.GetPosition((UIElement)sender);

                //        if (pt.X >= (Position.X + GlobalState.BrushSize.Width / 2)
                //            && pt.Y >= (Position.Y + GlobalState.BrushSize.Height / 2)
                //            && pt.X <= (Position.X + SpaceSize.Width - GlobalState.BrushSize.Width / 2)
                //            && pt.Y <= (Position.Y + SpaceSize.Height - GlobalState.BrushSize.Height / 2)) {

                //            if (!GlobalState.lightVector.IsEmpty()) {
                //                DrawLine(pt);
                //            }
                //            GlobalState.lightVector.Push(pt);
                //        }
                //    }
                //    break;
                default: break;
            }
        }


        protected override int VisualChildrenCount
        {
            get { return _visuals.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _visuals.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _visuals[index];
        }
    }
}
