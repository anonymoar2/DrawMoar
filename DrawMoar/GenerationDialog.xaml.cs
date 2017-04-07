﻿using DrawMoar.BaseElements;
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

namespace DrawMoar {
    /// <summary>
    /// Логика взаимодействия для GenerationDialog.xaml
    /// </summary>
    public partial class GenerationDialog : Window {
        Point prevPoint;
        Point point;
        Point start;
        ILayer layer;
        IShape clickedShape;
        Rectangle newRect;
        Ellipse newEllipse;
        Line newLine;
        List<Tuple<Transformation,int>> transList = new List<Tuple<Transformation,int>>();


        public GenerationDialog(ILayer layer) {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Activate();
            previewCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonDown);
            previewCanvas.MouseMove += new MouseEventHandler(previewCanvas_MouseMove);
            previewCanvas.MouseLeftButtonUp += new MouseButtonEventHandler(previewCanvas_MouseLeftButtonUp);
            previewCanvas.MouseLeave += new MouseEventHandler(previewCanvas_MouseLeave);
            this.layer = layer;
            previewCanvas.Width = GlobalState.canvSize.Width;
            previewCanvas.Height = GlobalState.canvSize.Height;
            this.Width = GlobalState.canvSize.Width + 250;
            this.Height = GlobalState.canvSize.Height + 50;

            if (layer is VectorLayer) {
                ((VectorLayer)layer).Picture.Draw(previewCanvas);
            }
            //TODO: РАСТР...
        }

        private void previewCanvas_MouseLeftButtonDown(object sender, MouseEventArgs e) {
            GlobalState.PressLeftButton = true;
            prevPoint = Mouse.GetPosition(previewCanvas);          
            if (layer is VectorLayer)
                clickedShape = GetClickedShape(prevPoint);
            if(clickedShape!=null)
                if (start.X == 0 && start.Y == 0) start = prevPoint;      //нужна какая-то другая проверка, чтобы старт ставился только в начале (счетчик count-?)
            previewCanvas_MouseMove(sender, e);
        }

        private void previewCanvas_MouseMove(object sender, MouseEventArgs e) {
            point = (Point)e.GetPosition(previewCanvas);
            if (!GlobalState.PressLeftButton) return;
            TranslatingRedrawing(clickedShape, e);
            prevPoint = point;
        }

        private void previewCanvas_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            GlobalState.PressLeftButton = false;
        }

        private void previewCanvas_MouseLeave(object sender, MouseEventArgs e) {
            GlobalState.PressLeftButton = false;
        }

        IShape GetClickedShape(Point clickPoint) {
            foreach (var item in ((VectorLayer)layer).Picture.shapes) {
                if (item is Rectangle) {
                    if (((Math.Abs(clickPoint.X - ((Rectangle)item).Center.X) < ((Rectangle)item).Size.Width / 4) &&    //вместо 20 потом придется скалировать пропорционально размерам фигуры
                        (Math.Abs(clickPoint.Y - ((Rectangle)item).Center.Y) < ((Rectangle)item).Size.Height / 4)))
                        return item;
                }
                else if (item is Ellipse) {
                    if (((Math.Abs(clickPoint.X - ((Ellipse)item).Center.X) < ((Ellipse)item).Size.Width / 4) &&
                        (Math.Abs(clickPoint.Y - ((Ellipse)item).Center.Y) < ((Ellipse)item).Size.Width / 4)))
                        return item;
                }
            }
            return null;
        }


        void TranslatingRedrawing(IShape shape, MouseEventArgs e) {           
            point = e.GetPosition(previewCanvas);
            if (clickedShape != null) {             
                ((VectorLayer)layer).Picture.Transform(new TranslateTransformation(new Point(point.X - prevPoint.X, point.Y - prevPoint.Y)));
                TranslateVector.Text = $"( {(int)(point.X-start.X)} ; {(int)(point.Y-start.Y)} )";
                Refresh();
            }
        }

        private void Refresh() {
            previewCanvas.Children.Clear();
                foreach(IShape item in ((VectorLayer)layer).Picture.shapes) {
                    item.Draw(previewCanvas);
                }
                //TODO: РАСТР...
        }

        void SaveIntoLayer(ILayer layer, IShape shape) {
            if (layer is VectorLayer) {
                ((VectorLayer)layer).Picture.shapes.Add(shape);
            }
        }

        private void ApplyTransform_Click(object sender, RoutedEventArgs e) {
            transList.Clear();
            Point translateVector = new Point();
            double scaleFactor;
            double angle;
            int time;   
            try {
                if (TranslateVector.Text == "" && ScaleFactor.Text == "" && Angle.Text == "") throw new IOException("You haven't created any transformation");
                if (TranslateVector.Text != "") {
                    if (TranslateTime.Text == "") throw new IOException("Enter all fields in the Translate section");
                    string [] coords =TranslateVector.Text.Split(new char[] { ';','(',')' }, StringSplitOptions.RemoveEmptyEntries);
                    translateVector.X=Int32.Parse(coords[0]);
                    translateVector.Y = Int32.Parse(coords[1]);
                    time = Int32.Parse(TranslateTime.Text);
                    transList.Add(new Tuple<Transformation, int>(new TranslateTransformation(translateVector), time));
                }
                if(Angle.Text!="") {
                    if (RotateTime.Text == "" || RotatePoint.Text == "") throw new IOException("Enter all fields in the Rotate section");
                    angle = double.Parse(Angle.Text);
                    time = Int32.Parse(RotateTime.Text);
                        //первым параметром передавать что-то (хз что, т.к центров много: все будет двигаться при скейле)
                    transList.Add(new Tuple<Transformation, int>(new RotateTransformation(new Point(0, 0), angle), time));  
                }
                if(ScaleFactor.Text!="") {
                    if (ScaleTime.Text == "" || ScalePoint.Text == "") throw new IOException("Enter all fields in the Scale section");
                    scaleFactor = double.Parse(ScaleFactor.Text);
                    time = Int32.Parse(ScaleTime.Text);
                    transList.Add(new Tuple<Transformation, int>(new ScaleTransformation(new Point(0, 0), scaleFactor), time));    //аналогично
                }
                this.Hide();
                
            }
            catch(IOException ioEx) {
                MessageBox.Show(ioEx.Message);
            }
            //убрал отлов всех исключений для тестирования
            //catch(Exception ex) {                       
            //    MessageBox.Show("Непредвиденная ошибка.");
            //}
        }

        private void TranslateTime_TextChanged(object sender, TextChangedEventArgs e) {
            int symb;
            if(!Int32.TryParse(TranslateTime.Text,out symb)) {
                TranslateTime.Clear();
            }
        }

        private void Angle_TextChanged(object sender, TextChangedEventArgs e) {
            double symb;
            if (!double.TryParse(Angle.Text, out symb)) {
                Angle.Clear();
            }
        }

        private void RotateTime_TextChanged(object sender, TextChangedEventArgs e) {
            int symb;
            if (!Int32.TryParse(RotateTime.Text, out symb)) {
                RotateTime.Clear();
            }
        }

        private void ScaleFactor_TextChanged(object sender, TextChangedEventArgs e) {
            double symb;
            if (!double.TryParse(ScaleFactor.Text, out symb)) {
                ScaleFactor.Clear();
            }
        }

        private void ScaleTime_TextChanged(object sender, TextChangedEventArgs e) {
            int symb;
            if (!Int32.TryParse(ScaleTime.Text, out symb)) {
                ScaleTime.Clear();
            }
        }
    }
}