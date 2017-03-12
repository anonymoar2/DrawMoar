using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseElements
{
    public class Cartoon
    {
        /// <summary>
        /// Текущая сцена.
        /// </summary>
        public Scene CurrentScene { get; set; }

        /// <summary>
        /// Список сцен мультфильма.
        /// </summary>
        private List<Scene> scenes = new List<Scene>();

        /// <summary>
        /// Минимальная ширина холста мультфильма в пикселях.
        /// Соответствует разрешению 144p.
        /// </summary>
        private const int MinimalWidth = 256;

        /// <summary>
        /// Минимальная высота холста мультфильма в пикселях.
        /// Соответствует разрешению 144p.
        /// </summary>
        private const int MinimalHeight = 144;

        /// <summary>
        /// Максимальная ширина холста мультфильма в пикселях.
        /// Соответствует разрешению 4K.
        /// </summary>
        private const int MaximumWidth = 3840;

        /// <summary>
        /// Максимальная высота холста мультфильма в пикселях.
        /// Соответствует разрешению 4K.
        /// </summary>
        private const int MaximumHeight = 2160; // MAXIMUM HATE 😡/

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

        private int width;
        public int Width {
            get { return width; }
            private set {
                if (value >= MinimalWidth && value <= MaximumWidth) {
                    width = value;
                }
                else {
                    throw new ArgumentException($"Cartoon's width must not be lower " +
                                                $"than {MinimalWidth} or bigger " +
                                                $"than {MaximumWidth} pixels.");
                }
            }
        }

        private int height;
        public int Height {
            get { return height; }
            private set {
                if (value >= MinimalHeight || value <= MaximumHeight) {
                    height = value;
                }
                else {
                    throw new ArgumentException($"Cartoon's height must not be lower " +
                                                $"than {MinimalHeight} or bigger " +
                                                $"than {MaximumHeight} pixels.");
                }
            }
        }

        private string workingDirectory;
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

        public Cartoon(string name, int width, int height, string workingDirectory) {
            Name = name;
            Width = width;
            Height = height;
            WorkingDirectory = workingDirectory;

            scenes.Add(new Scene($"scene{scenes.Count}"));
            CurrentScene = scenes.First();
        }

        #region Методы для работы со сценами.
        /// <summary>
        /// Получение сцены по её позиции.
        /// </summary>
        /// <param name="index">Позиция сцены в мультфильме.</param>
        /// <returns>Сцена находящаяся по указанной позиции.</returns>
        public Scene GetScene(int index) {
            if (index >= 0 && index < scenes.Count) {
                var scene = scenes[index];
                return scene;
            }
            else {
                throw new ArgumentException("Переданный параметр index не может " +
                                            $"быть < 0 или > {scenes.Count}");
            }
        }

        /// <summary>
        /// Получение списка всех сцен добавленных в мультфильм.
        /// </summary>
        /// <returns>Список добавленных в мультфильм сцен.</returns>
        public List<Scene> GetAllScenes() {
            return scenes;
        }

        /// <summary>
        /// Добавление пустой сцены в конец списка.
        /// </summary>
        public void AddScene() {
            scenes.Add(new Scene($"scene{scenes.Count}"));
            CurrentScene = scenes.Last();
        }

        /// <summary>
        /// Вставка сцены на указанную позицию.
        /// </summary>
        /// <param name="index">Позиция вставки сцены.</param>
        /// <param name="scene">Добавляемая к мультфильму сцена.</param>
        public void InsertScene(int index, Scene scene) {
            if (index >= 0 && index <= scenes.Count) {
                scenes.Insert(index, scene);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {scenes.Count}");
            }
        }

        /// <summary>
        /// Получение позиции сцены в мультфильме.
        /// </summary>
        /// <param name="scene">Интересуемая сцена.</param>
        /// <returns>Позиция запрошенной сцены в мультфильме.</returns>
        public int IndexOfScene(Scene scene) {
            // WARNING: каким будет поведение если такой сцены нет?
            return scenes.IndexOf(scene);
        }

        /// <summary>
        /// Удаление сцены из списка сцен.
        /// </summary>
        /// <param name="scene">Удаляемая сцена.</param>
        public void RemoveScene(Scene scene) {
            // WARNING: каким будет поведение если такой сцены нет?
            scenes.Remove(scene);
        }

        /// <summary>
        /// Удаление сцены по позиции.
        /// </summary>
        /// <param name="index">Позиция сцены в мультфильме.</param>
        public void RemoveSceneAt(int index) {
            if (index >= 0 && index <= scenes.Count) {
                scenes.RemoveAt(index);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {scenes.Count}");
            }
        }

        /// <summary>
        /// Изменение порядка сцен.
        /// </summary>
        /// <param name="firstSceneIndex">Позиция первой сцены.</param>
        /// <param name="secondSceneIndex">Позиция второй сцены.</param>
        public void SwapScenesPositions(int firstSceneIndex, int secondSceneIndex) {
            // WARNING: Проверить индексы.
            scenes.Insert(secondSceneIndex + 1, scenes[firstSceneIndex]);
            var tmp = scenes[secondSceneIndex];
            scenes.RemoveAt(secondSceneIndex);
            scenes.RemoveAt(firstSceneIndex);
            scenes.Insert(firstSceneIndex, tmp);
        }

        /// <summary>
        /// Поднятие сцены вверх.
        /// </summary>
        /// <param name="index">Позиция поднимаемой сцены.</param>
        public void PutSceneUp(int index) {
            if (index >= 0 && index < scenes.Count - 1) {
                scenes.Insert(index + 2, scenes[index]);
                scenes.RemoveAt(index);
            }
        }

        /// <summary>
        /// Опускание сцены вниз.
        /// </summary>
        /// <param name="index">Позиция опускаемой сцены.</param>
        public void PutSceneDown(int index) {
            if (index > 0 && index < scenes.Count) {
                scenes.Insert(index - 1, scenes[index]);
                scenes.RemoveAt(index + 1);
            }
        }
        #endregion
    }
}
