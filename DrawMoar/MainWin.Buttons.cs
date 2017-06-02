using DrawMoar.BaseElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawMoar {
    public partial class MainWindow : Window {


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
            SavePrev();
            ILayer cloneOfCurrent = (ILayer)Editor.cartoon.CurrentAnimation.layers[0].Clone();
            generationWin = new GenerationDialog(cloneOfCurrent);
            generationWin.Show();
        }

        private void GenerateFrame_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (Editor.cartoon == null) return;
            var timeDialog = new GenTimeDialog();
            timeDialog.Owner = this;
            timeDialog.Show();
        }


        private void CycleFrame_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            Editor.cartoon.CurrentScene.Cycle(25);
            scenesList_SelectionChanged(null, null);
            Refresh();
        }

        private void AddAudio(object sender, RoutedEventArgs e) {
            System.Windows.Forms.OpenFileDialog d = new System.Windows.Forms.OpenFileDialog();
            //d.Filter = "Audio Files|*.mp3;*.wav;*.wmp";
            d.Filter = "Audio Files|*.mp3";
            d.ShowDialog();
            if (d.FileName != "") {
                var pathToAudio = d.FileName;
                audio.Text = pathToAudio;
                Editor.cartoon.pathToAudio = pathToAudio;
            }
            else MessageBox.Show("You haven't chosen the audio file");
        }

    }
}
