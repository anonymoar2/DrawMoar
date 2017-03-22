using System;

using System.Windows;


namespace DrawMoar.BaseElements
{
    /// <summary>
    /// Перепишу нормально
    /// </summary>
    public class Transformation
    {
        private static Func<double, double> DegToRad = angle => angle * (Math.PI / 180);
        private static Func<double, double> RadToDeg = angle => (double)(angle / (Math.PI / 180));
        private static Func<double, double> Cos = angle => (double)Math.Cos(DegToRad(angle));
        private static Func<double, double> Sin = angle => (double)Math.Sin(DegToRad(angle));
        private Matrix<double> Transform;
        private Transformation(Matrix<double> transform) {
            Transform = transform;
        }
        public static Transformation Rotate(System.Windows.Point point, double angle) {
            var t = Translate(point);
            var tI = Translate(new System.Windows.Point(-point.X, -point.Y));
            var r = new Transformation(new Matrix<double>(new double[3, 3]{{Cos(angle),-Sin(angle),0},{Sin(angle),Cos(angle),0}, {0,0,1}}));
            return t * r * tI;
        }
        public static Transformation Translate(Point point) {
            return new Transformation(new Matrix<double>(new double[3, 3] {{1,0,point.X},{0,1,point.Y},{0,0,1}}));
        }
        public static Transformation Scale(Point point, double scaleFactor) {
            var trans = Translate(point);
            var tI = Translate(new Point(-point.X, -point.Y));
            var s = new Transformation(new Matrix<double>(new double[3, 3] {{scaleFactor,0,0},{ 0,scaleFactor,0},{0,0,1}}));
            return trans * s * tI;
        }
        public static Transformation Scale(Point point1, Point point2, double scaleFactor) {
            var t = Translate(point1);
            var tI = Translate(new Point(-point1.X, -point1.Y));
            var tP = tI[point2];
            var a = RadToDeg(Math.Asin(tP.Y/Math.Sqrt(tP.X * tP.X+tP.Y*tP.Y)));
            var r = Rotate(new Point(), -a);
            var rI = Rotate(new Point(), a);
            var s = new Transformation(new Matrix<double>(new double[3, 3] {{ scaleFactor, 0, 0 },{0,1,0},{0,0,1}}));
            return t * r * s * rI * tI;
        }
        public void Decompose(out Point translation, out Point scale, out double rotation) {
            translation = new Point(Transform[0, 2], Transform[1, 2]);
            rotation = RadToDeg(Math.Atan2(Transform[1, 0], Transform[1, 1]));
            var xS = Transform[0, 0] / Cos(rotation);
            var yS = Transform[1, 1] / Cos(rotation);
            scale = new Point(xS, yS);
        }
        public static Transformation operator *(Transformation m1, Transformation m2) {
            return new Transformation(m1.Transform * m2.Transform);
        }
        public Point this[Point point] {
            get {var rP = Transform *new Matrix<double>(new double[3, 1] {{point.X},{ point.Y},{1}});
            return new Point(rP[0, 0], rP[1, 0]);
            }
        }
    }
}
