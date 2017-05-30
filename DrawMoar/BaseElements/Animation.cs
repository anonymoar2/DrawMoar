using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMoar.BaseElements
{
    public class Animation
    {
        public List<ILayer> layers = new List<ILayer>();
        public List<Transformation> transformations;
        public string Name { get; set; }

        public Animation(string name,ILayer layer, List<Transformation> transformations) {
            this.layers.Add(layer);
            this.transformations = transformations;
            Name = name;
        }

        public Animation(ILayer layer, List<Transformation> transformations) {
            this.layers.Add(layer);
            this.transformations = transformations;
            Name = $"Animation_{Editor.cartoon.CurrentFrame.animations.Count}";
        }

        public Animation(List<ILayer> layer, List<Transformation> transformations) {
            this.layers = layer;
            this.transformations = transformations;
        }

        public ILayer GetByTime(int time) {
            ILayer copyLayer = (ILayer)layers[time % layers.Count].Clone();
            foreach(var transform in transformations) {
                for (int i = 0; i < time; i++) {
                    copyLayer.Transform(transform);
                }
            }
            return copyLayer;
        }

        internal List<string> SaveToFile(string pathToDrm) {
            List<string> lines = new List<string>();
            lines.Add($"Animation*");
            foreach(var l in layers) {
                lines.AddRange(l.SaveToFile(pathToDrm));
            }
            foreach (var transformation in transformations) {
                lines.AddRange(transformation.SaveToFile(pathToDrm));
            }
            return lines;
        }
    }
}
