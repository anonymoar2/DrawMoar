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
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                Cartoon.CurrentScene.frames.Add(new BaseElements.Frame());
                Cartoon.CurrentFrame = Cartoon.CurrentScene.frames.Last();
                Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations.Last();
                var frames = Cartoon.CurrentScene.frames;
                AddListBoxElement(framesList, Cartoon.CurrentFrame.Name);
            }
        }


        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (framesList.SelectedIndex != -1) {
                if (Cartoon.CurrentFrame.animations.Count > 0)
                    Cartoon.CurrentFrame = Cartoon.CurrentScene.frames[framesList.SelectedIndex];
                Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations.Last();
            }
            layersList.Items.Clear();
            canvas.Children.Clear();
            var lays = Cartoon.CurrentFrame.animations;
            foreach (var item in lays) {
                AddListBoxElement(layersList, item.layer.Name);
                item.layer.Draw(canvasDrawer);
                layersList.SelectedIndex = 0;
            }
        }

        private void DeleteFrame_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (cartoon == null) return;
            int index = framesList.SelectedIndex;
            framesList.Items.RemoveAt(index);
            var frames = Cartoon.CurrentScene.frames;
            frames.RemoveAt(index);
            if (frames.Count == 0) {
                frames.Add(new BaseElements.Frame());
                AddListBoxElement(framesList, Cartoon.CurrentFrame.Name);
            }
            framesList.SelectedIndex = index > 0 ? index - 1 : 0;
            Cartoon.CurrentFrame = index > 0 ? frames[index - 1] : frames[0];
            Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations[0];
            Refresh();
        }
    }
}
