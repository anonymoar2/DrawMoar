using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMoar
{
    class LightVector : VectorLayerView
    {
        /// <summary>
        /// Врзможно методы которые нужны будут для работы со списком
        /// </summary>
        private List<Size> points = new List<Size>();


        public LightVector(List<Size> points) {
            this.points = points;
        }
        

        /// <summary>
        /// А тут отрисовочка
        /// </summary>
        new public void Draw() {
            throw new NotImplementedException();
        }
    }
}
