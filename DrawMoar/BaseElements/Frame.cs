using System;
using System.Linq;
using System.Collections.Generic;

using System.Text.RegularExpressions;

using DrawMoar.Extensions;
using System.Drawing;

namespace DrawMoar.BaseElements
{
    public class Frame
    {
        /// <summary>
        /// Название (имя) кадра
        /// </summary>
        private string name;
        public string Name {
            get { return name; }
            private set {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Frame name must contain only letters and numbers.");
                }
            }
        }


        /// <summary>
        /// Текущий слой.
        /// </summary>
        public ILayer CurrentLayer { get; set; }


        /// <summary>
        /// Список слоёв кадра.
        /// </summary>
        private List<ILayer> layers = new List<ILayer>();


        public Frame() {
            name = "newFrame";
            layers.Add(new VectorLayer("Vector_Layer_0"));
            CurrentLayer = layers.Last();
        }


        public Frame(string name) {
            this.name = name;
            layers.Add(new VectorLayer("Vector_Layer_0"));
            CurrentLayer = layers.Last();
        }


        #region Методы для работы со слоями.


        /// <summary>
        /// Получить все слои кадра
        /// </summary>
        /// <returns>Список всех слоев кадра</returns>
        public List<ILayer> GetAllLayers() {
            return layers;
        }


        /// <summary>
        /// Добавление пустого РАСТРОВОГО слоя в конец списка.
        /// </summary>
        public void AddEmptyRasterLayer() {
            layers.Add(new RasterLayer($"layer_{layers.Count}"));
            CurrentLayer = layers.Last();
        }


        /// <summary>
        /// Добавление пустого ВЕКТОРНОГО слоя в конец списка.
        /// </summary>
        public void AddEmptyVectorLayer() {
            layers.Add(new VectorLayer($"layer_{layers.Count}"));
            CurrentLayer = layers.Last();
        }


        /// <summary>
        /// Добавление существующего слоя в конец списка.
        /// </summary>
        /// <param name="layer">Слой, который хотите добавить</param>
        public void AddLayer(ILayer layer) {
            layers.Add(layer);
            CurrentLayer = layers.Last();
        }


        /// <summary>
        /// Добавление КОПИИ существующего слоя в конец списка.
        /// </summary>
        /// <param name="layer">Слой, который хотите добавить</param>
        public void AddCopyOfLayer(ILayer layer) {
            layers.Add(ObjectCopier.Clone(layer));
            layers.Last().Name += "_Copy";
            CurrentLayer = layers.Last();
        }


        /// <summary>
        /// Вставка слоя в список на указанную позицию.
        /// </summary>
        /// <param name="index">Куда втавить слой</param>
        /// <param name="layer">Сам слой, который вставить</param>
        public void InsertLayer(int index, ILayer layer) {
            layers.Insert(index,layer);
            CurrentLayer = layers[index];
        }


        /// <summary>
        /// Вставка КОПИИ слоя в список на указанную позицию.
        /// </summary>
        /// <param name="index">Куда втавить слой</param>
        /// <param name="layer">Сам слой, который вставить</param>
        public void InsertCopyOfLayer(int index, ILayer layer) {
            layers.Insert(index, ObjectCopier.Clone(layer));
            layers[index].Name += "_Copy";
            CurrentLayer = layers[index];
        }


        /// <summary>
        /// Вставка на указанную позицию пустого растрового слоя
        /// </summary>
        /// <param name="index">Куда вставить пустой растровый слой</param>
        public void InsertEmptyRasterLayer(int index) {
            layers.Insert(index, new RasterLayer($"layer_{layers.Count}"));
            CurrentLayer = layers[index];
        }


        /// <summary>
        /// Вставка на указанную позицию пустого векторного слоя
        /// </summary>
        /// <param name="index">Куда вставить пустой векторный слой</param>
        public void InsertEmptyVectorLayer(int index) {
            layers.Insert(index, new VectorLayer($"layer_{layers.Count}"));
            CurrentLayer = layers[index];
        }


        /// <summary>
        /// Получение позиции (индекса) слоя в списке, если он в нём есть
        /// </summary>
        /// <param name="layer">Слой, индекс которого вернуть</param>
        /// <returns>Индекс этого слоя.</returns>
        public int IndexOfLayer(ILayer layer) {
            return layers.IndexOf(layer);
        }


        /// <summary>
        /// Получение слоя по его индексу в списке
        /// </summary>
        /// <param name="index">Индекс слоя</param>
        /// <returns>Слой с переданным индексом</returns>
        public ILayer GetLayer(int index) {
            return layers[index];
        }


        /// <summary>
        /// Получение КОПИИ слоя по его индексу в списке
        /// </summary>
        /// <param name="index">Индекс слоя</param>
        /// <returns>Слой с переданным индексом</returns>
        public ILayer GetCopyOfLayer(int index) {
            return ObjectCopier.Clone(layers[index]);
        }


        /// <summary>
        /// Удаление слоя из списка, если из списка удалили последний слой, то добавляем на его место новый пустой растровый слой
        /// CurrentLayer смещается на предыдущий(до удаляемого) слой, либо на 0-ой, если был удалён нулевой
        /// </summary>
        /// <param name="layer">Удаляемый слой</param>
        public void RemoveLayer(ILayer layer) {
            var index = layers.IndexOf(layer);
            if (layers.Remove(layer)) {
                if (layers.Count == 0) {
                    layers.Add(new RasterLayer("Layer_0"));
                    CurrentLayer = layers.First();
                }
                else if (index == 0) {
                    CurrentLayer = layers.First();
                }
                else {
                    CurrentLayer = layers[index - 1];
                }
            }
        }


        /// <summary>
        /// Удаление слоя по индексу, если удалён последний в списке, добавляем новый пустой растровый
        /// CurrentLayer смещается на предыдущий(до удаляемого) слой, либо на 0-ой, если был удалён нулевой
        /// </summary>
        /// <param name="index">Индекс удаляемого слоя.</param>
        public void RemoveLayerAt(int index) {
            layers.RemoveAt(index);
            if (layers.Count == 0) {
                layers.Add(new RasterLayer("Layer_0"));
                CurrentLayer = layers.First();
            }
            else if (index == 0) {
                CurrentLayer = layers.First();
            }
            else {
                CurrentLayer = layers[index - 1];
            }
        }


        /// <summary>
        /// Изменение порядка слоёв
        /// </summary>
        /// <param name="firstLayerIndex">Индекс первого слоя</param>
        /// <param name="secondLayerIndex">Индекс второго слоя</param>
        public void SwapLayersPositions(int firstLayerIndex, int secondLayerIndex) {
            layers.Insert(secondLayerIndex + 1, layers[firstLayerIndex]);
            var tmp = layers[secondLayerIndex];
            layers.RemoveAt(secondLayerIndex);
            layers.RemoveAt(firstLayerIndex);
            layers.Insert(firstLayerIndex, tmp);
        }


        /// <summary>
        /// Поднятие слоя вверх в списке на подну позицию
        /// </summary>
        /// <param name="index">Индекс слоя</param>
        public void PutLayerUp(int index) {
            if (index >= 0 && index < layers.Count - 1) { // Нужна ли проверка?
                layers.Insert(index + 2, layers[index]);
                layers.RemoveAt(index);
            }
        }


        /// <summary>
        /// Опускание слоя вниз в списке на одну позицию
        /// </summary>
        /// <param name="index">Индекс слоя</param>
        public void PutFrameDown(int index) {
            if (index > 0 && index < layers.Count) {
                layers.Insert(index - 1, layers[index]);
                layers.RemoveAt(index + 1);
            }
        }


        //// TODO: Переписать MergeLayers на 1) произвольное количество 2) когда буду точно знать как это вот всё хранить
        //// объединение текущего слоя с предыдущим если indexLayer >=1, иначе кидаем исключение
        //public void MergeLayers(int indexLayer) {
        //    if (indexLayer > 0) {
        //        // Если оба слоя растровые
        //        if ((layers[indexLayer] is RasterLayer) && (layers[indexLayer - 1] is RasterLayer)) {

        //            using (Graphics gr = Graphics.FromImage(((RasterLayer)layers[indexLayer]).Image)) {
        //                gr.DrawImage(((RasterLayer)layers[indexLayer - 1]).Image, new Point(0, 0));
        //            }
        //            layers.RemoveAt(indexLayer);
        //            layers.RemoveAt(indexLayer);
        //            layers.Insert(indexLayer - 1, new RasterLayer(((RasterLayer)layers[indexLayer - 1]).Image));
        //        }
        //    }
        //    else {
        //        throw new ArgumentException("Переданный параметр indexLayer не может быть <= 0");
        //    }
        //}
        #endregion

        
        public System.Drawing.Bitmap Join() {
            var bm = new Bitmap(450, 450);
            Graphics g = Graphics.FromImage(bm);
            foreach (var l in layers) {
                g.DrawImage(l.GetImage(450,450), 0, 0);
                g.Dispose();
            }
            return bm;
        }


        public void SaveToPicture() {
            //TODO: СРОЧНО ЭКСПОРТ
        }


        /// <summary>
        /// TODO: Переписать нормально крч, создание bitmap из всех видимых слоёв кадра, типа склеивает все в один
        /// </summary>
        /// <returns>bitmap</returns>
        //public Bitmap GetBitmap() {
        //    Bitmap result = new Bitmap(Width, Height, PixelFormat.Format32bppArgb); // наша новая картинка
        //    var graphics = Graphics.FromImage(result);
        //    graphics.CompositingMode = CompositingMode.SourceOver;
        //    foreach(var layer in layers) {
        //        if (layer.Visible) {
        //            graphics.DrawImage(layer.GetBitmap(), 0, 0); 
        //        }
        //    }
        //    return result;
        //}


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