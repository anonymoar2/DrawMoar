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
            if(animationsList.SelectedIndex!=-1)
                Editor.cartoon.CurrentAnimation = Editor.cartoon.CurrentFrame.animations[animationsList.SelectedIndex];
            canvas.Children.Clear();
            layersList.Items.Clear();
            foreach (var item in Editor.cartoon.CurrentAnimation.layers) {
                item.Draw(canvasDrawer);
                AddListBoxElement(layersList, item.Name);
            }
        }

        private void AddAnimation_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (Editor.cartoon == null) {
                return;
            }
            if (sender != null) {
                Editor.cartoon.CurrentFrame.animations.Add(new Animation(new VectorLayer(),new List<Transformation>()));
                AddListBoxElement(animationsList, Editor.cartoon.CurrentAnimation.Name);
            }            
            Editor.cartoon.CurrentAnimation = Editor.cartoon.CurrentFrame.animations.Last();
            Editor.cartoon.CurrentLayer = Editor.cartoon.CurrentAnimation.layers.Last();            
        }

        private void DeleteAnimation_Click(object sender, RoutedEventArgs e) {

        }
    }
}
