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
using DrawMoar.Drawing;


namespace DrawMoar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Cartoon cartoon;

        Point prevPoint;
        DrawMoar.Shapes.Line newLine;
        Point point;
        DrawMoar.Shapes.Ellipse newEllipse;
        DrawMoar.Shapes.Rectangle newRect;
        IDrawer canvasDrawer;
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
            canvasDrawer = new CanvasDrawer(canvas);
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


        private void AddListBoxElement(ListBox lBox, string content) {
            var lbl = new Label();          
            lbl.Content = content;
            lBox.Items.Add(lbl);
            lBox.SelectedIndex = lBox.Items.Count - 1;
        }


        private void AddPicture(object sender, RoutedEventArgs e) {
            if (cartoon == null) return;
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName;
            if (fileName == "") System.Windows.MessageBox.Show("You haven't chosen the file");
            else {
                GlobalState.CurrentFrame.layers.Add(new Tuple<ILayer, List<Transformation>, int>(new RasterLayer(), new List<Transformation>(), 0));
                GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
                ((RasterLayer)GlobalState.CurrentFrame.layers.Last().Item1).Picture.Image = System.Drawing.Image.FromFile(fileName);
                GlobalState.CurrentFrame.layers.Last().Item1.Draw(canvasDrawer);
                AddRasterLayer_Click(null, null);
            }
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            menu.Width = Width;
        }


        private void SaveToMp4(object sender, RoutedEventArgs e) {
            try {
                DrawMoar.ffmpeg.ExportToVideo.SaveToVideo(cartoon, "mp4", "");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }


        private void SaveToAvi(object sender, RoutedEventArgs e) {
            try {
                DrawMoar.ffmpeg.ExportToVideo.SaveToVideo(cartoon, "avi", cartoon.pathToAudio);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
     

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e) {
            var color = new DrawMoar.BaseElements.Color(ClrPcker_Background.SelectedColor.Value);
            GlobalState.Color = color.ToBrush();
        }


        private void Refresh() {
            canvas.Children.Clear();
            var layers = GlobalState.CurrentFrame.layers;
            foreach (var item in layers) {
                item.Item1.Draw(canvasDrawer);
            }
        }


        private void Window_Closed(object sender, EventArgs e) {
            if (generationWin != null) generationWin.Close();
        }

        private void AFT_Click(object sender, RoutedEventArgs e) {
            
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e) {

        }
    }
}
