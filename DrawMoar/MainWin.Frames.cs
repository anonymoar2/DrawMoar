using DrawMoar.BaseElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DrawMoar {
    public partial class MainWindow:Window {

        private void AddFrame_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (Editor.cartoon == null) {
                return;
            }
            if (sender != null) {
                var newDurationFrameDialog = new DurationFrameDialog(framesList);
                newDurationFrameDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                newDurationFrameDialog.Owner = this;
                newDurationFrameDialog.Show();
            }
        }


        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (framesList.SelectedIndex != -1) {
                if (Editor.cartoon.CurrentFrame.animations.Count > 0)
                    Editor.cartoon.CurrentFrame = Editor.cartoon.CurrentScene.frames[framesList.SelectedIndex];
                Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentFrame.animations.Last();
            }
            layersList.Items.Clear();
            canvas.Children.Clear();
            var lays = Editor.cartoon.CurrentFrame.animations;
            foreach (var item in lays) {
                AddListBoxElement(layersList, item.layer[0].Name);
                item.layer[Cartoon.CurrentFrame.stateNumbers[Cartoon.CurrentFrame.animations.IndexOf(item)]].Draw(canvasDrawer);
                layersList.SelectedIndex = 0;
            }
        }

        private void DeleteFrame_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (Editor.cartoon == null) return;
            int index = framesList.SelectedIndex;
            framesList.Items.RemoveAt(index);
            var frames = Editor.cartoon.CurrentScene.frames;
            frames.RemoveAt(index);
            if (frames.Count == 0) {
                frames.Add(new BaseElements.Frame());
                AddListBoxElement(framesList, Editor.cartoon.CurrentFrame.Name);
            }
            framesList.SelectedIndex = index > 0 ? index - 1 : 0;
            Editor.cartoon.CurrentFrame = index > 0 ? frames[index - 1] : frames[0];
            Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentFrame.animations[0];
            Refresh();
        }
    }
}
