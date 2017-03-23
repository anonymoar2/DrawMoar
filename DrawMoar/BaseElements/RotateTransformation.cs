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
            var one = new Matrix<double>(new double[3, 3] { { 1, 0, point.X }, { 0, 1, point.Y }, { 0, 0, 1 } });
            var three = new Matrix<double>(new double[3, 3] { { 1, 0, -point.X }, { 0, 1, -point.Y }, { 0, 0, 1 } });
            var two = new Matrix<double>(new double[3, 3] {{ Math.Cos(angle * (Math.PI / 180)), -Math.Sin(angle * (Math.PI / 180)), 0 }, { Math.Sin(angle * (Math.PI / 180)),  Math.Cos(angle * (Math.PI / 180)), 0 }, {0,0,1 }});
            Transform = one * two * three;
        }

        public override System.Windows.Point Apply(System.Windows.Point point) {
            var rP = Transform * new Matrix<double>(new double[3, 1] { { point.X }, { point.Y }, { 1 } });
            return new System.Windows.Point(rP[0, 0], rP[1, 0]);
        }

        public override Picture Apply(Picture picture) {
            throw new NotImplementedException();
        }
    }
}
