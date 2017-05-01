using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using DrawMoar.Shapes;
using DrawMoar.BaseElements;
using System.Linq;

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
        Point translation;
        GenerationDialog generationWin;

        public MainWindow() {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            canvas.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(canvas_MouseLeftButtonDown);
            canvas.PreviewMouseMove += new MouseEventHandler(canvas_MouseMove);
            canvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(canvas_MouseLeftButtonUp);
            canvas.MouseLeave += new MouseEventHandler(canvas_MouseLeave);
            GlobalState.ChangeInstrument += SetCursorStyle;
            GlobalState.CurrentTool = Instrument.Arrow;
            GlobalState.Color = Brushes.Black;
            GlobalState.BrushSize = new Size(5, 5);
        }

        private void CreateCartoon(object sender, RoutedEventArgs e) {
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
            this.Activate();
            GlobalState.canvSize = new Size(canvas.Width, canvas.Height);
            Height = canvas.Height;
            Width = canvas.Width + 260;        //пока что так (ширина двух крайних колонок грида)
            AddScene_Click(null, null);
            AddFrame_Click(null, null);
            AddVectorLayer_Click(null, null);
        }


        private void ExportToMP4(object sender, RoutedEventArgs e) {        
        }

        private void AddRasterLayer_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            //проверка на то, выделен ли какой-либо кадр(когда реализуем удаление)
            if (sender != null)
            {
                GlobalState.CurrentFrame.layers.Add(new Tuple<ILayer, List<Transformation>, int>(new RasterLayer(), new List<Transformation>(), 0));
            }
            GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
            var layers = GlobalState.CurrentFrame.layers;
            AddListBoxElement(layersList, $"RasterLayer_{layers.Count - 1}");
        }

        private void AddVectorLayer_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender == null) {
                layersList.Items.Clear();
            }
            else {
                GlobalState.CurrentFrame.layers.Add(new Tuple<ILayer, List<Transformation>, int>(new VectorLayer(), new List<Transformation>(), 0));
            }
            GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
            var layers = GlobalState.CurrentFrame.layers;
            AddListBoxElement(layersList, $"VectorLayer_{layers.Count - 1}");
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

        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (framesList.SelectedIndex != -1) {
                if (GlobalState.CurrentFrame.layers.Count > 0)
                GlobalState.CurrentFrame = GlobalState.CurrentScene.frames[framesList.SelectedIndex];
                GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
            }
            layersList.Items.Clear();
            canvas.Children.Clear();
            var lays = GlobalState.CurrentFrame.layers;
            foreach (var item in lays) {
                AddListBoxElement(layersList, item.Item1.Name);
                item.Item1.Print(canvas);
                layersList.SelectedIndex = 0;
            }
        }

        private void scenesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            framesList.Items.Clear();
            GlobalState.CurrentScene = cartoon.scenes[scenesList.SelectedIndex];
            var frames = GlobalState.CurrentScene.frames;
            foreach (var item in frames) {
                AddListBoxElement(framesList, item.Name);
            }
            framesList.SelectedIndex = 0;
        }

        private void layersList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (layersList.SelectedIndex != -1) {
                GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers[layersList.SelectedIndex];
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
                GlobalState.CurrentScene.frames.Add(new BaseElements.Frame());
                GlobalState.CurrentFrame = GlobalState.CurrentScene.frames.Last();
                GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
                var frames = GlobalState.CurrentScene.frames;
                AddListBoxElement(framesList, GlobalState.CurrentFrame.Name);
            }
        }

        private void AddScene_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                cartoon.scenes.Add(new Scene());
            }
            GlobalState.CurrentScene = cartoon.scenes.Last();
            GlobalState.CurrentFrame = GlobalState.CurrentScene.frames.Last();
            GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
            AddListBoxElement(scenesList, GlobalState.CurrentScene.Name);
        }


        private void AddListBoxElement(ListBox lBox, string content) {
            var lbl = new Label();          
            lbl.Content = content;
            lBox.Items.Add(lbl);
            lBox.SelectedIndex = lBox.Items.Count - 1;
        }

        void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            GlobalState.PressLeftButton = true;
            var currentLayer = GlobalState.CurrentLayer.Item1;
            prevPoint = Mouse.GetPosition(canvas);
            switch (GlobalState.CurrentTool) {
                case Instrument.Arrow:
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
                    newLine = new Line(prevPoint, prevPoint);
                    newLine.Draw(canvas);
                    SaveIntoLayer(currentLayer, newLine);
                    break;
            }
            canvas_MouseMove(sender, e);
        }

        void canvas_MouseMove(object sender, MouseEventArgs e) {
            point = (Point)e.GetPosition(canvas);
            var currentLayer = GlobalState.CurrentLayer.Item1;
            if (!GlobalState.PressLeftButton) return;
            switch (GlobalState.CurrentTool) {
                case Instrument.Arrow:
                    TranslatingRedrawing(e);
                    prevPoint = point;
                    break;
                case Instrument.Brush:
                    newLine = new Line(prevPoint, point);
                    newLine.Draw(canvas);
                    SaveIntoLayer(currentLayer, newLine);                   
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
                var layer = GlobalState.CurrentLayer;
                var shiftX = point.X - prevPoint.X;
                var shiftY = point.Y - prevPoint.Y;
                if (((shiftX <= 0) || (shiftY <= 0)) && (!(shape is Line))) return;   //пока из-за "плохих" шифтов так; уберу, когда сделаю зеркалирование
                canvas.Children.RemoveAt(canvas.Children.Count - 1);

                if (shape is Line) shape = new Line(prevPoint, point);
                else if (shape is Ellipse) shape = new Ellipse(prevPoint, new Size(15 + shiftX, 10 + shiftY));
                else if (shape is Rectangle) shape = new Rectangle(prevPoint, new Size(15 + shiftX, 10 + shiftY));
                shape.Draw(canvas);

                if (layer.Item1 is VectorLayer) {
                    var shapes = ((VectorLayer)layer.Item1).Picture.shapes;
                    shapes.RemoveAt(shapes.Count - 1);
                    SaveIntoLayer(layer.Item1, shape);
                }
                else ((RasterLayer)layer.Item1).Save(canvas);
            }
        }

        void TranslatingRedrawing(MouseEventArgs e) {
            point = e.GetPosition(canvas);
            translation.X += point.X - prevPoint.X;
            translation.Y += point.Y - prevPoint.Y;
            if (GlobalState.CurrentLayer.Item1 is VectorLayer)
                ((VectorLayer)GlobalState.CurrentLayer.Item1).Picture.Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
            else {
                ((RasterLayer)GlobalState.CurrentLayer.Item1).Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
            }
            Refresh();
        }

        void SaveIntoLayer(ILayer layer, IShape shape) {
            if (layer is VectorLayer) {
                ((VectorLayer)layer).Picture.shapes.Add(shape);
            }
        }

        void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            GlobalState.PressLeftButton = false;
            if (GlobalState.CurrentLayer.Item1 is RasterLayer) ((RasterLayer)GlobalState.CurrentLayer.Item1).Save(canvas);
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
            
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName;
            if (fileName == "") System.Windows.MessageBox.Show("You haven't chosen the file");
            else
            {
                GlobalState.CurrentFrame.layers.Add(new Tuple<ILayer, List<Transformation>, int>(new RasterLayer(), new List<Transformation>(), 0));
                GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
                ((RasterLayer)GlobalState.CurrentFrame.layers.Last().Item1).Picture.Image = System.Drawing.Image.FromFile(fileName);
                ((RasterLayer)GlobalState.CurrentFrame.layers.Last().Item1).Print(canvas);
                AddRasterLayer_Click(null, null);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            menu.Width = Width;
        }

        private void GenerateFrame_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) return;
            if (GlobalState.TotalTime == 0) return;
            GlobalState.CurrentScene.Generate(GlobalState.CurrentFrame, GlobalState.TotalTime);
            scenesList_SelectionChanged(null, null);
            Refresh();
        }

        private void SaveToV(object sender, RoutedEventArgs e) {
            List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();
            foreach (var scene in cartoon.scenes) {
                foreach (var frame in scene.frames) {
                    images.Add(frame.Join());
                }
            }
            Exporter.Video.Mp4Exporter ex = new Exporter.Video.Mp4Exporter();
            ex.Save(images, cartoon.WorkingDirectory);
        }

        private void SaveAvi(object sender, RoutedEventArgs e) {
            List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();
            foreach (var scene in cartoon.scenes) {
                foreach (var frame in scene.frames) {
                    images.Add(frame.Join());
                }
            }         
            Exporter.Video.AviExporter ex = new Exporter.Video.AviExporter();
            ex.Save(images, cartoon.WorkingDirectory);
        }
     
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e) {
            var color = new DrawMoar.BaseElements.Color(ClrPcker_Background.SelectedColor.Value);
            GlobalState.Color = color.ToBrush();
        }

        private void DeleteFrame_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) return;
            int index = framesList.SelectedIndex;
            framesList.Items.RemoveAt(index);
            var frames = GlobalState.CurrentScene.frames;
            frames.RemoveAt(index);
            if (frames.Count == 0) {
                frames.Add(new BaseElements.Frame());
                AddListBoxElement(framesList, GlobalState.CurrentFrame.Name);
            }
            framesList.SelectedIndex = framesList.Items.Count > 1 ? index - 1 : 0;
            GlobalState.CurrentFrame = framesList.Items.Count > 1 && index > 0 ? frames[index - 1] : frames[0];
            GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers[0];
            Refresh();
        }

        private void DeleteLayer_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) return;
            int index = layersList.SelectedIndex;
            var layerToDelete = GlobalState.CurrentFrame.layers[index].Item1;
            layersList.Items.RemoveAt(index);
            var layers = GlobalState.CurrentFrame.layers;
            layers.RemoveAt(index);
            if (layers.Count == 0) {
                layers.Add(new Tuple<ILayer, List<Transformation>, int>(new VectorLayer(), new List<Transformation>(), 0));
                AddListBoxElement(layersList, GlobalState.CurrentLayer.Item1.Name);
            }
            layersList.SelectedIndex = layersList.Items.Count > 1 ? index - 1 : 0;
            Refresh();
        }

        private void Refresh() {
            canvas.Children.Clear();
            var layers = GlobalState.CurrentFrame.layers;
            foreach (var item in layers) {
                if (item.Item1 is VectorLayer) {
                    ((VectorLayer)item.Item1).Picture.Draw(canvas);
                }
                else ((RasterLayer)item.Item1).Print(canvas);
            }
        }

        private void AT_Click(object sender, RoutedEventArgs e) {
            ILayer cloneOfCurrent = (ILayer)GlobalState.CurrentLayer.Item1.Clone();
            generationWin = new GenerationDialog(cloneOfCurrent);
            generationWin.Show();
        }

        private void Window_Closed(object sender, EventArgs e) {
            if (generationWin != null) generationWin.Close();
        }

        private void CycleFrame_Click(object sender, RoutedEventArgs e) {
            GlobalState.CurrentScene.Cycle(25);
            scenesList_SelectionChanged(null, null);
            Refresh();
        }
    }
}
