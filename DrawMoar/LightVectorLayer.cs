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
        private List<Size> points = new List<Size>();

        public LightVectorLayer() {

        }

        public LightVectorLayer(List<Size> points) {
            this.points = points;
        }

        /// <summary>
        /// А тут отрисовочка для показа содержимого слоя при переключении слоёв
        /// </summary>
        new public void Draw() {
            /// Проходим по списку точек и рисуем их линиями
        }
    }
}
