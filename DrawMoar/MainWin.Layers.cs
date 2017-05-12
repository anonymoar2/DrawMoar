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
                Cartoon.CurrentLayer = Cartoon.CurrentFrame.layers[layersList.SelectedIndex];
            }

        }


        private void AddRasterLayer_Click(object sender, RoutedEventArgs e) {
            SavePrev();
            if (cartoon == null) {
                return;
            }
            //проверка на то, выделен ли какой-либо кадр(когда реализуем удаление)
            if (sender != null) {
                Cartoon.CurrentFrame.layers.Add(new Tuple<ILayer, List<Transformation>, int>(new RasterLayer(), new List<Transformation>(), 0));
            }
            Cartoon.CurrentLayer = Cartoon.CurrentFrame.layers.Last();
            var layers = Cartoon.CurrentFrame.layers;
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
                Cartoon.CurrentFrame.layers.Add(new Tuple<ILayer, List<Transformation>, int>(new VectorLayer(), new List<Transformation>(), 0));
            }
            Cartoon.CurrentLayer = Cartoon.CurrentFrame.layers.Last();
            var layers = Cartoon.CurrentFrame.layers;
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
            var layerToDelete = Cartoon.CurrentFrame.layers[index].Item1;
            layersList.Items.RemoveAt(index);
            var layers = Cartoon.CurrentFrame.layers;
            layers.RemoveAt(index);
            if (layers.Count == 0) {
                layers.Add(new Tuple<ILayer, List<Transformation>, int>(new VectorLayer(), new List<Transformation>(), 0));
                AddListBoxElement(layersList, Cartoon.CurrentLayer.Item1.Name);
            }
            layersList.SelectedIndex = layersList.Items.Count > 1 ? index - 1 : 0;
            Cartoon.CurrentLayer = index > 0 ? layers[index - 1] : layers[0];
            Refresh();
        }
    }
}
