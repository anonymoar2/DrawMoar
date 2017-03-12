using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseElements;

namespace BaseElements.Figures
{
    /// <summary>
    /// Каждая фигура это слой новый векорный поэтому наследуется от этого класса
    /// </summary>
    public class Line : VectorLayer
    {
        public const int minWidth = 1;
        public const int maxWidth = 256;// хз какой максимальный, измените эту циферку

        private int width = minWidth; // толщина линии

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

        private Color mainColor = Color.Black;
        public Color MainColor { get; set; }
    }
}

