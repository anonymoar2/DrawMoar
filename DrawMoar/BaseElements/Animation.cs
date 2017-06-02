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
        public TimeFunction timeFunction;

        public Animation(string name,ILayer layer, List<Transformation> transformations, TimeFunction timeFunction) {
            this.layers.Add(layer);
            this.timeFunction = timeFunction;
            this.transformations = transformations;
            Name = name;
        }

        public Animation(ILayer layer, List<Transformation> transformations) {
            this.layers.Add(layer);
            this.transformations = transformations;
            Name = $"Animation{Editor.cartoon.CurrentFrame.animations.Count}";
        }

        public Animation(List<ILayer> layer, List<Transformation> transformations) {
            this.layers = layer;
            this.transformations = transformations;
        }
        

        public ILayer GetByTime(int time) {
            ILayer copyLayer = (ILayer)layers[time % layers.Count].Clone();
            foreach(var transform in transformations) {
                var valueOfBigTransform = transform.value;
                var newValue = timeFunction.GetTime(time);
                if(timeFunction.sum <= valueOfBigTransform) {
                    copyLayer.Transform(transform.GetTransformation(newValue));
                }
                else {
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
