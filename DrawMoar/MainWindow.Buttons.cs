using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using DrawMoar.BaseElements;


namespace DrawMoar
{
    public partial class MainWindow : Window
    {
        private void Lines_Click(object sender, RoutedEventArgs e) {
            CurrentTool = Instrument.Line;
        }


        private void AddEllipse_Click(object sender, RoutedEventArgs e) {
            CurrentTool = Instrument.Ellipse;
        }


        private void AddRectangle_Click(object sender, RoutedEventArgs e) {
            CurrentTool = Instrument.Rectangle;
        }


        private void Brush_Click(object sender, RoutedEventArgs e) {
            CurrentTool = Instrument.Brush;
        }


        private void Arrow_Click(object sender, RoutedEventArgs e) {
            CurrentTool = Instrument.Arrow;
        }

        private void Eraser_Click(object sender, RoutedEventArgs e) {
            CurrentTool = Instrument.Eraser;
        }

        private void AT_Click(object sender, RoutedEventArgs e) {
            ILayer cloneOfCurrent = (ILayer)Cartoon.CurrentLayer.Item1.Clone();
            generationWin = new GenerationDialog(cloneOfCurrent);
            generationWin.Show();
        }

        private void GenerateFrame_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) return;
            if (Cartoon.TotalTime == 0) return;
            Cartoon.CurrentScene.Generate(Cartoon.CurrentFrame, Cartoon.TotalTime);
            scenesList_SelectionChanged(null, null);
            Refresh();
        }


        private void CycleFrame_Click(object sender, RoutedEventArgs e) {
            Cartoon.CurrentScene.Cycle(25);
            scenesList_SelectionChanged(null, null);
            Refresh();
        }

        private void AddAudio(object sender, RoutedEventArgs e) {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog() {
                Filter = "Audio Files|*.mp3;*.wav;*.flac;*.ogg"
            };
            dialog.ShowDialog();
            if (dialog.FileName != "") {
                var pathToAudio = dialog.FileName;
                audio.Text = pathToAudio;
                cartoon.pathToAudio = pathToAudio;
            }
            else MessageBox.Show("You haven't chosen the audio file");
        }
    }
}
