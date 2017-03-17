using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseElements;
using System.Text.RegularExpressions;
using System.Drawing;

namespace DrawMoar
{
    class RasterLayerView : RasterLayer
    {
        /// <summary>
        /// А тут отрисовочка
        /// </summary>
        new public void Draw() {
            throw new NotImplementedException();
        }

        public RasterLayerView(Bitmap bitmap) : base(bitmap) { }

    }
}
