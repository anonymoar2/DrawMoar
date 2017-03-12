using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace BaseElements
{
    public class Frame
    {
        /// <summary>
        /// Текущий слой.
        /// </summary>
        public Layer CurrentLayer { get; set; }

        /// <summary>
        /// Список слоёв кадра.
        /// </summary>
        private List<Layer> layers = new List<Layer>();

        /// <summary>
        /// Продолжительность кадра.
        /// </summary>
        private float duration;

        /// <summary>
        /// Продолжительность кадра.
        /// </summary>
        public float Duration {
            get {
                return duration;
            }
            set {
                if (value > 0) {
                    duration = value;
                }
                else {
                    throw new ArgumentException("Длительность кадра не может быть <= 0");
                }
            }
        }

        // TODO: Подумать, взм переделать конструктор
        // Пусть в GUI будет задаваться вопрос: Какой слой создавать?
        // Тогда в конструктор пусть передаётся тип слоя и вызывается
        // AddRasterLayer() или AddVectorLayer() в зависимости от ситуации.
        public Frame() {
            layers.Add(new RasterLayer());
            CurrentLayer = layers.First();
        }

        #region Методы для работы со слоями.
        /// <summary>
        /// Создание нового растрового слоя и добавление его в конец списка слоёв.
        /// </summary>
        public void AddRasterLayer() {
            // WARNING: стоит ли обеспечить уникальность имён слоёв?
            // в многопоточном коде layers.Count может быть одинаковым
            // для двух разных потоков во время вызова этого метода.
            layers.Add(new RasterLayer() { Name = $"layer{layers.Count}" });
        }

        /// <summary>
        /// Создание нового векторного слоя и добавление его в конец списка слоёв.
        /// </summary>
        public void AddVectorLayer() {
            // WARNING: стоит ли обеспечить уникальность имён слоёв?
            // в многопоточном коде layers.Count может быть одинаковым
            // для двух разных потоков во время вызова этого метода.
            layers.Add(new VectorLayer() { Name = $"vector{layers.Count}" });
        }

        /// <summary>
        /// Удаление слоя.
        /// </summary>
        /// <param name="layer">Удаляемый с кадра слой.</param>
        public void RemoveLayer(Layer layer) {
            // WARNING: каким будет поведение если layer отсутствует в layers?
            layers.Remove(layer);
        }

        /// <summary>
        /// Изменение имени слоя.
        /// </summary>
        /// <param name="index">Позиция переименуемого слоя.</param>
        /// <param name="layerName">Новое имя слоя.</param>
        public void ChangeLayerName(int index, string layerName) {
            // WARNING: Проверить index
            layers[index].Name = layerName;
        }

        /// <summary>
        /// Изменение порядка слоёв.
        /// </summary>
        /// <param name="firstLayerIndex">Позиция первого слоя.</param>
        /// <param name="secondLayerIndex">Позиция второго слоя.</param>
        public void SwapLayersPositions(int firstLayerIndex, int secondLayerIndex) {
            // WARNING: проверить индексы.
            layers.Insert(secondLayerIndex + 1, layers[firstLayerIndex]);
            var tmp = layers[secondLayerIndex];
            layers.RemoveAt(secondLayerIndex);
            layers.RemoveAt(firstLayerIndex);
            layers.Insert(firstLayerIndex, tmp);
        }

        /// <summary>
        /// Поднятие слоя вверх на одну позицию.
        /// </summary>
        /// <param name="index">Позиция поднимаемого слоя.</param>
        public void PutLayerUp(int index) {
            if (index >= 0 && index < layers.Count - 1) {
                layers.Insert(index + 2, layers[index]); // WARNING: +2 ??
                layers.RemoveAt(index);
            }
        }

        /// <summary>
        /// Опускание слоя вниз на одну позицию.
        /// </summary>
        /// <param name="index">Позиция опускаемого слоя.</param>
        public void PutLayerDown(int index) {
            if (index > 0 && index < layers.Count) {
                layers.Insert(index - 1, layers[index]);
                layers.RemoveAt(index + 1);
            }
        }

        // TODO: Переписать MergeLayers на 1) произвольное количество 2) когда буду точно знать как это вот всё хранить
        // объединение текущего слоя с предыдущим если indexLayer >=1, иначе кидаем исключение
        public void MergeLayers(int indexLayer) {
            if (indexLayer > 0) {
                // Если оба слоя растровые
                if ((layers[indexLayer] is RasterLayer) && (layers[indexLayer - 1] is RasterLayer)) {

                    using (Graphics gr = Graphics.FromImage(((RasterLayer)layers[indexLayer]).Image)) {
                        gr.DrawImage(((RasterLayer)layers[indexLayer - 1]).Image, new Point(0, 0));
                    }
                    layers.RemoveAt(indexLayer);
                    layers.RemoveAt(indexLayer);
                    layers.Insert(indexLayer - 1, new RasterLayer(((RasterLayer)layers[indexLayer - 1]).Image));
                }
                // Если нижний слой растровый, а верхний векторный
                if ((layers[indexLayer] is RasterLayer) && (layers[indexLayer - 1] is VectorLayer)) {
                    /// TODO: Запилить склейку слоёв
                }
                // Если нижний векторный а верхний растровый
                if ((layers[indexLayer] is VectorLayer) && (layers[indexLayer - 1] is RasterLayer)) {
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
        #endregion

        // Использовать только если для каждого фрейма будет своя директория.
        //private string workingDirectory;
        //public string WorkingDirectory {
        //    get {
        //        return workingDirectory;
        //    }
        //    private set {
        //        if (Directory.Exists(value)) {
        //            workingDirectory = value;
        //        }
        //        else if (Directory.Exists(Path.GetDirectoryName(value))) {
        //            // TODO: handle all possible exceptions here and rethrow ArgumentException.
        //            Directory.CreateDirectory(value);
        //            workingDirectory = value;
        //        }
        //        else {
        //            throw new ArgumentException($"Can't open directory \"{value}\".");
        //        }
        //    }
        //}
    }
}
