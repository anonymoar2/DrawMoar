using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace DrawMoar.BaseElements
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
        private const int MaximumHeight = 2160; // MAXIMUM HATE 😡


        /// <summary>
        /// Название мультика
        /// </summary>
        public string Name { get; private set; }


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


        /// <summary>
        /// Рабочая директория, туда сохраняется мультик при экспорте, 
        /// там же создаются промежуточные файлы, если нужно
        /// </summary>
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
            //scenes.Add(new Scene($"scene{scenes.Count}")); гениальная строка крч
            scenes.Add(new Scene("Scene_0"));
            CurrentScene = scenes.Last();
        }


        #region Методы для работы со сценами.


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
        public void AddEmptyScene() {
            scenes.Add(new Scene() { Name = $"scene_{scenes.Count}" });
            CurrentScene = scenes.Last();
        }


        /// <summary>
        /// Добавление существующей сцены в конец списка.
        /// </summary>
        /// <param name="scene"></param>
        public void AddScene(Scene scene) {
            scenes.Add(scene);
        }


        /// <summary>
        /// Вставка сцены на указанную позицию.
        /// </summary>
        /// <param name="index">Позиция вставки сцены.</param>
        /// <param name="scene">Добавляемая к мультфильму сцена.</param>
        public void InsertScene(int index, Scene scene) {
            scenes.Insert(index, scene);
        }


        /// <summary>
        /// Вставка на указанную позицию пустой сцены
        /// </summary>
        /// <param name="index"></param>
        public void InsertEmptyScene(int index) {
            scenes.Insert(index, new Scene());
        }


        /// <summary>
        /// Получение позиции сцены в мультфильме.
        /// </summary>
        /// <param name="scene">Интересуемая сцена.</param>
        /// <returns>Позиция запрошенной сцены в мультфильме.</returns>
        public int IndexOfScene(Scene scene) {
            return scenes.IndexOf(scene);
        }


        /// <summary>
        /// Получение сцены по её позиции.
        /// </summary>
        /// <param name="index">Позиция сцены в мультфильме.</param>
        /// <returns>Сцена находящаяся по указанной позиции.</returns>
        public Scene GetScene(int index) {
            return scenes[index];
        }


        /// <summary>
        /// Удаление сцены из списка сцен, если из списка удалили последню сцену то добавляем на её место пустую новую
        /// </summary>
        /// <param name="scene">Удаляемая сцена.</param>
        public void RemoveScene(Scene scene) {
            var index = scenes.IndexOf(scene);
            if (scenes.Remove(scene)) {
                if (scenes.Count == 0) {
                    scenes.Add(new Scene("scene_0"));
                    CurrentScene = scenes.First();
                }
                else if (index == 0) {
                    CurrentScene = scenes.First();
                }
                else {
                    CurrentScene = scenes[index - 1];
                }
            }
        }


        /// <summary>
        /// Удаление сцены по позиции.
        /// </summary>
        /// <param name="index">Позиция сцены в мультфильме.</param>
        public void RemoveSceneAt(int index) {
            scenes.RemoveAt(index);
            if (scenes.Count == 0) {
                scenes.Add(new Scene("scene_0"));
                CurrentScene = scenes.First();
            }
            else if (index == 0) {
                CurrentScene = scenes.First();
            }
            else {
                CurrentScene = scenes[index - 1];
            }
        }


        /// <summary>
        /// Изменение порядка сцен.
        /// </summary>
        /// <param name="firstSceneIndex">Позиция первой сцены.</param>
        /// <param name="secondSceneIndex">Позиция второй сцены.</param>
        public void SwapScenesPositions(int firstSceneIndex, int secondSceneIndex) {
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