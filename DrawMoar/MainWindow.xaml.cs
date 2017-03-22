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

using DrawMoar.Shapes;
using DrawMoar.BaseElements;


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
            canvas.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(canvas_MouseLeftButtonDown);
            canvas.PreviewMouseMove += new MouseEventHandler(canvas_MouseMove);
            canvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(canvas_MouseLeftButtonUp);
            canvas.MouseLeave += new MouseEventHandler(canvas_MouseLeave);
            GlobalState.CurrentTool = Instrument.Arrow;
            GlobalState.ChangeInstrument += SetCursorStyle;
            GlobalState.Color = System.Windows.Media.Brushes.Black;
            GlobalState.BrushSize = new System.Windows.Size(5, 5);
        }

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
            GlobalState.canvSize = new System.Windows.Size(canvas.Width, canvas.Height);
            Height = canvas.Height;
            Width = canvas.Width + 260;        //пока что так (ширина двух крайних колонок грида)
            AddScene_Click(null, null);
            AddFrame_Click(null, null);
            AddLayer_Click(null, null);
        }

        private void ExportToMP4(object sender, RoutedEventArgs e)
        {
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
        private void AddLayer_Click(object sender, RoutedEventArgs e)
        {
            /*if (cartoon == null)
            {
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
            }*/

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
        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*layersList.Items.Clear();
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
            layersList.SelectedIndex = 0;*/
        }


        private void scenesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            framesList.Items.Clear();
            cartoon.CurrentScene = cartoon.GetAllScenes()[scenesList.SelectedIndex];
            var frames = cartoon.CurrentScene.GetAllFrames();
            int i = 0;                                          //с именами большая беда. нужно будет в BaseElements разобраться
            foreach (var item in frames)
            {
                AddListBoxElement(framesList, $"frame{i++}");
            }
        }

        private void layersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*foreach (LayerControl item in canvas.Children)
            {
                item.Visibility = Visibility.Hidden;
            }
            if ((layersList.SelectedIndex != -1) && (canvas.Children.Count > layersList.SelectedIndex))
                canvas.Children[layersList.SelectedIndex].Visibility = Visibility.Visible;*/

        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instrument.Brush;
        }

        private void testButton2_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instrument.Arrow;
        }

        private void AddFrame_Click(object sender, RoutedEventArgs e)
        {
            /*if (cartoon == null)
            {
                return;
            }
            cartoon.CurrentScene.AddFrame();
            cartoon.CurrentScene.currentFrame.CurrentLayer.drawingControl = new LayerControl();
            //cartoon.CurrentScene.currentFrame.CurrentLayer.drawingControl = new LayerControl();
            var frames = cartoon.CurrentScene.GetAllFrames();
            AddListBoxElement(framesList, $"frame_{frames.Count - 1}");
            //cartoon.CurrentScene.currentFrame = frames[framesList.SelectedIndex];*/
        }

        private void AddScene_Click(object sender, RoutedEventArgs e)
        {
            if (cartoon == null)
            {
                return;
            }
            if (sender != null)
            {
                cartoon.AddEmptyScene();
                cartoon.CurrentScene.AddEmptyFrame();
                cartoon.CurrentScene.CurrentFrame = cartoon.CurrentScene.GetAllFrames()[0];
            }
            AddListBoxElement(scenesList, cartoon.CurrentScene.Name);
        }

        private void AddListBoxElement(ListBox lBox, string content)
        {
            var lbl = new Label();          //здесь должен быть какой-то другой контрол (возможно, самописный)
            lbl.Content = content;
            lBox.Items.Add(lbl);
            lBox.SelectedIndex = lBox.Items.Count - 1;
        }


        void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GlobalState.PressLeftButton = true;
            prevPoint = Mouse.GetPosition(canvas);
            switch (GlobalState.CurrentTool)
            {
                case Instrument.Brush:
                    break;
                case Instrument.Rectangle:
                    newRect = new DrawMoar.Shapes.Rectangle(prevPoint, new Size(30, 20));
                    newRect.Draw(canvas);
                    break;
                case Instrument.Ellipse:
                    newEllipse = new DrawMoar.Shapes.Ellipse(prevPoint, new Size(30, 20));
                    newEllipse.Draw(canvas);
                    break;
                case Instrument.Line:
                    //задерживание кнопки мыши, отпустили = конец линии    
                    newLine = new DrawMoar.Shapes.Line(prevPoint, prevPoint);
                    newLine.Draw(canvas);
                    break;
            }
            canvas_MouseMove(sender, e);
        }

        void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            point = (Point)e.GetPosition(canvas);
            if (!GlobalState.PressLeftButton) return;
            switch (GlobalState.CurrentTool)
            {
                case Instrument.Brush:                                                                                                                   
                        newLine = new DrawMoar.Shapes.Line(prevPoint, point);
                        newLine.Draw(canvas);
                        prevPoint = point;
                    break;
                case Instrument.Rectangle:                  
                    ScalingRedrawing(canvas, newRect, e);
                    break;
                case Instrument.Ellipse:
                    ScalingRedrawing(canvas, newEllipse, e);
                    break;
                case Instrument.Line:
                    ScalingRedrawing(canvas, newLine, e);
                    break;
            }
        }

        void ScalingRedrawing(Canvas canvas, IShape shape, MouseEventArgs e)
        {
            if (shape != null & e.LeftButton == MouseButtonState.Pressed)
            {
                canvas.Children.RemoveAt(canvas.Children.Count - 1);
                if (shape is DrawMoar.Shapes.Line) shape = new DrawMoar.Shapes.Line(prevPoint, point);
                else if (shape is DrawMoar.Shapes.Ellipse) shape = new DrawMoar.Shapes.Ellipse(prevPoint, new Size(30+point.X-prevPoint.X, 20+point.Y-prevPoint.Y));
                else if (shape is DrawMoar.Shapes.Rectangle) shape = new DrawMoar.Shapes.Rectangle(prevPoint, new Size(30+ point.X - prevPoint.X, 20 + point.Y - prevPoint.Y));
                shape.Draw(canvas);
            }
        }


        void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           GlobalState.PressLeftButton = false;
        }

        void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            GlobalState.PressLeftButton = false;
        }

        private void Lines_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instrument.Line;
        }

        private void AddEllipse_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instrument.Ellipse;
        }

        private void AddRectangle_Click(object sender, RoutedEventArgs e)
        {
            GlobalState.CurrentTool = Instrument.Rectangle;
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
