using DrawMoar.BaseElements;
using DrawMoar.Shapes;
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
using DrawMoar.Drawing;

namespace DrawMoar {
    /// <summary>
    /// Логика взаимодействия для GenerationDialog.xaml
    /// </summary>
    public partial class GenerationDialog : Window {
        Point prevPoint;
        Point point;
        Point translation;
        ILayer cloneLayer;
        List<Transformation> transList = new List<Transformation>();
        State clickState = State.Translation;
        IDrawer canvasDrawer;
        bool PressLeftButton;


        public GenerationDialog(ILayer layer) {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Activate();
            previewCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonDown);
            previewCanvas.MouseMove += new MouseEventHandler(previewCanvas_MouseMove);
            previewCanvas.MouseLeftButtonUp += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonUp);
            previewCanvas.MouseLeave += new MouseEventHandler(previewCanvas_MouseLeave);
            cloneLayer = layer;
            previewCanvas.Width = MainWindow.canvSize.Width;
            previewCanvas.Height = MainWindow.canvSize.Height;
            this.Width = MainWindow.canvSize.Width + 250;
            this.Height = MainWindow.canvSize.Height + 50;
            canvasDrawer = new CanvasDrawer(previewCanvas);

            cloneLayer.Draw(canvasDrawer);
        }

        private void previewCanvas_MouseLeftButtonDown(object sender, MouseEventArgs e) {
            prevPoint = Mouse.GetPosition(previewCanvas);
            switch (clickState) {
                case State.Translation:
                    PressLeftButton = true;
                    previewCanvas_MouseMove(sender, e);
                    break;
                case State.RotateCenter:
                    RotatePoint.Text = $"( {(int)prevPoint.X} ; {(int)prevPoint.Y} )";
                    break;
                case State.ScaleCenter:
                    ScalePoint.Text = $"( {(int)prevPoint.X} ; {(int)prevPoint.Y} )";
                    break;
            }
        }

        private void previewCanvas_MouseMove(object sender, MouseEventArgs e) {
            if (clickState == State.Translation) {
                point = (Point)e.GetPosition(previewCanvas);
                if (!PressLeftButton) return;
                TranslatingRedrawing(e);
                prevPoint = point;
            }
        }

        private void previewCanvas_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            PressLeftButton = false;
        }

        private void previewCanvas_MouseLeave(object sender, MouseEventArgs e) {
            PressLeftButton = false;
        }




        void TranslatingRedrawing(MouseEventArgs e) {
            point = e.GetPosition(previewCanvas);
            translation.X += point.X - prevPoint.X;
            translation.Y += point.Y - prevPoint.Y;
            TranslateVector.Text = $"( {(int)(translation.X)} ; {(int)(translation.Y)} )";
            cloneLayer.Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
            Refresh();
        }

        private void Refresh() {
            previewCanvas.Children.Clear();
            cloneLayer.Draw(canvasDrawer);
        }


        private void ApplyTransform_Click(object sender, RoutedEventArgs e) {
            transList.Clear();


            int[] time = new int[3];
            try {
                if (TranslateVector.Text == "" && ScaleFactor.Text == "" && Angle.Text == "") throw new IOException("You haven't created any transformation");
                if (TranslateVector.Text != "") {
                    ApplyTranslation();    //не придется туда передавать время, т.к оно внутри будет свое генериться
                }

                if (Angle.Text != "") {
                    ApplyRotation();
                }

                if (ScaleFactor.Text != "") {
                    ApplyScaling();
                }
                ///TODO: Поместить трансформации в слой            
                var index = Editor.cartoon.CurrentFrame.animations.IndexOf(Editor.cartoon.CurrentAnimation);
                Editor.cartoon.CurrentFrame.animations[index] = new Animation(Editor.cartoon.CurrentAnimation.layers, transList);
                Editor.cartoon.CurrentFrame.animations[index].timeFunction = new TimeFunction(Function.Text);
                //Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentAnimation.layers[0];
                this.Hide();
            }
            catch (IOException ioEx) {
                MessageBox.Show(ioEx.Message);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void ApplyTranslation() {
            Point translateVector = new Point();
            string[] coords = TranslateVector.Text.Split(new char[] { ';', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            translateVector.X = double.Parse(coords[0]);
            translateVector.Y = double.Parse(coords[1]);
            transList.Add(new TranslateTransformation(translateVector));
        }

        private void ApplyScaling() {
            double scaleFactor;
            if (ScalePoint.Text == "") throw new IOException("Enter all fields in the Scale section");
            scaleFactor = 1 + (double.Parse(ScaleFactor.Text) - 1);
            string[] coords = ScalePoint.Text.Split(new char[] { ';', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            Point center = new Point();
            center.X = double.Parse(coords[0]);
            center.Y = double.Parse(coords[1]);
            transList.Add(new ScaleTransformation(center, scaleFactor));    //аналогично
        }

        private void ApplyRotation() {
            double angle;
            if (RotatePoint.Text == "") throw new IOException("Enter all fields in the Rotate section");
            angle = double.Parse(Angle.Text);
            string[] coords = RotatePoint.Text.Split(new char[] { ';', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            Point center = new Point();
            center.X = double.Parse(coords[0]);
            center.Y = double.Parse(coords[1]);
            transList.Add(new RotateTransformation(center, angle));
        }

        private void Angle_TextChanged(object sender, TextChangedEventArgs e) {
            double symb;
            if (!double.TryParse(Angle.Text, out symb)) {
                Angle.Clear();
            }
        }

        private void ScaleFactor_TextChanged(object sender, TextChangedEventArgs e) {
            double symb;
            if (!double.TryParse(ScaleFactor.Text, out symb)) {
                ScaleFactor.Clear();
            }
        }
     

        private void Translate_Click(object sender, RoutedEventArgs e) {
            clickState = State.Translation;
        }

        private void RotateCenter_Click(object sender, RoutedEventArgs e) {
            clickState = State.RotateCenter;
        }

        private void ScaleCenter_Click(object sender, RoutedEventArgs e) {
            clickState = State.ScaleCenter;
        }
    }

    enum State {            //набор возможных состояний для интерпретации клика по канвасу
        Translation,
        ScaleCenter,
        RotateCenter
    }
}
