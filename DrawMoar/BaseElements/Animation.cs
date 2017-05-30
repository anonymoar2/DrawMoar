using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMoar.BaseElements
{
    public class Animation
    {
        public List<ILayer> layer = new List<ILayer>();
        public List<Transformation> transformations;

        public Animation(ILayer layer, List<Transformation> transformations) {
            this.layer.Add(layer);
            this.transformations = transformations;
        }

        public Animation(List<ILayer> layer, List<Transformation> transformations) {
            this.layer = layer;
            this.transformations = transformations;
        }

        public ILayer GetByTime(int time) {
            ILayer copyLayer = (ILayer)layer[time % layer.Count].Clone();
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
            foreach(var l in layer) {
                lines.AddRange(l.SaveToFile(pathToDrm));
            }
            foreach (var transformation in transformations) {
                lines.AddRange(transformation.SaveToFile(pathToDrm));
            }
            return lines;
        }
    }
}
