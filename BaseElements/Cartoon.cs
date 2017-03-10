using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        private int width;
        private int height;
        private string workingDirectory;



        // текущий, выбранный кадр, на котором мы что-то делаем сейчас
        public Frame currentFrame;

        /// <summary>
        /// List of frames. Every cartoon should contain at least one frame.
        /// Don't pass it out of class instance and work with it carefully.
        /// </summary>
        private List<Frame> frames = new List<Frame>();
        // Сделать приватным возможно, но так удобненько пока


        public Frame GetFrame(int index) {
            if (index >= 0 && index < frames.Count) {
                var frame = frames[index];
                return frame;
            }
            else throw new ArgumentException($"Переданный параметр index не может быть < 0 или > {frames.Count}");
        }


        public List<Frame> GetAllFrames() {
            return frames;
        }

        // Добавлене кадра в конец
        public void AddFrame() {
            frames.Add(new Frame(workingDirectory));
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


        /// <summary>
        /// Cartoon's name.
        /// </summary>
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



        /// <summary>
        /// Width of cartoon's canvas.
        /// </summary>
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


        /// <summary>
        /// Height of cartoon's canvas.
        /// </summary>
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


        /// <summary>
        /// It is the directory in which the program saves the files 
        /// associated with the current project.
        /// </summary>
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
            frames.Add(new Frame(workingDirectory));
            currentFrame = frames.First();
        }


        //// Сохраняет в картинку кадр с переданным индексом
        //public string SaveFrameToPNG(int index) {

        //    return $"img{index}.png";
        //}


        public void SaveCartoonToMP4() {
            // Вызов метода из экспорта
        }




        // Возможно не передевать Frame и менять на currentFrame
        public void SetDuration(float duration, Frame frame) {
            if (duration > 0) {
                frame.Duration = duration;
            }
            else {
                throw new ArgumentException("Длительность не может быть <= 0"); // Возможно тут не нужно его кидать ибо оно кидается в Duration
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

        // Поднятие слоя вверх
        public void UpFrame(int index) {
            if (index >= 0 && index < frames.Count - 1) {
                frames.Insert(index + 2, frames[index]);
                frames.RemoveAt(index);
            }
        }


        // Опускание слоя вниз
        public void DownFrame(int index) {
            if (index > 0 && index < frames.Count) {
                frames.Insert(index - 1, frames[index]);
                frames.RemoveAt(index + 1);
            }
        }
    }
}
