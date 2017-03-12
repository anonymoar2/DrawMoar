using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        private List<Layer> savedLayers = new List<Layer>(); 
        
        private List<Frame> frames = new List<Frame>();
        public Frame currentFrame;
        

        #region Методы для работы со списокм кадров

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


        // Добавлене пустого кадра в конец списка кадров
        public void AddFrame() {
            frames.Add(new Frame());
            currentFrame = frames.Last();
        }


        // Удаление кадра по индексу
        public void RemoveFrameAt(int index) {
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
        #endregion

        #region Методы для работы с контейнером слоёв


        public Layer GetSavedLayers(int index) {
            if (index >= 0 && index < savedLayers.Count) {
                var layer = savedLayers[index];
                return layer;
            }
            else throw new ArgumentException($"Переданный параметр index не может быть < 0 или > {savedLayers.Count}");
        }


        public List<Layer> GetSavedLayers() {
            return savedLayers;
        }


        // Добавлене слоя в конец контейнера слоёв
        public void AddSavedLayers(Layer layer) {
            savedLayers.Add(layer);
        }


        // Удаление слоя из контейнера слоёв по индексу
        public void RemoveSavedLayerAt(int index) {
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

    }
}