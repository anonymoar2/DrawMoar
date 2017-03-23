using System;

using System.Drawing;


namespace DrawMoar.BaseElements
{
    /// <summary>
    /// TODO класс
    /// </summary>
    class ScaleTransformation : Transformation
    {
        private Matrix<double> Transform;


        public ScaleTransformation(System.Windows.Point point, double scaleFactor) {
            var one = new Matrix<double>(new double[3, 3] { { 1, 0, point.X }, { 0, 1, point.Y }, { 0, 0, 1 } });
            var three = new Matrix<double>(new double[3, 3] { { 1, 0, -point.X }, { 0, 1, -point.Y }, { 0, 0, 1 } });
            var two = new Matrix<double>(new double[3, 3] { { scaleFactor, 0, 0 }, { 0, scaleFactor, 0 }, { 0, 0, 1 } });
            Transform = one * two * three;
        }


        public ScaleTransformation(System.Windows.Point point1, System.Windows.Point point2, double scaleFactor) {
            var one = new Matrix<double>(new double[3, 3] { { 1, 0, point1.X }, { 0, 1, point1.Y }, { 0, 0, 1 } });
            var five = new Matrix<double>(new double[3, 3] { { 1, 0, -point1.X }, { 0, 1, -point1.Y }, { 0, 0, 1 } });
            var tP = new ScaleTransformation(point2, scaleFactor).Apply(point2);
            var a = (Math.Asin(tP.Y / Math.Sqrt(tP.X * tP.X + tP.Y * tP.Y))) / (Math.PI / 180);
            var two = new RotateTransformation(new System.Windows.Point(), -a).GetTransform();
            var four = new RotateTransformation(new System.Windows.Point(), a).GetTransform();
            var three = new Matrix<double>(new double[3, 3] { { scaleFactor, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } });
            Transform = one * two * three * four * five;
        }


        /// <summary>
        /// TODO: Scale картинки
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public override Picture Apply(Picture picture) {
            throw new NotImplementedException();
        }


        public override System.Windows.Point Apply(System.Windows.Point point) {
            var rP = Transform * new Matrix<double>(new double[3, 1] { { point.X }, { point.Y }, { 1 } });
            return new System.Windows.Point(rP[0, 0], rP[1, 0]);
        }
    }
}
