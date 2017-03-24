using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;

using DrawMoar.Shapes;
using DrawMoar.BaseElements;


namespace DrawMoar {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private Cartoon cartoon;

        Point prevPoint;
        DrawMoar.Shapes.Line newLine;
        Point point;
        DrawMoar.Shapes.Ellipse newEllipse;
        DrawMoar.Shapes.Rectangle newRect;
        ILayer currentLayer;
        IShape clickedShape;

        public MainWindow() {
            InitializeComponent();
            // при создании окна области рисования нет

            // только после создания мультика в котором указывается размер холста в пропорциях
            // потом ещё размеры подогнать надо либо сам развернёт
            // панельки фиксированные и списки тоже

            // после создания мультика появляется первый пустой кадр
            // и на нём первый пустой слой

            // для теста, потом всё это можно будет поменять без проблем
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            canvas.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(canvas_MouseLeftButtonDown);
            canvas.PreviewMouseMove += new MouseEventHandler(canvas_MouseMove);
            canvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(canvas_MouseLeftButtonUp);
            canvas.MouseLeave += new MouseEventHandler(canvas_MouseLeave);
            GlobalState.CurrentTool = Instrument.Arrow;
            GlobalState.ChangeInstrument += SetCursorStyle;
            GlobalState.Color = Brushes.Black;
            GlobalState.BrushSize = new Size(5, 5);
        }

        //тогда отрисовку придется выносить в отдельный класс и она будет сложнее - много работы в плане 

        private void CreateCartoon(object sender, RoutedEventArgs e) {
            // создаём новый пустой кадр
            // на кадре новый пустой слой создаём
            var newCartoonDialog = new CreateCartoonDialog();
            newCartoonDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newCartoonDialog.Owner = this;
            newCartoonDialog.Show();

        }


        /// <summary>
        /// "Успешное" закрытие окна создания мультфильма.
        /// Выполняется при нажатии кнопки Create (Создать)
        /// </summary>
        /// <param name="cartoon"></param>
        public void Success(Cartoon cartoon) {
            canvas.Visibility = Visibility.Visible;
            canvas.Width = cartoon.Width;
            canvas.Height = cartoon.Height;
            this.cartoon = cartoon;
            GlobalState.canvSize = new Size(canvas.Width, canvas.Height);
            Height = canvas.Height;
            Width = canvas.Width + 260;        //пока что так (ширина двух крайних колонок грида)
            AddScene_Click(null, null);
            AddFrame_Click(null, null);
            AddRasterLayer_Click(null, null);
        }


        private void ExportToMP4(object sender, RoutedEventArgs e) {
            //SaveControlsToBitmap();
            //var exp = new Mp4Exporter(); 
            //exp.Save(cartoon, cartoon.WorkingDirectory); 
        }


        //// https://github.com/artesdi/Paint.WPF
        //public void DeleteFrame(object sender, RoutedEventArgs e) {

        //    //cartoon.frames.Remove(cartoon.currentFrame);
        //    // удаление из списка кадров на экране
        //    // canvas.Strokes.Clear();
        //    // переключение и отображение предыдущего/следующего кадра
        //}

        ///// <summary>
        ///// Смена рабочего цвета на выбранный в основной палитре
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedEventArgs e)
        //{
        //    GlobalState.Color = new SolidColorBrush(ClrPcker_Background.SelectedColor.Value);
        //}

        /// <summary>
        /// Добавление нового кадра в мультфильм.
        /// Подразумевает добавление одного слоя на новый кадр.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRasterLayer_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            //проверка на то, выделен ли какой-либо кадр(когда реализуем удаление)
            if (sender == null) layersList.Items.Clear();
            else cartoon.CurrentScene.CurrentFrame.AddEmptyRasterLayer();
            var layers = cartoon.CurrentScene.CurrentFrame.GetAllLayers();
            AddListBoxElement(layersList, $"RasterLayer_{layers.Count - 1}");
            canvas.Children.Clear();
        }


        private void AddVectorLayer_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender == null) layersList.Items.Clear();
            else cartoon.CurrentScene.CurrentFrame.AddEmptyVectorLayer();
            var layers = cartoon.CurrentScene.CurrentFrame.GetAllLayers();
            AddListBoxElement(layersList, $"VectorLayer_{layers.Count - 1}");
            canvas.Children.Clear();
        }


        /// <summary>
        ///     Изменение курсора мыши в зависимости от выбранного инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCursorStyle(Object sender, EventArgs e) {
            switch (GlobalState.CurrentTool) {
                case Instrument.Brush:
                    canvas.Cursor = Cursors.Cross;
                    break;
                default:
                    canvas.Cursor = Cursors.Arrow;
                    break;
            }
        }


        /// <summary>
        /// Обработка выбора элемента из элемента, отображающего кадры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            layersList.Items.Clear();
            canvas.Children.Clear();
            if (framesList.SelectedIndex != -1)
                cartoon.CurrentScene.CurrentFrame = cartoon.CurrentScene.GetAllFrames()[framesList.SelectedIndex];
            var lays = cartoon.CurrentScene.CurrentFrame.GetAllLayers();
            foreach (var item in lays) {
                AddListBoxElement(layersList, item.Name);
            }
            cartoon.CurrentScene.CurrentFrame = cartoon.CurrentScene.GetAllFrames()[framesList.SelectedIndex];
            if (lays[0] is VectorLayer) {
                for (int i = 0; i < ((VectorLayer)lays[0]).Picture.shapes.Count; i++) {
                    ((VectorLayer)lays[0]).Picture.shapes[i].Draw(canvas);
                }
            }
            layersList.SelectedIndex = 0;
        }


        private void scenesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            framesList.Items.Clear();
            cartoon.CurrentScene = cartoon.GetAllScenes()[scenesList.SelectedIndex];
            var frames = cartoon.CurrentScene.GetAllFrames();
            foreach (var item in frames) {
                AddListBoxElement(framesList, item.Name);
            }
        }


        private void layersList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (layersList.SelectedIndex != -1) {
                var layer = cartoon.CurrentScene.CurrentFrame.GetAllLayers()[layersList.SelectedIndex];
                if (currentLayer is RasterLayer)
                    ((RasterLayer)currentLayer).Save(canvas);
                cartoon.CurrentScene.CurrentFrame.CurrentLayer = layer;
                canvas.Children.Clear();
                if (layer is VectorLayer)
                    foreach (var item in ((VectorLayer)layer).Picture.shapes) {
                        item.Draw(canvas);
                    }
                else {
                    var img = ((RasterLayer)layer).Picture.Image;
                    if (img!=null)
                        canvas.Children.Add(((RasterLayer)layer).ConvertDrawingImageToWPFImage(img));
                }
            }

        }


        private void testButton_Click(object sender, RoutedEventArgs e) {
            GlobalState.CurrentTool = Instrument.Brush;
        }


        private void testButton2_Click(object sender, RoutedEventArgs e) {
            GlobalState.CurrentTool = Instrument.Arrow;
        }


        private void AddFrame_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                cartoon.CurrentScene.AddEmptyFrame();
                var frames = cartoon.CurrentScene.GetAllFrames();
                AddListBoxElement(framesList, $"Frame_{frames.Count - 1}");
            }
        }


        private void AddScene_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                cartoon.AddEmptyScene();
                cartoon.CurrentScene.AddEmptyFrame();
                //cartoon.CurrentScene.CurrentFrame = cartoon.CurrentScene.GetAllFrames()[0];
            }
            AddListBoxElement(scenesList, cartoon.CurrentScene.Name);
        }


        private void AddListBoxElement(ListBox lBox, string content) {
            var lbl = new Label();          //здесь должен быть какой-то другой контрол (возможно, самописный)
            lbl.Content = content;
            lBox.Items.Add(lbl);
            lBox.SelectedIndex = lBox.Items.Count - 1;
        }


        void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            GlobalState.PressLeftButton = true;
            currentLayer = cartoon.CurrentScene.CurrentFrame.CurrentLayer;
            prevPoint = Mouse.GetPosition(canvas);
            switch (GlobalState.CurrentTool) {
                case Instrument.Arrow:
                    if (currentLayer is VectorLayer)
                        clickedShape = GetClickedShape(prevPoint);
                    break;
                case Instrument.Brush:
                    break;
                case Instrument.Rectangle:
                    newRect = new Rectangle(prevPoint, new Size(15, 10));
                    newRect.Draw(canvas);
                    SaveIntoLayer(currentLayer, newRect);
                    break;
                case Instrument.Ellipse:
                    newEllipse = new Ellipse(prevPoint, new Size(15, 10));
                    newEllipse.Draw(canvas);
                    SaveIntoLayer(currentLayer, newEllipse);
                    break;
                case Instrument.Line:
                    //задерживание кнопки мыши, отпустили = конец линии    
                    newLine = new Line(prevPoint, prevPoint);
                    newLine.Draw(canvas);
                    SaveIntoLayer(currentLayer, newLine);
                    break;
            }
            canvas_MouseMove(sender, e);
        }


        void canvas_MouseMove(object sender, MouseEventArgs e) {
            point = (Point)e.GetPosition(canvas);
            currentLayer = cartoon.CurrentScene.CurrentFrame.CurrentLayer;
            if (!GlobalState.PressLeftButton) return;
            switch (GlobalState.CurrentTool) {
                case Instrument.Arrow:
                    TranslatingRedrawing(clickedShape, e);
                    prevPoint = point;
                    break;
                case Instrument.Brush:
                    newLine = new Line(prevPoint, point);
                    newLine.Draw(canvas);
                    prevPoint = point;
                    break;
                case Instrument.Rectangle:
                    ScaleRedrawing(newRect, e);
                    break;
                case Instrument.Ellipse:
                    ScaleRedrawing(newEllipse, e);
                    break;
                case Instrument.Line:
                    ScaleRedrawing(newLine, e);
                    break;
            }
        }


        void ScaleRedrawing(IShape shape, MouseEventArgs e) {
            if (shape != null & e.LeftButton == MouseButtonState.Pressed) {
                var layer = cartoon.CurrentScene.CurrentFrame.CurrentLayer;
                var shiftX = point.X - prevPoint.X;
                var shiftY = point.Y - prevPoint.Y;
                if (((shiftX <= 0) || (shiftY <= 0)) && (!(shape is Line))) return;   //пока из-за "плохих" шифтов так; уберу, когда сделаю зеркалирование
                canvas.Children.RemoveAt(canvas.Children.Count - 1);

                if (shape is Line) shape = new Line(prevPoint, point);
                else if (shape is Ellipse) shape = new Ellipse(prevPoint, new Size(15 + shiftX, 10 + shiftY));
                else if (shape is Rectangle) shape = new Rectangle(prevPoint, new Size(15 + shiftX, 10 + shiftY));
                shape.Draw(canvas);

                if (layer is VectorLayer) {
                    var shapes = ((VectorLayer)layer).Picture.shapes;
                    shapes.RemoveAt(shapes.Count - 1);
                    SaveIntoLayer(layer, shape);
                }
               // else ((RasterLayer)currentLayer).Save(canvas);
            }
        }


        IShape GetClickedShape(Point clickPoint) {
            var layer = (VectorLayer)cartoon.CurrentScene.CurrentFrame.CurrentLayer;
            foreach (var item in layer.Picture.shapes) {
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
            point = e.GetPosition(canvas);
            if (clickedShape != null) {
                var shapes = ((VectorLayer)currentLayer).Picture.shapes;
                canvas.Children.RemoveAt(shapes.IndexOf(shape));
                shapes.RemoveAt(shapes.IndexOf(shape));              
                shape.Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
                shape.Draw(canvas);
                SaveIntoLayer(currentLayer, shape);             
            }
        }


        void SaveIntoLayer(ILayer layer, IShape shape) {
            if (layer is VectorLayer) {
                ((VectorLayer)layer).Picture.shapes.Add(shape);
            }
        }


        void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            GlobalState.PressLeftButton = false;
        }


        void canvas_MouseLeave(object sender, MouseEventArgs e) {
            GlobalState.PressLeftButton = false;
        }


        private void Lines_Click(object sender, RoutedEventArgs e) {
            GlobalState.CurrentTool = Instrument.Line;
        }


        private void AddEllipse_Click(object sender, RoutedEventArgs e) {
            GlobalState.CurrentTool = Instrument.Ellipse;
        }


        private void AddRectangle_Click(object sender, RoutedEventArgs e) {
            GlobalState.CurrentTool = Instrument.Rectangle;
        }


        private void AddPicture(object sender, RoutedEventArgs e) {
            // TODO: ТУТ ИМПОРТ, который вернул нам Image и мы делаем:
            // Cartoon.CurrentScene.CurrentFrame.AddNewRasterLayer(возвращенная Image из импорта)
            // Ну и это как-то отображается наверное, что-то меняется в общем.
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            menu.Width = Width;
        }


        //private void StartLightVector(object sender, RoutedEventArgs e) {
        //    if (cartoon != null) {
        //        GlobalState.lightVector = new Instruments.LightVector(cartoon);
        //        GlobalState.lightVector.active = true;
        //        var drawingControl = new LayerControl();
        //        drawingControl.Focus();
        //        GlobalState.CurrentTool = Instrument.Light;

        //        if (cartoon.CurrentScene.currentFrame.CurrentLayer.GetType().Name != "LightVectorLayer") {
        //            var layer = new LightVectorLayer();
        //            layer.Name = $"LIGHTlayer_{layersList.Items.Count}";
        //            layer.drawingControl = drawingControl;
        //            cartoon.CurrentScene.currentFrame.AddLayer(layer);
        //            string text = $"LIGHTlayer_{layersList.Items.Count}";
        //            AddListBoxElement(layersList, text);
        //        }
        //        canvas.Children.Add(drawingControl);

        //    }
        //}
    }
}
