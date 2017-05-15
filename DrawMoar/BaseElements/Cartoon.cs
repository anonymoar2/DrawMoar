using System;
using System.Collections.Generic;

using System.IO;


namespace DrawMoar.BaseElements {
    public class Cartoon {
        public List<Scene> scenes = new List<Scene>();

        private const int MinimalWidth = 256;
        private const int MinimalHeight = 144;
        private const int MaximumWidth = 3840;
        private const int MaximumHeight = 2160;

        public static string Name { get; private set; }

        private static int width;
        public static int Width {
            get { return width; }
            set {
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

        private static int height;
        public static int Height {
            get { return height; }
            set {
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

        private static string workingDirectory;
        public static string WorkingDirectory {
            get {
                return workingDirectory;
            }
            set {
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


        public static int TotalTime { get; set; }

        public static Animation CurrentLayer { get; set; }
        public static Frame CurrentFrame { get; set; }
        public static Scene CurrentScene { get; set; }
        private static Cartoon prev;
        public static Cartoon Prev
        {
            get
            {
                return prev;
            }
            set
            {
                prev = (Cartoon)value.Clone();
                GC.Collect();
            }
        }
        public static int PrevCurrentFrameNumber { get; set; }
        public static int PrevCurrentSceneNumber { get; set; }
        public static int PrevCurrentLayerNumber { get; set; }

        public object Clone()
        {
            var bufScenes = new List<Scene>();

            foreach (var scene in scenes)
            {
                bufScenes.Add((Scene)scene.Clone());
            }

            var bufCartoon = new Cartoon(Name, Width, Height, WorkingDirectory);
            bufCartoon.scenes = bufScenes;

            return bufCartoon;
        }
    }
}