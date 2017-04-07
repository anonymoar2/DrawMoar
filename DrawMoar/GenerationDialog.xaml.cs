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


        public GenerationDialog(ILayer layer, Size size) {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Activate();
            previewCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonDown);
            previewCanvas.MouseMove += new MouseEventHandler(previewCanvas_MouseMove);
            previewCanvas.MouseLeftButtonUp += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonUp);
            previewCanvas.MouseLeave += new MouseEventHandler(previewCanvas_MouseLeave);
            this.layer = layer;
            this.Width = size.Width + 250;
            this.Height = size.Height + 50;
            previewCanvas.Width = size.Width;
            previewCanvas.Height = size.Height;
            previewCanvas.VerticalAlignment = VerticalAlignment.Center;
            this.Background = Brushes.LightGray;
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
                var index = shapes.IndexOf(shape);
                previewCanvas.Children.RemoveAt(index);
                shapes.RemoveAt(shapes.IndexOf(shape));
                shape.Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
                shape.Draw(previewCanvas);
                SaveIntoLayer(layer, shape);
                TranslateStart.Text = start.ToString();
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

        private void ApplyTransform_Click(object sender, RoutedEventArgs e) {

        }
    }
}
