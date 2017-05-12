using DrawMoar.BaseElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DrawMoar {
    public partial class MainWindow : Window {

        private void AddScene_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                cartoon.scenes.Add(new Scene());
            }
            GlobalState.CurrentScene = cartoon.scenes.Last();
            GlobalState.CurrentFrame = GlobalState.CurrentScene.frames.Last();
            GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
            AddListBoxElement(scenesList, GlobalState.CurrentScene.Name);
        }

        private void scenesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            framesList.Items.Clear();
            if (scenesList.SelectedIndex != -1)
                GlobalState.CurrentScene = cartoon.scenes[scenesList.SelectedIndex];
            var frames = GlobalState.CurrentScene.frames;
            foreach (var item in frames) {
                AddListBoxElement(framesList, item.Name);
            }
            framesList.SelectedIndex = 0;
        }

        private void DeleteScene_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) return;
            int index = scenesList.SelectedIndex;
            scenesList.Items.RemoveAt(index);
            cartoon.scenes.RemoveAt(index);
            if (cartoon.scenes.Count == 0) {
                cartoon.scenes.Add(new Scene());
                AddListBoxElement(scenesList, GlobalState.CurrentScene.Name);
            }
            scenesList.SelectedIndex = index > 0 ? index - 1 : 0;
            GlobalState.CurrentScene = index > 0 ? cartoon.scenes[index - 1] : cartoon.scenes[0];
            GlobalState.CurrentFrame = GlobalState.CurrentScene.frames[0];
            GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers[0];
            Refresh();
        }
    }
}
