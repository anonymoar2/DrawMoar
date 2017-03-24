using DrawMoar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;


namespace DrawMoar.BaseElements
{
    public class Scene
    {
        /// <summary>
        /// Текущий кадр
        /// </summary>
        public Frame CurrentFrame { get; set; }


        /// <summary>
        /// Название (имя) сцены
        /// </summary>
        private string name;
        public string Name {
            get { return name; }
            set {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Frame name must contain only letters and numbers.");
                }
            }
        }


        public Scene() {
            name = "newScene";
            frames.Add(new Frame("Frame_0"));
            CurrentFrame = frames.Last();
        }


        public Scene(string name) {
            this.name = name;
            frames.Add(new Frame());
            CurrentFrame = frames.Last();
        }


        /// <summary>
        /// Список кадров сцены
        /// </summary>
        private List<Frame> frames = new List<Frame>();


        private List<ILayer> savedLayers = new List<ILayer>();


        #region Методы для работы с кадрами


        /// <summary>
        /// Получение списка всех кадров сцены.
        /// </summary>
        /// <returns>Список кадров.</returns>
        public List<Frame> GetAllFrames() {
            return frames;
        }


        /// <summary>
        /// Добавление пустого кадра в конец списка.
        /// </summary>
        public void AddEmptyFrame() {
            frames.Add(new Frame());
            CurrentFrame = frames.Last();
        }


        /// <summary>
        /// Добавление существующего кадра в конец списка.
        /// </summary>
        /// <param name="frame">Кадр, который хотите добавить</param>
        public void AddFrame(Frame frame) {
            frames.Add(frame);
            CurrentFrame = frames.Last();
        }


        /// <summary>
        /// Добавление КОПИИ существующего кадра в конец списка.
        /// </summary>
        /// <param name="frame">Кадр, который хотите добавить</param>
        public void AddCopyOfFrame(Frame frame) {
            frames.Add(ObjectCopier.Clone(frame));
            CurrentFrame = frames.Last();
        }


        /// <summary>
        /// Вставка  кадра в список на указанную позицию.
        /// </summary>
        /// <param name="index">Куда втавить кадр</param>
        /// <param name="frame">Сам кадр</param>
        public void InsertFrame(int index, Frame frame) {
            frames.Insert(index, frame);
            CurrentFrame = frames[index];
        }


        /// <summary>
        /// Вставка КОПИИ кадра в список на указанную позицию.
        /// </summary>
        /// <param name="index">Куда втавить кадр</param>
        /// <param name="frame">Сам кадр</param>
        public void InsertCopyOfFrame(int index, Frame frame) {
            frames.Insert(index, ObjectCopier.Clone(frame));
            CurrentFrame = frames[index];
        }


        /// <summary>
        /// Вставка на указанную позицию пустого кадра
        /// </summary>
        /// <param name="index">Куда вставить пустой кадр</param>
        public void InsertEmptyFrame(int index) {
            frames.Insert(index, new Frame());
            CurrentFrame = frames[index];
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
        /// Получение КОПИИ кадра по его индексу в списке
        /// </summary>
        /// <param name="index">Индекс кадра</param>
        /// <returns>Кадр с переданным индексом</returns>
        public Frame GetCopyOfFrame(int index) {
            return ObjectCopier.Clone(frames[index]);
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


        #endregion


        #region Методы для работы с сохраненными слоями


        /// <summary>
        /// Получение списка всех сохранённых слоёв сцены.
        /// </summary>
        /// <returns>Список сохраненных слоёв.</returns>
        public List<ILayer> GetAllSavedLayers() {
            return savedLayers;
        }


        /// <summary>
        /// Добавление слоя в конец списка. 
        /// (Добавляется КОПИЯ этого слоя:  при изменении слоя извне, в сохраненных его копия не поменяется)
        /// </summary>
        /// <param name="layer">Слой, который хотите добавить</param>
        public void AddSavedLayer(ILayer layer) {
            savedLayers.Add(ObjectCopier.Clone(layer));
        }


        /// <summary>
        /// Вставка  слоя в список на указанную позицию.
        /// (Добавляется КОПИЯ этого слоя:  при изменении слоя извне, в сохраненных его копия не поменяется)
        /// </summary>
        /// <param name="index">Куда втавить слой</param>
        /// <param name="layer">Сам слой</param>
        public void InsertSavedLayer(int index, ILayer layer) {
            savedLayers.Insert(index, ObjectCopier.Clone(layer));
        }


        /// <summary>
        /// Получение позиции (индекса) слоя в списке
        /// </summary>
        /// <param name="layer">Слой, индекс которого вернуть</param>
        /// <returns>Индекс этого слоя.</returns>
        public int IndexOfFrame(ILayer layer) {
            return savedLayers.IndexOf(layer);
        }


        /// <summary>
        /// Получение КОПИИ по его индексу в списке
        /// (Возращается КОПИЯ этого слоя:  при изменении вернувшегося слоя извне, в сохраненных он не поменяется)
        /// </summary>
        /// <param name="index">Индекс слоя</param>
        /// <returns>Слой с переданным индексом</returns>
        public ILayer GetSavedLayers(int index) {
            return ObjectCopier.Clone(savedLayers[index]);
        }


        /// <summary>
        /// Удаление слоя из списка, если из списка удалили последний слой, то список пустой остаётся
        /// </summary>
        /// <param name="layer">Удаляемый слой</param>
        public void RemoveSavedLayer(ILayer savedLayer) {
            var index = savedLayers.IndexOf(savedLayer);
            savedLayers.Remove(savedLayer);
        }


        /// <summary>
        /// Удаление слоя по индексу
        /// </summary>
        /// <param name="index">Индекс удаляемого слоя.</param>
        public void RemoveSavedLayerAt(int index) {
            savedLayers.RemoveAt(index);
        }


        /// Пока не вижу смысла в изменении порядка сохраненных слоёв
        /// Но потом если надо будет сделаю
        ///// <summary>
        ///// Изменение порядка кадров
        ///// </summary>
        ///// <param name="firstFrameIndex">Индекс первого кадра</param>
        ///// <param name="secondFrameIndex">Индекс второго кадра</param>
        //public void SwapFramesPositions(int firstFrameIndex, int secondFrameIndex) {
        //    frames.Insert(secondFrameIndex + 1, frames[firstFrameIndex]);
        //    var tmp = frames[secondFrameIndex];
        //    frames.RemoveAt(secondFrameIndex);
        //    frames.RemoveAt(firstFrameIndex);
        //    frames.Insert(firstFrameIndex, tmp);
        //}


        ///// <summary>
        ///// Поднятие кадра вверх в списке на подну позицию
        ///// </summary>
        ///// <param name="index">Индекс кадра</param>
        //public void PutFrameUp(int index) {
        //    if (index >= 0 && index < frames.Count - 1) {
        //        frames.Insert(index + 2, frames[index]);
        //        frames.RemoveAt(index);
        //    }
        //}


        ///// <summary>
        ///// Опускание кадра вниз в списке на одну позицию
        ///// </summary>
        ///// <param name="index">Индекс кадра</param>
        //public void PutFrameDown(int index) {
        //    if (index > 0 && index < frames.Count) {
        //        frames.Insert(index - 1, frames[index]);
        //        frames.RemoveAt(index + 1);
        //    }
        //}

        #endregion
    }
}