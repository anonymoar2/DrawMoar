using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DrawMoar.BaseElements
{
    public class Scene : ICloneable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Frame> frames = new List<Frame>();


        public Scene()
        {
            name = $"Scene{Editor.cartoon.scenes.Count}";
            Frame frame = new Frame("Frame0");
            frame.duration = 1;
            frames.Add(frame);
        }


        public Scene(string name)
        {
            this.name = name;
            Frame frame = new Frame("Frame0");
            frame.duration = 1;
            frames.Add(frame);
        }


        public void Generate(Frame currentFrame, int seconds)
        {
            /// На каждую секунду генерируем по 25 кадров. Каждый кадр будет по длительности 0.04 секунды.
            for (int i = 1; i < seconds * 25; i ++)
            {
                frames.Insert(frames.IndexOf(currentFrame) + i, currentFrame.GetByTime(i));
            }
            frames.RemoveAt(0);
        }


        public void Cycle(int count)
        {
            var newFrames = new List<Frame>();           
            foreach (var frame in frames)
            {
                newFrames.Add((Frame)frame.Clone());
            }
            for (int i = 0; i < count; i++)
            {
                foreach (var frame in newFrames)
                {
                    frames.Add((Frame)frame.Clone());
                }
            }
        }

        public object Clone()
        {
            var bufFrames = new List<Frame>();

            foreach (var frame in frames)
            {
                bufFrames.Add((Frame)frame.Clone());
            }

            var bufScene = new Scene(Name);
            bufScene.frames = bufFrames;

            return bufScene;
        }

        internal List<string> SaveToFile(string pathToDrm) {
            List<string> lines = new List<string>();
            lines.Add($"Scene*{Name}");
            foreach (var frame in frames) {
                lines.AddRange(frame.SaveToFile(pathToDrm));
            }
            return lines;
        }
        
    }
}