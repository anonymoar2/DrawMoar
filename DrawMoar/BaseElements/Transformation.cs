using System;

using System.Drawing;


namespace DrawMoar.BaseElements
{
    /// <summary>
    /// Перепишу нормально
    /// </summary>
    public class Transformation
    {
        private static Func<float, double> DegToRad = angle => angle * (Math.PI / 180);
        private static Func<double, float> RadToDeg = angle => (float)(angle / (Math.PI / 180));
        private static Func<float, float> Cos = angle => (float)Math.Cos(DegToRad(angle));
        private static Func<float, float> Sin = angle => (float)Math.Sin(DegToRad(angle));

        private Matrix<float> Transform;

        private Transformation(Matrix<float> transform) {
            Transform = transform;
        }

        /// <summary>
        /// Возвращает преобразование поворота на угол angle вокруг точки point
        /// </summary>
        /// <remarks>
        /// Если оси Х и У смотрят вправо и вверх соответственно => поворот против часовой стрелки,
        /// Если оси Х и У смотрят вправо и вниз соответственно => поворот по часовой стрелки.
        /// </remarks>
        public static Transformation Rotate(PointF point, float angle) {
            var trans = Translate(point);
            var transInvert = Translate(new PointF(-point.X, -point.Y));

            var rotation = new Transformation(new Matrix<float>(new float[3, 3] {
                  { Cos(angle), -Sin(angle), 0 }
                , { Sin(angle),  Cos(angle), 0 }
                , {     0,           0,      1 }
            }));

            return trans * rotation * transInvert;
        }

        /// Возвращает преобразование параллельного переноса на вектор ((0,0), point)
        public static Transformation Translate(PointF point) {
            return new Transformation(new Matrix<float>(new float[3, 3] {
                  { 1, 0, point.X }
                , { 0, 1, point.Y }
                , { 0, 0,    1    }
            }));
        }

        /// Возвращает преобразование масштабирования с коэффициентом scaleFactor
        /// относительно точки point.
        /// отрицательные значения параметра scaleFactor соответствуют инверсии
        public static Transformation Scale(PointF point, float scaleFactor) {
            var trans = Translate(point);
            var transInvert = Translate(new PointF(-point.X, -point.Y));

            var scale = new Transformation(new Matrix<float>(new float[3, 3] {
                  { scaleFactor,      0,      0 }
                , {      0,      scaleFactor, 0 }
                , {      0,           0,      1 }
            }));

            return trans * scale * transInvert;
        }

        /// Возвращает преобразование масштабирования с коэффициентом scaleFactor
        /// относительно прямой, проходящей через точки point1, point2
        /// отрицательные значения параметра scaleFactor соответствуют инверсии
        /// бросает InvalidOperationException, если точки point1 и point2 равны
        public static Transformation Scale(PointF point1, PointF point2, float scaleFactor) {
            if (point1 == point2) {
                throw new InvalidOperationException(
                    "ERROR: невозможно задать линию через 2 одинаковые точки");
            }

            var trans = Translate(point1);
            var transInvert = Translate(new PointF(-point1.X, -point1.Y));
            var translatedPoint = transInvert[point2];

            // Угол между осью Х и смещенной второй точкой.
            var angle = RadToDeg(Math.Asin(translatedPoint.Y
                      / Math.Sqrt(translatedPoint.X * translatedPoint.X
                                + translatedPoint.Y * translatedPoint.Y)
                      ));

            // ROTATE - COUNTER-CLOCKWISE!!! so,
            var rotate = Rotate(new PointF(), -angle); // clockwise rotation
            var rotateInvert = Rotate(new PointF(), angle); // counter-clockwise rotation.

            var scale = new Transformation(new Matrix<float>(new float[3, 3] {
                  { scaleFactor, 0, 0 }
                , {      0,      1, 0 }
                , {      0,      0, 1 }
            }));

            return trans * rotate * scale * rotateInvert * transInvert;
        }

        public void Decompose(out PointF translation, out PointF scale, out float rotation) {
            translation = new PointF(Transform[0, 2], Transform[1, 2]);

            rotation = RadToDeg(Math.Atan2(Transform[1, 0], Transform[1, 1]));

            var xScale = Transform[0, 0] / Cos(rotation);
            var yScale = Transform[1, 1] / Cos(rotation);

            scale = new PointF(xScale, yScale);
        }

        /// Возвращает преобразование, получающееся последовательным применением
        /// преобразований m1 и m2
        public static Transformation operator *(Transformation m1, Transformation m2) {
            return new Transformation(m1.Transform * m2.Transform);
        }

        /// Для любой точки плоскости возвращает её образ
        public PointF this[PointF point] {
            get {
                var resPoint = Transform * new Matrix<float>(new float[3, 1] {
                      { point.X }
                    , { point.Y }
                    , {    1    }
                });

                return new PointF(resPoint[0, 0], resPoint[1, 0]);
            }
        }
    }
}
