using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Instruments
{
    class Pipette
    {
        Color color;

        public string GetPixel(Bitmap bitmap, int x, int y) {
            return new BaseElements.Color(bitmap.GetPixel(x, y)).ToString();
        }
    }
}
