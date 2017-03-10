using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

using BaseElements;
using Exporter.Video;
using Exporter.Photo;

namespace DrawMoar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool cartoonE = false;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            GlobalState.Color = Brushes.Black;
            GlobalState.BrushSize = new Size(5, 5);
        }

        private void CreateCartoon(object sender, RoutedEventArgs e) {
            // создаём новый пустой кадр
            // на кадре новый пустой слой создаём
            var newCartoonDialog = new CreateCartoonDialog();
            newCartoonDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newCartoonDialog.Owner = this;
            newCartoonDialog.Show();
        }

        /// <summary>
        /// По нажатию в двух местах канваса сделать создание линии и других фигур
        /// </summary>
        private void AddLine(object sender, RoutedEventArgs e) {
            var myLine = new Line();
            myLine.Stroke = Brushes.LightSteelBlue;

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

        private void ExportToMP4(object sender, RoutedEventArgs e) {
            var mp4Exporter = new Mp4Exporter();
            mp4Exporter.Save(cartoon, cartoon.WorkingDirectory);
        }

        private void SaveToPNG(object sender, RoutedEventArgs e) {
            var saveDlg = new SaveFileDialog {
                FileName = "img",
                DefaultExt = ".png",
                Filter = "PNG (.png)|*.png"
            };

            if (saveDlg.ShowDialog() == true) {
                SaveCurrentCanvas();
                var pngExporter = new PngExporter();
                pngExporter.Save(cartoon.currentFrame, saveDlg.FileName);
            }
        }

        private RenderTargetBitmap CanvasToBitmap(InkCanvas canvas) {
            var width = canvas.ActualWidth;
            var height = canvas.ActualHeight;

            var size = new Size(width, height);
            canvas.Measure(size);

            var rtb = new RenderTargetBitmap(
                (int)width + 120,
                (int)height,
                96, //dpi x 
                96, //dpi y 
                PixelFormats.Pbgra32 // pixelformat
            );
            rtb.Render(canvas);
            return rtb;
        }

        public void Success(Cartoon cartoon) {
            // возможно пересоздание окна, либо что-то сделать с холстом, но можно забить пока
            cartoonE = true;
            this.cartoon = cartoon;
            canvas.Height = cartoon.Height;
            canvas.Width = cartoon.Width;
            canvas.EditingMode = InkCanvasEditingMode.Ink;
            canvas.Opacity = 1;
        }

        public void CreateNewFrame(object sender, RoutedEventArgs e) {
            if (cartoonE == true) {
                //string frameName = $"img{i++}.png";
                //SaveCanvas(canvas, 90, System.IO.Path.Combine(cartoon.WorkingDirectory, /*cartoon.Name*/frameName));
                SaveCurrentCanvas();
                canvas.Strokes.Clear();
                cartoon.AddFrame();
                // отображение в списке кадров
            }
        }

        /// <summary>
        /// На переписывание
        /// </summary>
        public void SaveCurrentCanvas() {
            cartoon.currentFrame.Bitmap = CanvasToBitmap(canvas);
        }

        //public void ShowFrame(int index) {
        //    var frame = cartoon.GetAllFrames()[index];
        //    Image image = cartoon.ImageFromBytes(frame.bytes);
        //    canvas.Children.Add(image);
        //    canvas.Strokes.


        //}

        public void DeleteFrame(object sender, RoutedEventArgs e) {
            if (cartoon.GetAllFrames().IndexOf(cartoon.currentFrame) > 0 && cartoon.GetAllFrames().IndexOf(cartoon.currentFrame) < cartoon.GetAllFrames().Count) {
                var frame = cartoon.currentFrame;
                cartoon.currentFrame = cartoon.GetAllFrames()[cartoon.GetAllFrames().IndexOf(frame) - 1];
                cartoon.RemoveFrame(frame);
            }
            else if (cartoon.GetAllFrames().IndexOf(cartoon.currentFrame) == 0) {
                var frame = cartoon.currentFrame;
                cartoon.currentFrame = cartoon.GetAllFrames()[cartoon.GetAllFrames().IndexOf(frame) + 1];
                cartoon.RemoveFrame(frame);
            }
            else {
                throw new ArgumentException();//  ну или другое, продумаю этот момент позже
            }
        }

        //// https://github.com/artesdi/Paint.WPF
        //public void DeleteFrame(object sender, RoutedEventArgs e) {

        //    cartoon.frames.Remove(cartoon.currentFrame);
        //    // удаление из списка кадров на экране
        //    canvas.Strokes.Clear();
        //    // переключение и отображение предыдущего/следующего кадра
        //}

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedEventArgs e) {
            canvas.DefaultDrawingAttributes.Color = ClrPcker_Background.SelectedColor.Value;
        }

        /// Кнопка сохранения в меню
        /// Ctrl + S
        private void ButtonSaveClicked(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SaveCurrentCanvas();
        }
    }
}
