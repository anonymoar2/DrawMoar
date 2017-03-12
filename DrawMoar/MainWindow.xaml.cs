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
using System.Windows.Shapes;
using BaseElements;
using BaseElements.Instruments;

namespace DrawMoar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Cartoon cartoon;

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
            GlobalState.ChangeInstrument += SetCursorStyle;
            GlobalState.Color = Brushes.Black;
            GlobalState.BrushSize = new Size(5, 5);
        }

        private List<FrameControl> frames = new List<FrameControl>();
        private List<Label> labels = new List<Label>();
        //тогда отрисовку придется выносить в отдельный класс и она будет сложнее - много работы в плане 

        private void CreateCartoon(object sender, RoutedEventArgs e)
        {
            // создаём новый пустой кадр
            // на кадре новый пустой слой создаём
            var newCartoonDialog = new CreateCartoonDialog();
            newCartoonDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newCartoonDialog.Owner = this;
            newCartoonDialog.Show();

        }

        /// <summary>
        /// по нажатию в двух местах канваса сделать создание линии и других фигур
        ///  
        /// </summary>

        private void AddLine(object sender, RoutedEventArgs e)
        {

            var myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;

            Random rand = new Random();
            myLine.X1 = rand.Next(1, 282);
            myLine.X2 = rand.Next(1, 282);
            myLine.Y1 = rand.Next(1, 291);
            myLine.Y2 = rand.Next(1, 291);
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            canvas.Children.Add(myLine);
        }


        private void ExportToMP4(object sender, RoutedEventArgs e)
        {
            // должны 1) пройтись по кадрам и сохранить все в картинки
            // 
            // 2) запилить видео
            // 3) удалить вспомогательные файлы
        }

        private void SaveToPNG(object sender, RoutedEventArgs e) {
            var saveDlg = new SaveFileDialog {
                FileName = "img",
                DefaultExt = ".png",
                Filter = "PNG (.png)|*.png"
            };

            if (saveDlg.ShowDialog() == true) {
                //SaveCanvas(canvas, 96, saveDlg.FileName);
            }
        } 


        private void SaveCanvas(InkCanvas canvas, int dpi, string filename) {
            var width = canvas.ActualWidth;
            var height = canvas.ActualHeight;

            var size = new Size(width, height);
            canvas.Measure(size);

            var rtb = new RenderTargetBitmap(
                (int)width + 120,
                (int)height,
                dpi, //dpi x 
                dpi, //dpi y 
                PixelFormats.Pbgra32 // pixelformat 
                );
            rtb.Render(canvas);

            SaveAsPng(rtb, filename);
        }

        private static void SaveAsPng(RenderTargetBitmap bmp, string filename)
        {
            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bmp));

            using (FileStream stm = File.Create(filename))
            {
                enc.Save(stm);
            }
        }

        public void Success(Cartoon cartoon)
        {
            canvas.Visibility = Visibility.Visible;
            canvas.Width = cartoon.Width;
            canvas.Height = cartoon.Height;
            this.cartoon = cartoon;
            GlobalState.canvSize = new Size(canvas.Width, canvas.Height);
            this.Height = canvas.Height;
            this.Width = canvas.Width + 260;        //пока что так (ширина двух крайних колонок грида)
            AddFrame_Click(null, null);
        }


        private int i = 0;
        public void CreateNewFrame(object sender, RoutedEventArgs e) {

            string frameName = $"img{i++}.png";
           // SaveCanvas(canvas, 90, System.IO.Path.Combine(cartoon.WorkingDirectory, /*cartoon.Name*/frameName));
           // canvas.Strokes.Clear();
            cartoon.frames.Add(new BaseElements.Frame(cartoon.WorkingDirectory));
            cartoon.currentFrame = cartoon.frames.Last();
            // сделать миниатюру и отображение в списке кадров 
            

        }


        // https://github.com/artesdi/Paint.WPF
        public void DeleteFrame(object sender, RoutedEventArgs e) {

            cartoon.frames.Remove(cartoon.currentFrame);
            // удаление из списка кадров на экране
           // canvas.Strokes.Clear();
            // переключение и отображение предыдущего/следующего кадра
        }


        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedEventArgs e)
        {
            //позже
        }


        private void AddFrame_Click(object sender, RoutedEventArgs e)
        {
            var frame = new FrameControl();
            canvas.Children.Add(frame);
            var lbl = new Label();
            lbl.Content = $"frame_{GlobalState.FramesCount}";
            lbl.Height = 40;
            lbl.Width = 70;
            framesList.Items.Add(lbl);
            GlobalState.LayersIndexes++;
            framesList.SelectedIndex = framesList.Items.Count-1;
            canvas.Children[framesList.SelectedIndex].Focus();
        }

        /// <summary>
        ///     Изменение курсора мыши в зависимости от выбранного инструмента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCursorStyle(Object sender, EventArgs e)
        {
            switch (GlobalState.CurrentTool)
            {
                case Instruments.Brush:
                    canvas.Cursor = Cursors.Cross;
                    break;
                default:
                    canvas.Cursor = Cursors.Arrow;
                    break;
            }
        }

        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIElement frame = canvas.Children[framesList.SelectedIndex];            
            frame.Visibility = Visibility.Visible;
            frame.Focus();
            foreach (FrameControl child in canvas.Children)
            {
                if (child != frame)
                {
                    child.Visibility = Visibility.Hidden;
                    child.NonFocus(null, null);
                }
            }
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instruments.Brush;
        }

        private void testButton2_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instruments.Arrow;
        }
    }
}
