using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseElements
{
    public class Scene
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// Название (имя) сцены
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
                    throw new ArgumentException("Scene name must contain only letters and numbers.");
                }
            }
        }


        /// <summary>
        /// Контейнер слоёв, не путаем со списком слоёв в каждом кадре
        /// </summary>
        private List<ILayer> savedLayers = new List<ILayer>();


        /// <summary>
        /// Список кадров сцены
        /// </summary>
        private List<Frame> frames = new List<Frame>();


        /// <summary>
        /// Текущий кадр
        /// </summary>
        public Frame currentFrame;

        /// <summary>
        /// Создает сцену с указанным именем
        /// </summary>
        /// <param name="sceneName">Имя сцены</param>
        public Scene(string sceneName, int width, int height) {
            name = sceneName;
            Width = width;
            Height = height;
        }


        /// <summary>
        /// Создает сцену со стандартным именем "newScene"
        /// </summary>
        public Scene(int width, int height) {
            name = "newScene";
            Width = width;
            Height = height;
        }


        #region Методы для работы со списокм кадров

        /// <summary>
        /// Получение кадраC:\Users\Никита\Source\Repos\DrawMoar\BaseElements\Scene.cs
        /// </summary>
        /// <param name="index">Индекс кадра который хотим получить</param>
        /// <returns>Кадр с перееданным индексом</returns>
        public Frame GetFrame(int index) {
            if (index >= 0 && index < frames.Count) {
                var frame = frames[index];
                return frame;
            }
            else throw new ArgumentException($"Переданный параметр index не может быть < 0 или > {frames.Count}");
        }


        /// <summary>
        /// Получить все кадры сцены
        /// </summary>
        /// <returns>Список всех кадров сцены</returns>
        public List<Frame> GetAllFrames() {
            return frames;
        }


        /// <summary>
        /// Добавить новый пустой кадр в конец списка кадров
        /// </summary>
        public void AddFrame() {
            frames.Add(new Frame(Width, Height));
            currentFrame = frames.Last();
        }


        /// <summary>
        /// Добавить кадр в конец списка кадров
        /// </summary>
        /// <param name="frame">Кадр, который нужно добавить в конец списка кадров</param>
        public void AddFrame(Frame frame) {
            frames.Add(frame);
            currentFrame = frames.Last();
        }


        /// <summary>
        /// Удаление кадра с переданным индексом
        /// </summary>
        /// <param name="index">Индекс кадра который нужно удалить</param>
        public void RemoveFrameAt(int index) {
            if (index >= 0 && index < frames.Count) {
                frames.RemoveAt(index);
                if(index == frames.Count - 1) {
                    currentFrame = frames.Last();
                }
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {frames.Count}");
            }
        }


        /// <summary>
        /// Получение индекса переданного кадра
        /// </summary>
        /// <param name="frame">Кадр индекс которого нужно получить</param>
        /// <returns>Индекс переданного кадра</returns>
        public int IndexOf(Frame frame) {
            return frames.IndexOf(frame);
        }


        /// <summary>
        /// Удаление кадра
        /// </summary>
        /// <param name="frame">Кадр который необходимо удалить</param>
        public void RemoveFrame(Frame frame) {
            frames.Remove(frame);
        }

        
        /// <summary>
        /// Вставка кадра по индексу
        /// </summary>
        /// <param name="index">Индекс по которому нужно вставить кадр</param>
        /// <param name="frame">Кадр который нужно вставить</param>
        public void InsertFrame(int index, Frame frame) {
            if (index >= 0 && index <= frames.Count) {
                frames.Insert(index, frame);
                if(index == frames.Count) {
                    currentFrame = frames.Last();
                }
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {frames.Count}");
            }
        }
        

        /// <summary>
        /// Добавление пустого кадра по переданному индексу
        /// </summary>
        /// <param name="index">Индекс по которому нужно вставить кадр</param>
        public void InsertEmptyFrame(int index) {
            if (index >= 0 && index <= frames.Count) {
                frames.Insert(index, new Frame(Width,Height));
                if (index == frames.Count) {
                    currentFrame = frames.Last();
                }
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {frames.Count}");
            }
        }


        /// <summary>
        /// Изменение порядка кадров с переданными индексами
        /// </summary>
        /// <param name="firstFrameIndex">Индекс первого кадра</param>
        /// <param name="secondFrameIndex">Индекс второго кадра</param>
        public void ChangeOrderFrames(int firstFrameIndex, int secondFrameIndex) {
            frames.Insert(secondFrameIndex + 1, frames[firstFrameIndex]);
            var tmp = frames[secondFrameIndex];
            frames.RemoveAt(secondFrameIndex);
            frames.RemoveAt(firstFrameIndex);
            frames.Insert(firstFrameIndex, tmp);
        }


        /// <summary>
        /// Поднятие кадра по списку кадров на позицию вверх
        /// </summary>
        /// <param name="index">Индекс кадра который нужно поднять</param>
        public void UpFrame(int index) {
            if (index >= 0 && index < frames.Count - 1) {
                frames.Insert(index + 2, frames[index]);
                frames.RemoveAt(index);
            }
        }

        /// <summary>
        /// Перемещение кадра из списка кадров на позицию вниз
        /// </summary>
        /// <param name="index">Индекс кадра который нужно переместить вниз</param>
        public void DownFrame(int index) {
            if (index > 0 && index < frames.Count) {
                frames.Insert(index - 1, frames[index]);
                frames.RemoveAt(index + 1);
            }
        }
        #endregion

        #region Методы для работы с контейнером слоёв


        /// <summary>
        /// Получение слоя
        /// </summary>
        /// <param name="index">Индекс слоя который хотим получить</param>
        /// <returns>Слой с перееданным индексом</returns>
        public ILayer GetSavedLayer(int index) {
            if (index >= 0 && index < savedLayers.Count) {
                var layer = savedLayers[index];
                return layer;
            }
            else throw new ArgumentException($"Переданный параметр index не может быть < 0 или > {savedLayers.Count}");
        }


        /// <summary>
        /// Получить все слои из контейнера слоёв
        /// </summary>
        /// <returns>Список всех слоёв из контейнера сцены</returns>
        public List<ILayer> GetAllSavedLayer() {
            return savedLayers;
        }


        /// <summary>
        /// Добавить текущий слой текущего кадра в конец контейнера слоёв
        /// </summary>
        public void SaveSavedLayer() {
            savedLayers.Add(currentFrame.CurrentLayer);
        }


        /// <summary>
        /// Удаление слоя с переданным индексом
        /// </summary>
        /// <param name="index">Индекс слоя который нужно удалить</param>
        public void RemoveSavedLayerAt(int index) {
            if (index >= 0 && index < savedLayers.Count) {
                savedLayers.RemoveAt(index);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {savedLayers.Count}");
            }
        }


        /// <summary>
        /// Получение индекса переданного слоя
        /// </summary>
        /// <param name="layer">Слой индекс которого нужно получить</param>
        /// <returns>Индекс переданного кадра</returns>
        public int IndexOf(ILayer layer) {
            return savedLayers.IndexOf(layer);
        }


        /// <summary>
        /// Удаление слоя
        /// </summary>
        /// <param name="layer">Слой который необходимо удалить</param>
        public void RemoveSavedLayer(ILayer layer) {
            savedLayers.Remove(layer);
        }


        /// <summary>
        /// Вставка слоя по индексу
        /// </summary>
        /// <param name="index">Индекс по которому нужно вставить слой</param>
        /// <param name="layer">слой который нужно вставить</param>
        public void InsertFrame(int index, ILayer layer) {
            if (index >= 0 && index <= savedLayers.Count) {
                savedLayers.Insert(index, layer);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {savedLayers.Count}");
            }
        }


        /// <summary>
        /// Изменение порядка слоёв с переданными индексами
        /// </summary>
        /// <param name="firstSavedLayerIndex">Индекс первого слоя</param>
        /// <param name="secondSavedLayerIndex">Индекс второго слоя</param>
        public void ChangeOrderSavedLayers(int firstSavedLayerIndex, int secondSavedLayerIndex) {
            frames.Insert(secondSavedLayerIndex + 1, frames[firstSavedLayerIndex]);
            var tmp = frames[secondSavedLayerIndex];
            frames.RemoveAt(secondSavedLayerIndex);
            frames.RemoveAt(firstSavedLayerIndex);
            frames.Insert(firstSavedLayerIndex, tmp);
        }


        /// <summary>
        /// Поднятие слоя по списку слоёв на позицию вверх
        /// </summary>
        /// <param name="index">Индекс слоя который нужно поднять</param>
        public void UpSavedLayer(int index) {
            if (index >= 0 && index < savedLayers.Count - 1) {
                savedLayers.Insert(index + 2, savedLayers[index]);
                savedLayers.RemoveAt(index);
            }
        }


        /// <summary>
        /// Перемещение слоя из списка слоёв на позицию вниз
        /// </summary>
        /// <param name="index">Индекс слоя который нужно переместить вниз</param>
        public void DownSavedLayer(int index) {
            if (index > 0 && index < savedLayers.Count) {
                savedLayers.Insert(index - 1, savedLayers[index]);
                savedLayers.RemoveAt(index + 1);
            }
        }
        #endregion

    }
}