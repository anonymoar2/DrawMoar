using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Figures
{
    public class Line : IFigure
    {
        public static int minSize = 0;
        public static int maxSize = 256;// хз какой максимальный, измените эту циферку

        private int width = minSize; // толщина линии
        public int Width {
            get {
                return width;
            }

            set {
                if(value >= minSize && value <= maxSize) {
                    width = value;
                }
                else {
                    throw new ArgumentException($"Width must be >= {minSize} and <= {maxSize}.");
                }
            }
        }

        private Color mainColor;
        public Color MainColor {
            get {
                return mainColor;
            }

            set {
                // мб проверочки, но хз, они должны быть в самом Color наверное
                mainColor = value;
            }
        }
    }
}
