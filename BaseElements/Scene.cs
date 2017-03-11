using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseElements
{
    public class Scene
    {
        private string name;
        public string Name {
            get { return name; }
            private set {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Cartoon name must contain only letters and numbers.");
                }
            }
        }

        public Scene(string sceneName) {
            name = sceneName;
        }

        public Layer[] layers = null; // TODO: Подумать над контейнером слоёв, возможно запилить отдельный класс
        // TODO: Метод для добавления в контейнер слоёв нового слоя


        private List<Frame> frames = new List<Frame>();
        public Frame currentFrame;

        // Методы для работы со списком кадров


        public Frame GetFrame(int index) {
            if (index >= 0 && index < frames.Count) {
                var frame = frames[index];
                return frame;
            }
            else throw new ArgumentException($"Переданный параметр index не может быть < 0 или > {frames.Count}");
        }


        public List<Frame> GetAllScenes() {
            return frames;
        }


        // Добавлене кадра в конец списка кадров
        public void AddScene() {
            frames.Add(new Frame());
            currentFrame = frames.Last();
        }


        // Удаление кадра по индексу
        public void RemoveAt(int index) {
            if (index >= 0 && index <= frames.Count) {
                frames.RemoveAt(index);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {frames.Count}");
            }
        }


        public int IndexOf(Frame frame) {
            return frames.IndexOf(frame);
        }


        public void RemoveFrame(Frame frame) {
            frames.Remove(frame);
        }


        // Вставка кадра по индексу
        public void InsertFrame(int index, Frame frame) {
            if (index >= 0 && index <= frames.Count) {
                frames.Insert(index, frame);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {frames.Count}");
            }
        }


        // Изменение порядка кадров
        public void ChangeOrder(int indexOne, int indexTwo) {
            frames.Insert(indexTwo + 1, frames[indexOne]);
            var tmp = frames[indexTwo];
            frames.RemoveAt(indexTwo);
            frames.RemoveAt(indexOne);
            frames.Insert(indexOne, tmp);
        }


        // Поднятие кадра вверх
        public void UpFrame(int index) {
            if (index >= 0 && index < frames.Count - 1) {
                frames.Insert(index + 2, frames[index]);
                frames.RemoveAt(index);
            }
        }


        // Опускание кадра вниз
        public void DownFrame(int index) {
            if (index > 0 && index < frames.Count) {
                frames.Insert(index - 1, frames[index]);
                frames.RemoveAt(index + 1);
            }
        }

    }
}
