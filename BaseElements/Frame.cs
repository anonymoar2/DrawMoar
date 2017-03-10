using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseElements.Figures;
using System.Drawing;
using System.Drawing.Imaging;

namespace BaseElements
{
    public class Frame
    {
        private string workingDirectory;
        private List<Layer> layers = new List<Layer>();
        

        private float duration;
        public float Duration {
            get {
                return duration;
            }
            set {
                if(value > 0) {
                    duration = value;
                }
                else {
                    throw new ArgumentException("Длительность кадра не может быть <= 0");
                }
            }
        }


        /// <summary>
        /// It is the subdirectory of cartoon which contain internal important files.
        /// </summary>
        public string WorkingDirectory {
            get {
                return workingDirectory;
            }
            private set {
                if (Directory.Exists(value)) {
                    workingDirectory = value;
                }
                else if (Directory.Exists(Path.GetDirectoryName(value))) {
                    // TODO: handle all possible exceptions here and rethrow ArgumentException.
                    Directory.CreateDirectory(value);
                    workingDirectory = value;
                }
                else {
                    throw new ArgumentException($"Can't open directory \"{value}\".");
                }
            }
        }


        /// TODO: Подумать, взм переделать конструктор
        public Frame(string workingDirectory) {
            WorkingDirectory = workingDirectory;
            // в будущем создавать новую папку в workingdirectore "rasterlayers" и уже туда закидввать этот новый слой и вообще все растровые
            layers.Add(new RasterLayer(/*workingDirectory*/));
        }



        // объединение текущего слоя с предыдущим если indexLayer >=1, иначе кидаем исключение
        public void MergeLayers(int indexLayer) {
            if (indexLayer > 0) {
                // Если оба слоя растровые
                if ((layers[indexLayer] is RasterLayer) && (layers[indexLayer - 1] is RasterLayer)) {

                    using (Graphics gr = Graphics.FromImage(((RasterLayer)layers[indexLayer]).image)) {
                        gr.DrawImage(((RasterLayer)layers[indexLayer - 1]).image, new Point(0, 0));
                    }
                    layers.RemoveAt(indexLayer);
                    layers.RemoveAt(indexLayer);
                    layers.Insert(indexLayer - 1, new RasterLayer(((RasterLayer)layers[indexLayer - 1]).image));
                }
                // Если нижний слой растровый, а верхний векторный
                if ((layers[indexLayer] is RasterLayer) && (layers[indexLayer - 1] is VectorLayer)) {
                    /// TODO: Запилить склейку слоёв
                }
                // Если нижний векторный а верхний растровый
                if ((layers[indexLayer] is VectorLayer) && (layers[indexLayer - 1] is RasterLayer)){
                    /// TODO: Запилить склейку слоёв x2
                }
                if ((layers[indexLayer] is VectorLayer) && (layers[indexLayer - 1] is VectorLayer)) {
                    var layer = new VectorLayer();
                    var firstFigures = ((VectorLayer)layers[indexLayer - 1]).figures;
                    var lastFigures = ((VectorLayer)layers[indexLayer]).figures;
                    layer.figures.AddRange(firstFigures);
                    layer.figures.AddRange(lastFigures);
                    layers.Insert(indexLayer - 1, layer);
                }
            }
            else {
                throw new ArgumentException("Переданный параметр indexLayer не может быть <= 0");
            }
        }


        // Создание нового растрового слоя
        public void AddRasterLayer() {
            layers.Add(new RasterLayer() { Name = $"layer{layers.Count}" });
        }


        // Создание векторного слоя
        public void AddVectorLayer() {
            layers.Add(new VectorLayer() { Name = $"vector{layers.Count}" });
        }


        // Изменение имени слоя
        public void ChangeNameLyer(int index, string layerName) {
            layers[index].Name = layerName;
        }


        // Изменение порядка слоёв
        public void ChangeOrder(int indexOne, int indexTwo) {
            layers.Insert(indexTwo + 1, layers[indexOne]);
            var tmp = layers[indexTwo];
            layers.RemoveAt(indexTwo);
            layers.RemoveAt(indexOne);
            layers.Insert(indexOne, tmp);
        }


        // Поднятие слоя вверх
        public void UpLayer(int index) {
            if (index >= 0 && index < layers.Count - 1) {
                layers.Insert(index + 2, layers[index]);
                layers.RemoveAt(index);
            }
        }


        // Опускание слоя вниз
        public void DownLayer(int index) {
            if (index > 0 && index < layers.Count) {
                layers.Insert(index - 1, layers[index]);
                layers.RemoveAt(index + 1);
            }
        }
    }
}
