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

namespace DrawMoar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<InkCanvas> canv = new List<InkCanvas>();
        public MainWindow() {
            InitializeComponent();
            // при создании окна области рисования нет

            // только после создания мультика в котором указывается размер холста в пропорциях
            // потом ещё размеры подогнать надо либо сам развернёт
            // панельки фиксированные и списки тоже

            // после создания мультика появляется первый пустой кадр
            // и на нём первый пустой слой

            // для теста, потом всё это можно будет поменять без проблем

            GlobalState.Color = Brushes.Black;
            GlobalState.BrushSize = new Size(5, 5);
        }

        private void CreateCartoon(object sender, RoutedEventArgs e) {
            // создаём новый пустой кадр
            // на кадре новый пустой слой создаём
            var InkCanvas1 = new InkCanvas();   //новое окно для получения размеров - ?
            InkCanvas1.Width = 200;
            InkCanvas1.Height = 200;        //Если без задания размеров, изменить свойство DockPanel.LastChildFill на true
            InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;        //выделение InkCanvas рамкой для визуального удобства - ?(нужно ли?)
            rootPanel.Children.Add(InkCanvas1);        
            canv.Add(InkCanvas1);
            
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

        private void SaveCanvas(Canvas canvas, int dpi, string filename) {
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

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            canv[0].Opacity = 0.5;                    //questionable (0 <= x <= 1)
            canv[0].EditingMode = InkCanvasEditingMode.None; 
            var InkCanvas2 = new InkCanvas();
            InkCanvas2.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas2.Width = 200;
            InkCanvas2.Height = 200;
            rootPanel.Children.Add(InkCanvas2);     //добавление НА предыдущий инк - ? (съезжает влево)
            canv.Add(InkCanvas2);
        }
    }
}
