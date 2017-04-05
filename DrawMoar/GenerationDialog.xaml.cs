using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DrawMoar {
    /// <summary>
    /// Логика взаимодействия для GenerationDialog.xaml
    /// </summary>
    public partial class GenerationDialog : Window {
        Point prevPoint;
        Point point;
        Point start;
        ILayer layer;
        IShape clickedShape;
        Rectangle newRect;
        Ellipse newEllipse;
        Line newLine;


        public GenerationDialog(ILayer layer) {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Activate();
            previewCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonDown);
            previewCanvas.MouseMove += new MouseEventHandler(previewCanvas_MouseMove);
            previewCanvas.MouseLeftButtonUp += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonUp);
            previewCanvas.MouseLeave += new MouseEventHandler(previewCanvas_MouseLeave);
            this.layer = layer;
            previewCanvas.Width = GlobalState.canvSize.Width;
            previewCanvas.Height = GlobalState.canvSize.Height;
            this.Width = GlobalState.canvSize.Width + 250;
            this.Height = GlobalState.canvSize.Height + 50;

            if (layer is VectorLayer) {
                ((VectorLayer)layer).Picture.Draw(previewCanvas);
            }
            //TODO: РАСТР...
        }

        private void previewCanvas_MouseLeftButtonDown(object sender, MouseEventArgs e) {
            GlobalState.PressLeftButton = true;
            prevPoint = Mouse.GetPosition(previewCanvas);
            start = prevPoint;
            if (layer is VectorLayer)
                clickedShape = GetClickedShape(prevPoint);
            previewCanvas_MouseMove(sender, e);
        }

        private void previewCanvas_MouseMove(object sender, MouseEventArgs e) {
            point = (Point)e.GetPosition(previewCanvas);
            if (!GlobalState.PressLeftButton) return;
            TranslatingRedrawing(clickedShape, e);
            prevPoint = point;
        }

        private void previewCanvas_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            GlobalState.PressLeftButton = false;
        }

        private void previewCanvas_MouseLeave(object sender, MouseEventArgs e) {
            GlobalState.PressLeftButton = false;
        }

        IShape GetClickedShape(Point clickPoint) {
            foreach (var item in ((VectorLayer)layer).Picture.shapes) {
                if (item is Rectangle) {
                    if (((Math.Abs(clickPoint.X - ((Rectangle)item).Center.X) < ((Rectangle)item).Size.Width / 4) &&    //вместо 20 потом придется скалировать пропорционально размерам фигуры
                        (Math.Abs(clickPoint.Y - ((Rectangle)item).Center.Y) < ((Rectangle)item).Size.Height / 4)))
                        return item;
                }
                else if (item is Ellipse) {
                    if (((Math.Abs(clickPoint.X - ((Ellipse)item).Center.X) < ((Ellipse)item).Size.Width / 4) &&
                        (Math.Abs(clickPoint.Y - ((Ellipse)item).Center.Y) < ((Ellipse)item).Size.Width / 4)))
                        return item;
                }
            }
            return null;
        }


        void TranslatingRedrawing(IShape shape, MouseEventArgs e) {           
            point = e.GetPosition(previewCanvas);
            if (clickedShape != null) {
                var shapes = ((VectorLayer)layer).Picture.shapes;              
                for(int i = 0; i < shapes.Count; i++) {
                    var item = shapes[i];
                    previewCanvas.Children.RemoveAt(shapes.IndexOf(item));
                    shapes.RemoveAt(shapes.IndexOf(item));
                    item.Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
                    item.Draw(previewCanvas);
                    SaveIntoLayer(layer, item);
                }
                TranslateVector.Text = $"( {(int)(point.X-start.X)} ; {(int)(point.Y-start.Y)} )";
                Refresh();
            }
        }

        private void Refresh() {
            previewCanvas.Children.Clear();
                foreach(IShape item in ((VectorLayer)layer).Picture.shapes) {
                    item.Draw(previewCanvas);
                }
                //TODO: РАСТР...
        }

        void SaveIntoLayer(ILayer layer, IShape shape) {
            if (layer is VectorLayer) {
                ((VectorLayer)layer).Picture.shapes.Add(shape);
            }
        }
    }
}
