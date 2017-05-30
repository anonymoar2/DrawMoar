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
                Editor.cartoon.CurrentAnimation = Editor.cartoon.CurrentFrame.animations.Last();
                Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentAnimation.layers.Last();
            }
            animationsList.Items.Clear();
            canvas.Children.Clear();
            var anims = Editor.cartoon.CurrentFrame.animations;
            foreach (var item in anims) {
                AddListBoxElement(animationsList, item.Name);
                //item.layer[Editor.cartoon.CurrentFrame.stateNumbers[Editor.cartoon.CurrentFrame.animations.IndexOf(item)]].Draw(canvasDrawer);
                animationsList.SelectedIndex = 0;
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
            Editor.cartoon.CurrentAnimation = index > 0 ? Editor.cartoon.CurrentFrame.animations[index-1] : Editor.cartoon.CurrentFrame.animations[0];
            Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentAnimation.layers[0];
            Refresh();
        }
    }
}
