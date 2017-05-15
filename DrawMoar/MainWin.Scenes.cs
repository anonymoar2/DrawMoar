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
            SavePrev();
            if (cartoon == null) {
                return;
            }
            if (sender != null) {
                cartoon.scenes.Add(new Scene());
            }
            Cartoon.CurrentScene = cartoon.scenes.Last();
            Cartoon.CurrentFrame = Cartoon.CurrentScene.frames.Last();
            Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations.Last();
            AddListBoxElement(scenesList, Cartoon.CurrentScene.Name);
        }

        private void scenesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            framesList.Items.Clear();
            if (scenesList.SelectedIndex != -1)
                Cartoon.CurrentScene = cartoon.scenes[scenesList.SelectedIndex];
            var frames = Cartoon.CurrentScene.frames;
            foreach (var item in frames) {
                AddListBoxElement(framesList, item.Name);
            }
            framesList.SelectedIndex = 0;
        }

        private void DeleteScene_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (cartoon == null) return;
            int index = scenesList.SelectedIndex;
            scenesList.Items.RemoveAt(index);
            cartoon.scenes.RemoveAt(index);
            if (cartoon.scenes.Count == 0) {
                cartoon.scenes.Add(new Scene());
                AddListBoxElement(scenesList, Cartoon.CurrentScene.Name);
            }
            scenesList.SelectedIndex = index > 0 ? index - 1 : 0;
            Cartoon.CurrentScene = index > 0 ? cartoon.scenes[index - 1] : cartoon.scenes[0];
            Cartoon.CurrentFrame = Cartoon.CurrentScene.frames[0];
            Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations[0];
            Refresh();
        }
    }
}
