using DrawMoar.BaseElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DrawMoar {
    partial class MainWindow : Window {

        private void animationsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (animationsList.SelectedIndex != -1) {
                Editor.cartoon.CurrentAnimation = Editor.cartoon.CurrentFrame.animations[animationsList.SelectedIndex];
                Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentAnimation.layers[0];
            }
            layersList.Items.Clear();
            foreach (var item in Editor.cartoon.CurrentAnimation.layers) {
                AddListBoxElement(layersList, item.Name);
            }
        }

        private void AddAnimation_Click(object sender, RoutedEventArgs e) {
            if (Editor.cartoon == null) {
                return;
            }
            if (sender != null) {
                Editor.cartoon.CurrentFrame.animations.Add(new Animation(new VectorLayer("VectorLayer_0"),new List<Transformation>()));
                AddListBoxElement(animationsList, Editor.cartoon.CurrentFrame.animations.Last().Name);
                animationsList.SelectedIndex = animationsList.Items.Count - 1;
                Editor.cartoon.CurrentAnimation = Editor.cartoon.CurrentFrame.animations[animationsList.SelectedIndex];
            }            
            Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentAnimation.layers[0];            
        }

        private void DeleteAnimation_Click(object sender, RoutedEventArgs e) {
            if (Editor.cartoon == null) return;
            int index = animationsList.SelectedIndex;
            animationsList.Items.RemoveAt(index);
            var anims = Editor.cartoon.CurrentFrame.animations;
            anims.RemoveAt(index);           
            if (anims.Count == 0) {
                anims.Add(new Animation("Animation0",new VectorLayer(),new List<Transformation>()));
                Editor.cartoon.CurrentAnimation = anims[0];
                AddListBoxElement(animationsList, Editor.cartoon.CurrentAnimation.Name);
            }
            framesList.SelectedIndex = index > 0 ? index - 1 : 0;
            Editor.cartoon.CurrentAnimation = index > 0 ? anims[index - 1] : anims[0];
            Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentAnimation.layers[0];
            Refresh();
        }
    }
}
