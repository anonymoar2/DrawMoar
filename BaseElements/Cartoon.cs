using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseElements
{
    public class Cartoon
    {
        // Lower bound is 144p.
        private const int MINIMAL_WIDTH = 256;
        private const int MINIMAL_HEIGHT = 144; 

        // Upper bound is 4K.
        private const int MAXIMUM_WIDTH = 3840;
        private const int MAXIMUM_HEIGHT = 2160; // MAXIMUM HATE 😡/

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
                if (value >= MINIMAL_WIDTH && value <= MAXIMUM_WIDTH) {
                    width = value;
                }
                else {
                    throw new ArgumentException($"Cartoon's width must not be lower " +
                                                $"than {MINIMAL_WIDTH} or bigger " +
                                                $"than {MAXIMUM_WIDTH} pixels.");
                }
            }
        }

        private int height;
        public int Height {
            get { return height; }
            private set {
                if (value >= MINIMAL_HEIGHT || value <= MAXIMUM_HEIGHT) {
                    height = value;
                }
                else {
                    throw new ArgumentException($"Cartoon's height must not be lower " +
                                                $"than {MINIMAL_HEIGHT} or bigger " +
                                                $"than {MAXIMUM_HEIGHT} pixels.");
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
            Directory.CreateDirectory(Path.Combine(workingDirectory, name));
            WorkingDirectory = Path.Combine(workingDirectory, name);
            scenes.Add(new Scene($"scene{scenes.Count}"));
            currentScene = scenes.First();
        }


        private List<Scene> scenes = new List<Scene>();
        public Scene currentScene; // выбранная сцена (текущая сцена)


        // Методы для работы со списком сцен


        public Scene GetScene(int index) {
            if (index >= 0 && index < scenes.Count) {
                var scene = scenes[index];
                return scene;
            }
            else throw new ArgumentException($"Переданный параметр index не может быть < 0 или > {scenes.Count}");
        }


        public List<Scene> GetAllScenes() {
            return scenes;
        }


        // Добавлене сцены в конец списка сцен
        public void AddScene() {
            scenes.Add(new Scene($"scene{scenes.Count}"));
            currentScene = scenes.Last();
        }


        // Удаление сцены по индексу
        public void RemoveAt(int index) {
            if (index >= 0 && index <= scenes.Count) {
                scenes.RemoveAt(index);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {scenes.Count}");
            }
        }


        public int IndexOf(Scene scene) {
            return scenes.IndexOf(scene);
        }


        public void RemoveFrame(Scene scene) {
            scenes.Remove(scene);
        }


        // Вставка сцены по индексу
        public void InsertFrame(int index, Scene scene) {
            if (index >= 0 && index <= scenes.Count) {
                scenes.Insert(index, scene);
            }
            else {
                throw new ArgumentException($"Переданный индекс должен быть >= 0 и <= {scenes.Count}");
            }
        }


        // Изменение порядка сцен
        public void ChangeOrder(int indexOne, int indexTwo) {
            scenes.Insert(indexTwo + 1, scenes[indexOne]);
            var tmp = scenes[indexTwo];
            scenes.RemoveAt(indexTwo);
            scenes.RemoveAt(indexOne);
            scenes.Insert(indexOne, tmp);
        }


        // Поднятие сцены вверх
        public void UpFrame(int index) {
            if (index >= 0 && index < scenes.Count - 1) {
                scenes.Insert(index + 2, scenes[index]);
                scenes.RemoveAt(index);
            }
        }


        // Опускание сцены вниз
        public void DownFrame(int index) {
            if (index > 0 && index < scenes.Count) {
                scenes.Insert(index - 1, scenes[index]);
                scenes.RemoveAt(index + 1);
            }
        }
    }
}
