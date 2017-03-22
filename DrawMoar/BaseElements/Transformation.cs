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
            var trans = Translate(point);
            var transInvert = Translate(new System.Windows.Point(-point.X, -point.Y));

            var rotation = new Transformation(new Matrix<double>(new double[3, 3] {
                  { Cos(angle), -Sin(angle), 0 }
                , { Sin(angle),  Cos(angle), 0 }
                , {     0,           0,      1 }
            }));

            return trans * rotation * transInvert;
        }
        public static Transformation Translate(Point point) {
            return new Transformation(new Matrix<double>(new double[3, 3] {
                  { 1, 0, point.X }
                , { 0, 1, point.Y }
                , { 0, 0,    1    }
            }));
        }
        public static Transformation Scale(Point point, double scaleFactor) {
            var trans = Translate(point);
            var transInvert = Translate(new Point(-point.X, -point.Y));

            var scale = new Transformation(new Matrix<double>(new double[3, 3] {
                  { scaleFactor,      0,      0 }
                , {      0,      scaleFactor, 0 }
                , {      0,           0,      1 }
            }));

            return trans * scale * transInvert;
        }
        public static Transformation Scale(Point point1, Point point2, double scaleFactor) {
            
            var trans = Translate(point1);
            var transInvert = Translate(new Point(-point1.X, -point1.Y));
            var translatedPoint = transInvert[point2];
            
            var angle = RadToDeg(Math.Asin(translatedPoint.Y
                      / Math.Sqrt(translatedPoint.X * translatedPoint.X
                                + translatedPoint.Y * translatedPoint.Y)
                      ));
            
            var rotate = Rotate(new Point(), -angle);
            var rotateInvert = Rotate(new Point(), angle);

            var scale = new Transformation(new Matrix<double>(new double[3, 3] {
                  { scaleFactor, 0, 0 }
                , {      0,      1, 0 }
                , {      0,      0, 1 }
            }));

            return trans * rotate * scale * rotateInvert * transInvert;
        }
        public void Decompose(out Point translation, out Point scale, out double rotation) {
            translation = new Point(Transform[0, 2], Transform[1, 2]);

            rotation = RadToDeg(Math.Atan2(Transform[1, 0], Transform[1, 1]));

            var xScale = Transform[0, 0] / Cos(rotation);
            var yScale = Transform[1, 1] / Cos(rotation);

            scale = new Point(xScale, yScale);
        }
        public static Transformation operator *(Transformation m1, Transformation m2) {
            return new Transformation(m1.Transform * m2.Transform);
        }
        public Point this[Point point] {
            get {
                var resPoint = Transform * new Matrix<double>(new double[3, 1] {
                      { point.X }
                    , { point.Y }
                    , {    1    }
                });

                return new Point(resPoint[0, 0], resPoint[1, 0]);
            }
        }
    }
}
