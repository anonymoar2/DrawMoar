using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DrawMoar {
    public partial class MainWindow :Window {


        private void layersList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (layersList.SelectedIndex != -1) {
                Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations[layersList.SelectedIndex];
            }

        }


        private void AddRasterLayer_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (cartoon == null) {
                return;
            }
            //проверка на то, выделен ли какой-либо кадр(когда реализуем удаление)
            if (sender != null) {
                Cartoon.CurrentFrame.animations.Add(new Animation(new RasterLayer(), new List<Transformation>()));
            }
            Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations.Last();
            var layers = Cartoon.CurrentFrame.animations;
            AddListBoxElement(layersList, $"RasterLayer_{layers.Count - 1}");
        }


        private void AddVectorLayer_Click(object sender, RoutedEventArgs e) {
            if (cartoon == null) {
                return;
            }
            if (sender == null) {
                layersList.Items.Clear();
            }
            else {
                Cartoon.CurrentFrame.animations.Add(new Animation(new VectorLayer(), new List<Transformation>()));
            }
            Cartoon.CurrentLayer = Cartoon.CurrentFrame.animations.Last();
            var layers = Cartoon.CurrentFrame.animations;
            AddListBoxElement(layersList, $"VectorLayer_{layers.Count - 1}");
        }

        void SaveIntoLayer(ILayer layer, IShape shape) {
            if (layer is VectorLayer) {
                ((VectorLayer)layer).Picture.shapes.Add(shape);
            }
        }

        private void DeleteLayer_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (cartoon == null) return;
            int index = layersList.SelectedIndex;
            var layerToDelete = Cartoon.CurrentFrame.animations[index].layer;
            layersList.Items.RemoveAt(index);
            var layers = Cartoon.CurrentFrame.animations;
            layers.RemoveAt(index);
            if (layers.Count == 0) {
                layers.Add(new Animation(new VectorLayer(), new List<Transformation>()));
                AddListBoxElement(layersList, Cartoon.CurrentLayer.layer.Name);
            }
            layersList.SelectedIndex = layersList.Items.Count > 1 ? index - 1 : 0;
            Cartoon.CurrentLayer = index > 0 ? layers[index - 1] : layers[0];
            Refresh();
        }
    }
}
