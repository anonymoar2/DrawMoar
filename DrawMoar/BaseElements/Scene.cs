using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace DrawMoar.BaseElements
{
    public class Scene
    {
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


        /// <summary>
        /// Текущий кадр
        /// </summary>
        public Frame CurrentFrame { get; set; }


        public Scene() {
            name = "new_scene";
            frames.Add(new Frame());
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
        public List<Frame> frames = new List<Frame>();


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
            // TODO: curent
        }


        /// <summary>
        /// Вставка кадра в список на указанную позицию.
        /// </summary>
        /// <param name="index">Куда втавить кадр</param>
        /// <param name="frame">Сам кадр</param>
        public void InsertFrame(int index, Frame frame) {
            frames.Insert(index, frame);
            // TODO: curent
        }


        /// <summary>
        /// Вставка на указанную позицию пустого кадра
        /// </summary>
        /// <param name="index">Куда вставить пустой кадр</param>
        public void InsertEmptyFrame(int index) {
            frames.Insert(index, new Frame());
            // TODO: curent
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


        #endregion

    }
}