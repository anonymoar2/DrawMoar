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
    class RotateTransformation : Transformation
    {
        private Matrix<double> Transform;

        public RotateTransformation(System.Windows.Point point, double angle) {

        }

        public override System.Windows.Point Apply(System.Windows.Point point) {
            throw new NotImplementedException();
        }

        public override Image Apply(Image image) {
            throw new NotImplementedException();
        }
    }
}
