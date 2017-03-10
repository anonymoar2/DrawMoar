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
using Exporter;
using Exporter.Video;

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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
        /// по нажатию в двух местах канваса сделать создание линии и других фигур
        ///  
        /// </summary>

        private void AddLine(object sender, RoutedEventArgs e) {
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
        

        private void ExportToMP4(object sender, RoutedEventArgs e) {
            Mp4Exporter exp = new Mp4Exporter();
            exp.Save(cartoon, cartoon.WorkingDirectory);

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
                SaveCanvas(canvas, 96, saveDlg.FileName);
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

        private static void SaveAsPng(RenderTargetBitmap bmp, string filename) {
            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(bmp));

            using (FileStream stm = File.Create(filename)) {
                enc.Save(stm);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e) {
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


        private int i = 0;
        public void CreateNewFrame(object sender, RoutedEventArgs e) {
            if (cartoonE == true) {
                //string frameName = $"img{i++}.png";
                //SaveCanvas(canvas, 90, System.IO.Path.Combine(cartoon.WorkingDirectory, /*cartoon.Name*/frameName));
                SaveCurrentCanvas();
                canvas.Strokes.Clear();
                cartoon.AddFrame();
                // отображение в списке кадров
            }
            else {
                throw new Exception("Мультик не создан");
            }
        }



        // Вызывать при переключении на другой кадр, сохраняет содержимое cartoon.currentframe
        // то есть вызывать до того как currentframe поменятся на другой frame
        public void SaveCurrentCanvas() {
            using (MemoryStream ms = new MemoryStream()) {
                if (canvas.Strokes.Count > 0) {
                    canvas.Strokes.Save(ms, true);
                    cartoon.currentFrame.bytes = ms.ToArray();
                }
            }
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
    }
}
