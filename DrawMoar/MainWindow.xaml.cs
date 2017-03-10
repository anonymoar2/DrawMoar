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
        public MainWindow()
        {
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

        private int TotalFrames = 0;
        private List<Label> labels = new List<Label>();
        private List<InkCanvas> canv = new List<InkCanvas>();

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
            //canvas.Children.Add(myLine);
        }


        private void ExportToMP4(object sender, RoutedEventArgs e)
        {
            // должны 1) пройтись по кадрам и сохранить все в картинки
            // 
            // 2) запилить видео
            // 3) удалить вспомогательные файлы
        }

        private void SaveToPNG(object sender, RoutedEventArgs e)
        {
            /*var saveDlg = new SaveFileDialog {
                FileName = "img",
                DefaultExt = ".png",
                Filter = "PNG (.png)|*.png"
            };

            if (saveDlg.ShowDialog() == true) {
                SaveCanvas(canvas, 96, saveDlg.FileName);
            }*/
        }

        private void SaveCanvas(Canvas canvas, int dpi, string filename)
        {
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

        public void Success(string Name, int CartoonHeight, int CartoonWidth)
        {
            canvas.Visibility = Visibility.Visible;
            canvas.Width = CartoonWidth;
            canvas.Height = CartoonHeight;
            canvas.EditingMode = InkCanvasEditingMode.Ink;
            canv.Add(canvas);
            AddFrame_Click(null, null);
            /*canvas.Strokes.Clear();
            canvas.Height = CartoonHeight;
            canvas.Width = CartoonWidth;
            canvas.EditingMode = InkCanvasEditingMode.Ink;
            canvas.Opacity = 1;*/
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedEventArgs e)
        {
            canv[frames.SelectedIndex].DefaultDrawingAttributes.Color = ClrPcker_Background.SelectedColor.Value;
        }


        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            frames.Height = (rootGrid.ActualHeight - buttons.ActualHeight - smth.ActualHeight) / 2;     //не знаю, стоит ли так делать
            layers.Height = (rootGrid.ActualHeight - buttons.ActualHeight - smth.ActualHeight) / 2;         //smth - выделение места под что-то (макет Ирины)
        }

        private void AddFrame_Click(object sender, RoutedEventArgs e)
        {
            if (e != null)                                          //здесь также нужно проверять наличие экземпляра мультика
            {
                var inkCanv = new InkCanvas();
                inkCanv.Height = canvas.Height;
                inkCanv.Width = canvas.Width;
                inkCanv.EditingMode = InkCanvasEditingMode.Ink;
                canv.Add(inkCanv);
                rootGrid.Children.Add(inkCanv);
            }
            var lbl = new Label();
            lbl.Content = $"frame_{TotalFrames++}";
            lbl.Width = 110;
            lbl.Height = 40;
            frames.Items.Add(lbl);
            frames.SelectedItem = frames.Items[frames.Items.Count - 1];
        }

        private void frames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var snd = sender as ListBox;
            var selIndex = snd.SelectedIndex;   //дальше в коде употреблялось
            foreach (var item in canv)              //переделаю в for()
            {
                item.Visibility = Visibility.Hidden;
            }          
            canv[selIndex].Visibility = Visibility.Visible;
            //здесь был функционал полупрозрачности
        }
    }
}
