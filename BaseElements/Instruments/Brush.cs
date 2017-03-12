using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Brush
{
    public class Brush : IBrush
    {
        public const int minWidth = 1;
        public const int maxWidth = 256;// хз какой максимальный, измените эту циферку

        private int width = minWidth; // толщина

        public int Width {
            get {
                return width;
            }
            set {
                if (value >= minWidth && value <= maxWidth) {
                    width = value;
                }
                else {
                    throw new ArgumentException($"Width must be >= {minWidth} and <= {maxWidth}.");
                }
            }
        }

        private System.Drawing.Color mainColor;
        public System.Drawing.Color MainColor {
            get; set;
        }
    }

}
