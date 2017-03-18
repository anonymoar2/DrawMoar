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
using System.Drawing;

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
            GlobalState.Color = System.Windows.Media.Brushes.Black;
            GlobalState.BrushSize = new System.Windows.Size(5, 5);
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
                //SaveCanvas(canvas, 96, saveDlg.FileName);
            }
        }


        private void SaveCanvas(InkCanvas canvas, int dpi, string filename) {
            var width = canvas.ActualWidth;
            var height = canvas.ActualHeight;

            var size = new System.Windows.Size(width, height);
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
            GlobalState.canvSize = new System.Windows.Size(canvas.Width, canvas.Height);
            Height = canvas.Height;
            Width = canvas.Width + 260;        //пока что так (ширина двух крайних колонок грида)
            AddScene_Click(null, null);
            AddFrame_Click(null, null);
            AddLayer_Click(null, null);
        }


        public void CreateNewFrame(object sender, RoutedEventArgs e) {

            //string frameName = $"img{i++}.png";
            // SaveCanvas(canvas, 90, System.IO.Path.Combine(cartoon.WorkingDirectory, /*cartoon.Name*/frameName));
            // canvas.Strokes.Clear();
            //cartoon.frames.Add(new BaseElements.Frame(cartoon.WorkingDirectory));
            //cartoon.currentFrame = cartoon.frames.Last();
            // сделать миниатюру и отображение в списке кадров 


        }


        // https://github.com/artesdi/Paint.WPF
        public void DeleteFrame(object sender, RoutedEventArgs e) {

            //cartoon.frames.Remove(cartoon.currentFrame);
            // удаление из списка кадров на экране
            // canvas.Strokes.Clear();
            // переключение и отображение предыдущего/следующего кадра
        }

        /// <summary>
        /// Смена рабочего цвета на выбранный в основной палитре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedEventArgs e) {
            GlobalState.Color = new SolidColorBrush(ClrPcker_Background.SelectedColor.Value);
        }

        /// <summary>
        /// Добавление нового кадра в мультфильм.
        /// Подразумевает добавление одного слоя на новый кадр.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLayer_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            //проверка на то, выделен ли какой-либо кадр(когда реализуем удаление)
            var drawingControl = new LayerControl();
            drawingControl.Focus();
            var layer = new RasterLayerView(new Bitmap(cartoon.CurrentScene.currentFrame.Width,
                                                        cartoon.CurrentScene.currentFrame.Height));
            layer.Name = $"layer_{layersList.Items.Count}";
            layer.drawingControl = drawingControl;

            if (sender != null)
            {
                cartoon.CurrentScene.currentFrame.AddLayer(layer);
                AddListBoxElement(layersList, layer.Name);
                canvas.Children.Add((LayerControl)layer.drawingControl);
            }
            
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
            {
                cartoon.CurrentScene.currentFrame = cartoon.CurrentScene.GetAllFrames()[framesList.SelectedIndex];
            }
            var lays = cartoon.CurrentScene.currentFrame.GetAllLayers();
            int i = 0;      //пока не пофиксили имена слоев
            foreach (var item in lays)
            {
                canvas.Children.Add((LayerControl)item.drawingControl);
                AddListBoxElement(layersList, $"layer{i++}"); //вторым параметром должны быть Names
            }
            layersList.SelectedIndex = 0;
        }


        private void scenesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            framesList.Items.Clear();
            cartoon.CurrentScene = cartoon.GetAllScenes()[scenesList.SelectedIndex];
            var frames = cartoon.CurrentScene.GetAllFrames();
            int i = 0;                                          //с именами большая беда. нужно будет в BaseElements разобраться
            foreach (var item in frames) {
                AddListBoxElement(framesList, $"frame{i++}");
            }
        }

        private void layersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (LayerControl item in canvas.Children)
            {
                item.Visibility = Visibility.Hidden;
            }
            if((layersList.SelectedIndex!=-1)&&(canvas.Children.Count > layersList.SelectedIndex))
                canvas.Children[layersList.SelectedIndex].Visibility = Visibility.Visible;

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
            cartoon.CurrentScene.AddFrame();
            cartoon.CurrentScene.currentFrame.CurrentLayer.drawingControl = new LayerControl();
            //cartoon.CurrentScene.currentFrame.CurrentLayer.drawingControl = new LayerControl();
            var frames = cartoon.CurrentScene.GetAllFrames();
            AddListBoxElement(framesList, $"frame_{frames.Count - 1}");
            //cartoon.CurrentScene.currentFrame = frames[framesList.SelectedIndex];
        }

        private void AddScene_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                cartoon.AddScene();
                cartoon.CurrentScene.AddFrame();
                cartoon.CurrentScene.currentFrame = cartoon.CurrentScene.GetAllFrames()[0];
            }
            AddListBoxElement(scenesList, cartoon.CurrentScene.Name);
        }

        private void AddListBoxElement(ListBox lBox, string content) {
            var lbl = new Label();          //здесь должен быть какой-то другой контрол (возможно, самописный)
            lbl.Content = content;
            lBox.Items.Add(lbl);
            lBox.SelectedIndex = lBox.Items.Count - 1;
        }

        private void StartLightVector(object sender, RoutedEventArgs e) {
            GlobalState.lightVector = new Instruments.LightVector(cartoon);
            GlobalState.lightVector.active = true;
            var drawingControl = new LayerControl();
            drawingControl.Focus();
            GlobalState.CurrentTool = Instrument.Light;

            if (cartoon.CurrentScene.currentFrame.CurrentLayer.GetType().Name != "LightVectorLayer") {
                var layer = new LightVectorLayer();
                layer.Name = $"LIGHTlayer_{layersList.Items.Count}";
                layer.drawingControl = drawingControl;
                cartoon.CurrentScene.currentFrame.AddLayer(layer);
                string text = $"LIGHTlayer_{layersList.Items.Count}";
                AddListBoxElement(layersList, text);
            }
            canvas.Children.Add(drawingControl);
            
        }
    }
}
