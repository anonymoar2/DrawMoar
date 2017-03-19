﻿using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace DrawMoar
{
    public class Frame
    {
        /// <summary>
        /// Название (имя) кадра
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            private set
            {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+"))
                {
                    name = value;
                }
                else
                {
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


        /// <summary>
        /// Продолжительность кадра.
        /// </summary>
        private float duration;
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


        /// <summary>
        /// Ширина типа, нужна толко для метода SaveLayer
        /// </summary>
        private int width;
        public int Width {
            get {
                return width;
            }
            set {
                width = value;
            }
        }


        /// <summary>
        /// Высота кадра, ну и мультика заодно тоже, для SaveLayer
        /// </summary>
        private int height;
        public int Height {
            get {
                return height;
            }
            set {
                height = value;
            }
        }


        /// <summary>
        /// TODO: вернуть классы или подумать ещё тут крч
        /// </summary>
        public Frame(int width, int height) {
            Width = width;
            Height = height;
            layers.Add(new RasterLayer() { Name = "layer_0" }); // По умолчанию всегда создается растровый слой
            CurrentLayer = layers.First();
        }


        /// <summary>
        /// Получить все слои кадра
        /// </summary>
        /// <returns>Список всех слоев кадра</returns>
        public List<ILayer> GetAllLayers()
        {
            return layers;
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
            CurrentLayer = layers.Last();
        }


        public void AddLayer(ILayer layer)
        {
            layers.Add(layer);
            CurrentLayer = layers.Last();
        }

        /// <summary>
        /// Создание нового векторного слоя и добавление его в конец списка слоёв.
        /// </summary>
        public void AddVectorLayer() {
            // WARNING: стоит ли обеспечить уникальность имён слоёв?
            // в многопоточном коде layers.Count может быть одинаковым
            // для двух разных потоков во время вызова этого метода.
            layers.Add(new VectorLayer() { Name = $"vector{layers.Count}" });
            CurrentLayer = layers.Last();
        }


        /// <summary>
        /// Удаление слоя.
        /// </summary>
        /// <param name="layer">Удаляемый с кадра слой.</param>
        public void RemoveLayer(ILayer layer) {
            // WARNING: каким будет поведение если layer отсутствует в layers?
            layers.Remove(layer);
            /// currentlayer
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
                layers.Insert(index + 2, layers[index]);
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

        
        /// <summary>
        /// Создание bitmap из всех видимых слоёв кадра, типа склеивает все в один
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