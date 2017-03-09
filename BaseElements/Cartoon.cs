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


        /// <summary>
        /// List of frames. Every cartoon should contain at least one frame.
        /// Don't pass it out of class instance and work with it carefully.
        /// </summary>
        public List<Frame> frames = new List<Frame>();
        // Сделать приватным возможно, но так удобненько пока
        

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
        }

        // метод Save из Frame, сохраняет в картинку кадр с переданным индексом
        public void SaveFrame(int index) {

        }

        // текущий, выбранный кадр, на котором мы что-то делаем сейчас
        public Frame currentFrame;


        //public void InsertFrame(Frame frame) {
        //    // TODO: write checks to improve the code safety.
        //    frames.Add(frame);
        //    // TODO: throw some errors.
        //}

       
        // May be index as argument is bad choice.

        //public Frame ExtractFrame(int index) {
        //    // TODO: write checks to improve the code safety.
        //    var extractedFrame = frames[index];
        //    frames.RemoveAt(index);
        //    // TODO: throw some errors.
        //    return extractedFrame;
        //}
    }
}
