using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace DrawMoar.BaseElements
{
    public class Scene
    {
        private string name;
        public string Name {
            get { return name; }
            set {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Frame name must contain only letters and numbers.");
                }
            }
        }


        public Scene() {
            name = "newScene";
            frames.Add(new Frame("Frame_0"));
        }


        public Scene(string name) {
            this.name = name;
            frames.Add(new Frame());
        }
        

        public List<Frame> frames = new List<Frame>();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentFrame">Кадр, из которого будут генерироваться остальные</param>
        /// <param name="seconds">Количество секунд которые необходимо сгенерировать</param>
        /// <param name="smallTransformation">Список из кадров и маленьких трансформаций</param>
        public void Generate(Frame currentFrame, int seconds, List<Tuple<ILayer, List<Transformation>>> smallTransformation) {
            /// На каждую секунду генерируем по 25 кадров, чтобы ровненько было. Каждый кадр будет по длительности 0.04 секунды.
            for (int i = 0; i < seconds * 24; i++) {
                frames.Add(new Frame($"generated_frame_{i}"));
                foreach (var layer in currentFrame.layers) {
                    foreach (var transformedLayer in smallTransformation) {
                        if (layer.Item1.Equals(transformedLayer.Item1)) { /// (!!)
                            frames.Last().layers.Add(new Tuple<ILayer, List<Transformation>>(layer.Item1, transformedLayer.Item2));
                        }
                    }
                }
            }
        }
    }
}