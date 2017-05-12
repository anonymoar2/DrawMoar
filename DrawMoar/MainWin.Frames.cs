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
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                GlobalState.CurrentScene.frames.Add(new BaseElements.Frame());
                GlobalState.CurrentFrame = GlobalState.CurrentScene.frames.Last();
                GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
                var frames = GlobalState.CurrentScene.frames;
                AddListBoxElement(framesList, GlobalState.CurrentFrame.Name);
            }
        }


        private void framesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (framesList.SelectedIndex != -1) {
                if (GlobalState.CurrentFrame.layers.Count > 0)
                    GlobalState.CurrentFrame = GlobalState.CurrentScene.frames[framesList.SelectedIndex];
                GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
            }
            layersList.Items.Clear();
            canvas.Children.Clear();
            var lays = GlobalState.CurrentFrame.layers;
            foreach (var item in lays) {
                AddListBoxElement(layersList, item.Item1.Name);
                item.Item1.Draw(canvasDrawer);
                layersList.SelectedIndex = 0;
            }
        }

        private void DeleteFrame_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) return;
            int index = framesList.SelectedIndex;
            framesList.Items.RemoveAt(index);
            var frames = GlobalState.CurrentScene.frames;
            frames.RemoveAt(index);
            if (frames.Count == 0) {
                frames.Add(new BaseElements.Frame());
                AddListBoxElement(framesList, GlobalState.CurrentFrame.Name);
            }
            framesList.SelectedIndex = index > 0 ? index - 1 : 0;
            GlobalState.CurrentFrame = index > 0 ? frames[index - 1] : frames[0];
            GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers[0];
            Refresh();
        }
    }
}
