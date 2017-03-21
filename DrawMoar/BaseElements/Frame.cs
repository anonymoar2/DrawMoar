using System;
using System.Linq;
using System.Collections.Generic;

using System.Text.RegularExpressions;


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


        ///Возможно её не будет
        ///// <summary>
        ///// Продолжительность кадра.
        ///// </summary>
        //private float duration;
        //public float Duration {
        //    get {
        //        return duration;
        //    }
        //    set {
        //        if (value > 0) {
        //            duration = value;
        //        }
        //        else {
        //            throw new ArgumentException("Длительность кадра не может быть <= 0");
        //        }
        //    }
        //}


        /// <summary>
        /// TODO: запилить конструктор
        /// </summary>
        public Frame() {
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
            layers.Add(new RasterLayer());
            CurrentLayer = layers.Last();
        }


        /// <summary>
        /// Добавление пустого ВЕКТОРНОГО слоя в конец списка.
        /// </summary>
        public void AddEmptyVectorLayer() {
            layers.Add(new VectorLayer());
            CurrentLayer = layers.Last();
        }


        /// <summary>
        /// Добавление существующего кадра в конец списка.
        /// </summary>
        /// <param name="frame">Кадр, который хотите добавить</param>
        public void AddFrame(Frame frame) {
            frames.Add(frame);
        }


        /// <summary>
        /// Вставка кадра в список на указанную позицию.
        /// </summary>
        /// <param name="index">Куда втавить кадр</param>
        /// <param name="frame">Сам кадр</param>
        public void InsertFrame(int index, Frame frame) {
            frames.Insert(index, frame);
        }


        /// <summary>
        /// Вставка на указанную позицию пустого кадра
        /// </summary>
        /// <param name="index">Куда вставить пустой кадр</param>
        public void InsertEmptyFrame(int index) {
            frames.Insert(index, new Frame());
        }


        /// <summary>
        /// Получение позиции (индекса) кадра в списке
        /// </summary>
        /// <param name="frame">Кадр, индекс которого вернуть</param>
        /// <returns>Индекс этого кадра.</returns>
        public int IndexOfFrame(Frame frame) {
            return frames.IndexOf(frame);
        }


        /// <summary>
        /// Получение кадра по его индексу в списке
        /// </summary>
        /// <param name="index">Индекс кадра</param>
        /// <returns>Кадр с переданным индексом</returns>
        public Frame GetFrame(int index) {
            return frames[index];
        }


        /// <summary>
        /// Удаление кадра из списка, если из списка удалили последний кадр, то добавляем на его место новый пустой кадр
        /// </summary>
        /// <param name="frame">Удаляемый кадр</param>
        public void RemoveFrame(Frame frame) {
            var index = frames.IndexOf(frame);
            if (frames.Remove(frame)) {
                if (frames.Count == 0) {
                    frames.Add(new Frame());
                    CurrentFrame = frames.First();
                }
                else if (index == 0) {
                    CurrentFrame = frames.First();
                }
                else {
                    CurrentFrame = frames[index - 1];
                }
            }
        }


        /// <summary>
        /// Удаление кадра по индексу
        /// </summary>
        /// <param name="index">Индекс удаляемого кадра.</param>
        public void RemoveFrameAt(int index) {
            frames.RemoveAt(index);
            if (frames.Count == 0) {
                frames.Add(new Frame());
                CurrentFrame = frames.First();
            }
            else if (index == 0) {
                CurrentFrame = frames.First();
            }
            else {
                CurrentFrame = frames[index - 1];
            }
        }


        /// <summary>
        /// Изменение порядка кадров
        /// </summary>
        /// <param name="firstFrameIndex">Индекс первого кадра</param>
        /// <param name="secondFrameIndex">Индекс второго кадра</param>
        public void SwapFramesPositions(int firstFrameIndex, int secondFrameIndex) {
            frames.Insert(secondFrameIndex + 1, frames[firstFrameIndex]);
            var tmp = frames[secondFrameIndex];
            frames.RemoveAt(secondFrameIndex);
            frames.RemoveAt(firstFrameIndex);
            frames.Insert(firstFrameIndex, tmp);
        }


        /// <summary>
        /// Поднятие кадра вверх в списке на подну позицию
        /// </summary>
        /// <param name="index">Индекс кадра</param>
        public void PutFrameUp(int index) {
            if (index >= 0 && index < frames.Count - 1) {
                frames.Insert(index + 2, frames[index]);
                frames.RemoveAt(index);
            }
        }


        /// <summary>
        /// Опускание кадра вниз в списке на одну позицию
        /// </summary>
        /// <param name="index">Индекс кадра</param>
        public void PutFrameDown(int index) {
            if (index > 0 && index < frames.Count) {
                frames.Insert(index - 1, frames[index]);
                frames.RemoveAt(index + 1);
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