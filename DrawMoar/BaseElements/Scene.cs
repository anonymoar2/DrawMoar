using System;
using System.Linq;
using System.Collections.Generic;

using NLog;


namespace DrawMoar.BaseElements
{
    public class Scene
    {
        public List<Frame> frames = new List<Frame>();

        private string name;
        public string Name {
            get { return name; }
            set { name = value; }
        }

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();


        public Scene() : this("newScene") {
        }


        public Scene(string name) {
            this.name = name;
            Frame frame = new Frame("Frame_0");
            frame.duration = 1;
            frames.Add(frame);
            logger.Debug($"Создана сцена {name}");
        }


        public void Generate(Frame currentFrame, int seconds) {
            /// На каждую секунду генерируем по 25 кадров. Каждый кадр будет по длительности 0.04 секунды.
            for (int i = 0; i < seconds * 25; i++) {
                Frame frame = new Frame($"generated_frame_{i}");
                frame.duration = (float)seconds / 25;
                frames.Add(frame);
                foreach (var layer in currentFrame.layers) {
                    ILayer tmpLayer = (ILayer)layer.Item1.Clone();
                    foreach (var trans in layer.Item2) {
                        for (int j = 0; j <= i; j++) {
                            tmpLayer.Transform(trans);
                        }
                    }
                    frames.Last().layers.Add(new Tuple<ILayer,
                                             List<Transformation>,
                                             int>((ILayer)tmpLayer.Clone(),
                                             new List<Transformation>(), 0));
                }
                frames.Last().layers.RemoveAt(0);
            }
        }


        public void Cycle(int count) {
            var newFrames = new List<Frame>();
            foreach (var frame in frames) {
                newFrames.Add((Frame)frame.Clone());
            }
            for (int i = 0; i < count; i++) {
                foreach (var frame in newFrames) {
                    frames.Add((Frame)frame.Clone());
                }
            }
        }
    }
}