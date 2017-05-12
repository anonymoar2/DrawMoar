using System;
using System.Collections.Generic;

using NLog;
using System.IO;


namespace DrawMoar.BaseElements
{
    public class Cartoon
    {
        public string Name { get; private set; }
        public List<Scene> scenes = new List<Scene>();
        public string pathToAudio;

        private int width;
        public int Width
        {
            get { return width; }
            private set
            {
                if (value >= MinimalWidth && value <= MaximumWidth)
                {
                    width = value;
                }
                else
                {
                    throw new ArgumentException($"Ширина мультфильма не должна быть меньше " +
                                                $"чем {MinimalWidth} и не больше" +
                                                $"{MaximumWidth} пикселей.");
                }
            }
        }

        private int height;
        public int Height
        {
            get { return height; }
            private set
            {
                if (value >= MinimalHeight || value <= MaximumHeight)
                {
                    height = value;
                }
                else
                {
                    throw new ArgumentException($"Высота мультфильма не должна быть меньше " +
                                                $"чем {MinimalHeight} и не больше " +
                                                $"{MaximumHeight} пикселей.");
                }
            }
        }

        private string workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                return workingDirectory;
            }
            private set
            {
                if (Directory.Exists(value))
                {
                    workingDirectory = value;
                }
                else if (Directory.Exists(Path.GetDirectoryName(value)))
                {
                    Directory.CreateDirectory(value);
                    workingDirectory = value;
                }
                else
                {
                    throw new ArgumentException($"Невозможно открыть директорию \"{value}\".");
                }
            }
        }

        private const int MinimalWidth = 256;
        private const int MinimalHeight = 144;
        private const int MaximumWidth = 3840;
        private const int MaximumHeight = 2160;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();


        public Cartoon(string name, int width, int height, string workingDirectory) {
            Name = name;
            Width = width;
            Height = height;
            WorkingDirectory = workingDirectory;
            scenes.Add(new Scene("Scene_0"));
            logger.Debug($"Мультфильм {Name} создан. " +
                         $"Ширина - {width}px, высота - {height}px, " + 
                         $"рабочая директория - {workingDirectory}");
        }
    }
}