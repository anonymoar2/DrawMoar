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
    class LightVectorLayer : VectorLayer
    {
        private List<Size> points;

        public LightVectorLayer() {

        }

        public LightVectorLayer(List<Size> points) {
            this.points = points;
        }

        /// <summary>
        /// А тут отрисовочка
        /// </summary>
        new public void Draw() {
            /// Проходим по списку точек и рисуем их линиями
        }
    }
}
