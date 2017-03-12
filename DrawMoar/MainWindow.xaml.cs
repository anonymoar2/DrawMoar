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
            GlobalState.ChangeInstrument += SetCursorStyle;
            GlobalState.Color = Brushes.Black;
            GlobalState.BrushSize = new Size(5, 5);
        }

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

        private void SaveToPNG(object sender, RoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog
            {
                FileName = "img",
                DefaultExt = ".png",
                Filter = "PNG (.png)|*.png"
            };

            if (saveDlg.ShowDialog() == true)
            {
                //SaveCanvas(canvas, 96, saveDlg.FileName);
            }
        }


        private void SaveCanvas(InkCanvas canvas, int dpi, string filename)
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

        /// <summary>
        /// "Успешное" закрытие окна создания мультфильма.
        /// Выполняется при нажатии кнопки Create (Создать)
        /// </summary>
        /// <param name="cartoon"></param>
        public void Success(Cartoon cartoon)
        {
            canvas.Visibility = Visibility.Visible;
            canvas.Width = cartoon.Width;
            canvas.Height = cartoon.Height;
            this.cartoon = cartoon;
            GlobalState.canvSize = new Size(canvas.Width, canvas.Height);
            this.Height = canvas.Height;
            this.Width = canvas.Width + 260;        //пока что так (ширина двух крайних колонок грида)
            AddScene_Click(null, null);         // должно быть добавление сцены
        }


        private int i = 0;
        public void CreateNewFrame(object sender, RoutedEventArgs e)
        {

            string frameName = $"img{i++}.png";
            // SaveCanvas(canvas, 90, System.IO.Path.Combine(cartoon.WorkingDirectory, /*cartoon.Name*/frameName));
            // canvas.Strokes.Clear();
            cartoon.frames.Add(new BaseElements.Frame(cartoon.WorkingDirectory));
            cartoon.currentFrame = cartoon.frames.Last();
            // сделать миниатюру и отображение в списке кадров 


        }


        // https://github.com/artesdi/Paint.WPF
        public void DeleteFrame(object sender, RoutedEventArgs e)
        {

            cartoon.frames.Remove(cartoon.currentFrame);
            // удаление из списка кадров на экране
            // canvas.Strokes.Clear();
            // переключение и отображение предыдущего/следующего кадра
        }

        /// <summary>
        /// Смена рабочего цвета на выбранный в основной палитре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedEventArgs e)
        {
            GlobalState.Color = new SolidColorBrush(ClrPcker_Background.SelectedColor.Value);
        }

        /// <summary>
        /// Добавление нового кадра в мультфильм.
        /// Подразумевает добавление одного слоя на новый кадр.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {
            //проверка на то, выделен ли какой-либо кадр
            var layer = new LayerControl();     //может, это вынести в Base Elements - ?
            canvas.Children.Add(layer);         //куда добавлять - ?
            var lbl = new Label();
            lbl.Content = $"layer_{framesList.LayersCount++}";   //внутри кадра должен быть счетчик - ?
            lbl.Height = 40;
            lbl.Width = 70;
            layersList.Items.Add(lbl);
            //GlobalState.LayersIndexes++;
            layersList.SelectedIndex = layersList.Items.Count - 1;
            canvas.Children[layersList.SelectedIndex].Focus();
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

        /// <summary>
        /// Обработка выбора элемента из элемента, отображающего кадры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UIElement layer = canvas.Children[framesList.SelectedIndex];
            layer.Visibility = Visibility.Visible;
            layer.Focus();
            foreach (LayerControl child in canvas.Children) //ищем не здесь, а в кадре
            {
                if (child != layer)
                {
                    child.Visibility = Visibility.Hidden;
                    child.NonFocus(null, null);
                }
            }
        }


        //ТАКЖЕ НУЖНО СДЕЛАТЬ SCENELIST И LAYERLIST_SELECTIONCHANGED

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instruments.Brush;
        }

        private void testButton2_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instruments.Arrow;
        }

        private void AddFrame_Click(object sender, RoutedEventArgs e)
        {
            var newFrame = new Frame();
            //создаем фрейм, берем его в фокус, создаем в нем слой, запихиваем его в активную сцену
        }

        private void AddScene_Click(object sender, RoutedEventArgs e)
        {
            var newScene = new Scene();
            //делаем что-то осмысленное: создаем внутри сцены кадр, внутри кадра - слой(в целом, а не здесь)
            //добавляем сцену в список, который еще на окне нужно как-то отразить (и где-то)
            cartoon.Scenes.Add(newScene);
            AddFrame_Click(scenesList,null);
        }
    }
}
