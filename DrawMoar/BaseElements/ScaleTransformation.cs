using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawMoar.BaseElements
{
    /// <summary>
    /// TODO класс
    /// </summary>
    class ScaleTransformation : Transformation
    {
        private Matrix<double> Transform;

        public ScaleTransformation(System.Windows.Point point, double scaleFactor) {
            
        }


        public ScaleTransformation(System.Windows.Point point1, System.Windows.Point point2, double scaleFactor) {

        }


        public override System.Windows.Point Apply(System.Windows.Point point) {
            throw new NotImplementedException();
        }

        public override Image Apply(Image image) {
            throw new NotImplementedException();
        }
    }
}
