using System;
using System.Collections.Generic;

using System.IO;


namespace DrawMoar.BaseElements
{
    public class Cartoon
    {
        public List<Scene> scenes = new List<Scene>();

        private const int MinimalWidth = 256;
        private const int MinimalHeight = 144;
        private const int MaximumWidth = 3840;
        private const int MaximumHeight = 2160;

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
                    Directory.CreateDirectory(value);
                    workingDirectory = value;
                }
                else {
                    throw new ArgumentException($"Can't open directory \"{value}\".");
                }
            }
        }


        public string pathToAudio;


        public Cartoon(string name, int width, int height, string workingDirectory) {
            Name = name;
            Width = width;
            Height = height;
            WorkingDirectory = workingDirectory;
            scenes.Add(new Scene("Scene_0"));
        }
    }
}