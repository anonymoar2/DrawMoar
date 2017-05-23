using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMoar.BaseElements
{
    public class Animation
    {
        public ILayer layer;
        public List<Transformation> transformations;

        public Animation(ILayer layer, List<Transformation> transformations) {
            this.layer = layer;
            this.transformations = transformations;
        }

        public ILayer GetByTime(int time) {
            ILayer copyLayer = (ILayer)layer.Clone();
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
            lines.AddRange(layer.SaveToFile(pathToDrm));
            foreach (var transformation in transformations) {
                lines.AddRange(transformation.SaveToFile(pathToDrm));
            }
            return lines;
        }
    }
}
