using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DrawMoar {
    public partial class MainWindow : Window {

        void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            SavePrev();
            PressLeftButton = true;
            BrushSize = new Size(slider.Value, slider.Value);
            var currentLayer = Editor.cartoon.CurrentLayer;           
            prevPoint = Mouse.GetPosition(canvas);
            if (currentLayer is RasterLayer) return;
            switch (CurrentTool) {
                case Instrument.Arrow:
                    break;
                case Instrument.Brush:
                    break;
                case Instrument.Rectangle:
                    newRect = new Rectangle(prevPoint, new Size(15, 10));
                    newRect.Draw(canvasDrawer);
                    SaveIntoLayer(currentLayer, newRect);
                    break;
                case Instrument.Ellipse:
                    newEllipse = new Ellipse(prevPoint, new Size(15, 10));
                    newEllipse.Draw(canvasDrawer);
                    SaveIntoLayer(currentLayer, newEllipse);
                    break;
                case Instrument.Line:
                    newLine = new Line(prevPoint, prevPoint);
                    newLine.Draw(canvasDrawer);
                    SaveIntoLayer(currentLayer, newLine);
                    break;
                case Instrument.Eraser:
                    break;
            }
            canvas_MouseMove(sender, e);
        }


        void canvas_MouseMove(object sender, MouseEventArgs e) {
            point = (Point)e.GetPosition(canvas);
            var currentLayer = Editor.cartoon.CurrentLayer;          
            if (!PressLeftButton) return;
            switch (CurrentTool) {
                case Instrument.Arrow:
                    TranslatingRedrawing(e);
                    prevPoint = point;
                    break;
                case Instrument.Brush:
                    if (currentLayer is RasterLayer) return;
                    newLine = new Line(prevPoint, point);
                    newLine.Draw(canvasDrawer);
                    SaveIntoLayer(currentLayer, newLine);
                    prevPoint = point;
                    break;
                case Instrument.Rectangle:
                    if (currentLayer is RasterLayer) return;
                    ScaleRedrawing(newRect, e);
                    break;
                case Instrument.Ellipse:
                    if (currentLayer is RasterLayer) return;
                    ScaleRedrawing(newEllipse, e);
                    break;
                case Instrument.Line:
                    if (currentLayer is RasterLayer) return;
                    ScaleRedrawing(newLine, e);
                    break;
                case Instrument.Eraser:
                    if (currentLayer is RasterLayer) return;
                    var bufColor = ClrPcker_Background.SelectedColor.Value;
                    ClrPcker_Background.SelectedColor = Colors.Transparent;
                    newLine = new Line(prevPoint, point);
                    newLine.Draw(canvasDrawer);
                    SaveIntoLayer(currentLayer, newLine);
                    prevPoint = point;
                    ClrPcker_Background.SelectedColor = bufColor;
                    break;
            }
        }


        void ScaleRedrawing(IShape shape, MouseEventArgs e) {
            if (shape != null & e.LeftButton == MouseButtonState.Pressed) {
                var layer = Editor.cartoon.CurrentLayer;
                var shiftX = point.X - prevPoint.X;
                var shiftY = point.Y - prevPoint.Y;
                if (((shiftX <= 0) || (shiftY <= 0)) && (!(shape is Line))) return;   //пока из-за "плохих" шифтов так; уберу, когда сделаю зеркалирование
                canvas.Children.RemoveAt(canvas.Children.Count - 1);

                if (shape is Line) shape = new Line(prevPoint, point);
                else if (shape is Ellipse) shape = new Ellipse(prevPoint, new Size(15 + shiftX, 10 + shiftY));
                else if (shape is Rectangle) shape = new Rectangle(prevPoint, new Size(15 + shiftX, 10 + shiftY));
                shape.Draw(canvasDrawer);
                if (layer is VectorLayer) {
                    var shapes = ((VectorLayer)layer).Picture.shapes;
                    shapes.RemoveAt(shapes.Count - 1);
                    SaveIntoLayer(layer, shape);
                }
                //else ((RasterLayer)layer.Item1).Save(canvas);
            }
        }


        void TranslatingRedrawing(MouseEventArgs e) {
            point = e.GetPosition(canvas);
            Editor.cartoon.CurrentLayer.Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
            Refresh();
        }



        void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            PressLeftButton = false;
        }

        void canvas_MouseLeave(object sender, MouseEventArgs e) {
            PressLeftButton = false;
        }
    }
}
