using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFGUI
{
    class Draw : FrameworkElement
    {
        public new bool IsFocused { get; set; }

        // Коллекция для хранения DrawingVisual
        private readonly VisualCollection _visuals;

        //Свойства для хранения состояния о корневом экземпляре коллекции _visuals
        private Brush FillBrush { get; set; }
        private Point Position { get; set; }
        public Size SpaceSize { get; private set; }

        public Draw() {
            _visuals = new VisualCollection(this);
            _visuals.Add(ClearVisualSpace());

            this.MouseLeftButtonUp += new MouseButtonEventHandler(Draw_MouseLeftButtonUp);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Draw_MouseLeftButtonDown);
            this.MouseMove += new MouseEventHandler(VisualHost_MouseMove);
        }

        /// <summary>
        /// Создание корневого элемента VisualCollection
        /// </summary>
        /// <param name="borderBrush">Цвет границ</param>
        /// <param name="backgroundBrush">Заливка</param>
        /// <param name="position">Начальное положение холста</param>
        /// <param name="size">Размер холста</param>
        /// <returns></returns>
        private DrawingVisual CreateDrawingVisualSpace(Brush borderBrush, Brush backgroundBrush, Point position, Size size) {
            FillBrush = backgroundBrush;
            Position = position;
            SpaceSize = size;

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen()) {
                Rect rect = new Rect(Position, SpaceSize);
                Pen pen = new Pen(borderBrush, 1);

                drawingContext.DrawRectangle(FillBrush, pen, rect);
            }

            return drawingVisual;
        }

        private DrawingVisual ClearVisualSpace() {
            return CreateDrawingVisualSpace(Brushes.Silver, Brushes.Transparent, new Point(0, 0), new Size(300, 300));
        }

        public void FocusSpace() {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.DimGray, FillBrush, Position, SpaceSize);
        }

        public void UnFocusSpace() {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.Silver, FillBrush, Position, SpaceSize);
        }

        public void ChangeFill(Brush backgroundBrush) {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.DimGray, backgroundBrush, Position, SpaceSize);
        }

        public void ChangeSize(Size newSize) {
            _visuals[0] = null;
            _visuals[0] = CreateDrawingVisualSpace(Brushes.DimGray, FillBrush, Position, newSize);
        }

        public void HideWorkSpace() {
            _visuals[0] = null;
        }

        public void RestoreWorkSpace() {
            if (_visuals[0] == null)
                _visuals[0] = CreateDrawingVisualSpace(Brushes.Silver, FillBrush, Position, SpaceSize);
        }

        /// <summary>
        /// Определения состояния для начального рисования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Draw_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            GlobalState.PressLeftButton = true;
            VisualHost_MouseMove(sender, e);
        }

        /// <summary>
        /// Окончание рисования и определение конечных координат инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Draw_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            GlobalState.PressLeftButton = false;
            Point pt = e.GetPosition((UIElement)sender);
        }


        /// <summary>
        /// Метода для рисования точек кистью
        /// </summary>
        /// <param name="pt"></param>
        private void DrawPoint(Point pt) {
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen()) {
                Rect rect = new Rect(pt, GlobalState.BrushSize);
                drawingContext.DrawRoundedRectangle(GlobalState.CurrentColor, null, rect, GlobalState.BrushSize.Width, GlobalState.BrushSize.Height);
            }
            _visuals.Add(drawingVisual);
        }

        /// <summary>
        /// Событие для определения координат рисования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void VisualHost_MouseMove(object sender, MouseEventArgs e) {
            // тут проверка на инструмент наверное и что-то такое в общем
            if (GlobalState.PressLeftButton && this.IsFocused) {
                Point pt = e.GetPosition((UIElement)sender);
                DrawPoint(pt);
            }

        }

        protected override int VisualChildrenCount {
            get { return _visuals.Count; }
        }

        protected override Visual GetVisualChild(int index) {
            if (index < 0 || index >= _visuals.Count) {
                throw new ArgumentOutOfRangeException();
            }

            return _visuals[index];
        }



    }


}

