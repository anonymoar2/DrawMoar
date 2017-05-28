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

        private void RefreshScenes() {
            scenesList.Items.Clear();
            foreach (var item in Editor.cartoon.scenes) {
                Editor.cartoon.CurrentScene = Editor.cartoon.scenes.Last();
                Editor.cartoon.CurrentFrame = Editor.cartoon.CurrentScene.frames.Last();
                Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentFrame.animations.Last();
                AddListBoxElement(scenesList, item.Name);
            }           
        }

        private void AddScene_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (Editor.cartoon == null) {
                return;
            }
            if (sender != null) {
                Editor.cartoon.scenes.Add(new Scene());
            }
            Editor.cartoon.CurrentScene = Editor.cartoon.scenes.Last();
            Editor.cartoon.CurrentFrame = Editor.cartoon.CurrentScene.frames.Last();
            Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentFrame.animations.Last();
            AddListBoxElement(scenesList, Editor.cartoon.CurrentScene.Name);
        }

        private void scenesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            framesList.Items.Clear();
            if (scenesList.SelectedIndex != -1)
                Editor.cartoon.CurrentScene = Editor.cartoon.scenes[scenesList.SelectedIndex];
            var frames = Editor.cartoon.CurrentScene.frames;
            foreach (var item in frames) {
                AddListBoxElement(framesList, $"{item.Name} \n {item.duration} sec");
            }
            framesList.SelectedIndex = 0;
        }

        private void DeleteScene_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (Editor.cartoon == null) return;
            int index = scenesList.SelectedIndex;
            scenesList.Items.RemoveAt(index);
            Editor.cartoon.scenes.RemoveAt(index);
            if (Editor.cartoon.scenes.Count == 0) {
                Editor.cartoon.scenes.Add(new Scene());
                AddListBoxElement(scenesList, Editor.cartoon.CurrentScene.Name);
            }
            scenesList.SelectedIndex = index > 0 ? index - 1 : 0;
            Editor.cartoon.CurrentScene = index > 0 ? Editor.cartoon.scenes[index - 1] : Editor.cartoon.scenes[0];
            Editor.cartoon.CurrentFrame = Editor.cartoon.CurrentScene.frames[0];
            Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentFrame.animations[0];
            Refresh();
        }
    }
}
