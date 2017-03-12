using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMoar.Instruments
{
    class Pipette
    {
        System.Drawing.Color color;

        public Color GetPixel(Bitmap bitmap, int x, int y) {
            return bitmap.GetPixel(x, y);
        }
    }
}
