using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawMoar.BaseElements
{
    class NewTr : Transformation
    {
        private Matrix<double> Transform;
        public double s;
        public Point point;

        public NewTr(Point point, double s, Point translate) {
            this.s = s;
            value = s;
            this.point = point;
            var one = new Matrix<double>(new double[3, 3] { { 1, 0, translate.X }, { 0, 1, translate.Y }, { 0, 0, 1 } });
            var three = new Matrix<double>(new double[3, 3] { { 1, 0, -point.X }, { 0, 1, -point.Y }, { 0, 0, 1 } });
            var two = new Matrix<double>(new double[3, 3] { { Math.Cos(s * (Math.PI / 180)), -Math.Sin(s * (Math.PI / 180)), 0 }, { Math.Sin(s * (Math.PI / 180)), Math.Cos(s * (Math.PI / 180)), 0 }, { 0, 0, 1 } });
            Transform = one * two * three;
        }

        public override Point Apply(Point point) {
            var rP = Transform * new Matrix<double>(new double[3, 1] { { point.X }, { point.Y }, { 1 } });
            return new System.Windows.Point(rP[0, 0], rP[1, 0]);
        }

        public override Picture Apply(Picture picture) {
            picture.Position = this.Apply(picture.Position);
            picture.Angle = (picture.Angle + (float)value) % 360;
            return picture;
        }

        public override void Decompose(out Point translation, out Point scale, out double rotation) {
            translation = new System.Windows.Point(Transform[0, 2], Transform[1, 2]);
            rotation = (Math.Atan2(Transform[1, 0], Transform[1, 1]) / (Math.PI / 180));
            var xScale = Transform[0, 0] / Math.Cos(rotation * (Math.PI / 180));
            var yScale = Transform[1, 1] / Math.Cos(rotation * (Math.PI / 180));
            scale = new System.Windows.Point(xScale, yScale);
        }

        public override Transformation GetTransformation(double value) {
            throw new NotImplementedException();
        }

        internal override List<string> SaveToFile(string pathToDrm) {
            throw new NotImplementedException();
        }
    }
}
