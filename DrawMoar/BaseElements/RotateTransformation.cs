using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace DrawMoar.BaseElements
{
    class RotateTransformation : Transformation
    {
        private Matrix<double> Transform;
        private System.Windows.Point point;
        private double angle;

            
        public RotateTransformation(System.Windows.Point point, double angle) {
            this.angle = angle;
            this.point = point;
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
            picture.Position = this.Apply(picture.Position);
            picture.Angle =(picture.Angle+ (float)this.angle)%360;
            return picture;
        }


        public Matrix<double> GetTransform() {
            return Transform;
        }


        public override void Decompose(out System.Windows.Point translation, out System.Windows.Point scale, out double rotation) {
            translation = new System.Windows.Point(Transform[0, 2], Transform[1, 2]);
            rotation = (Math.Atan2(Transform[1, 0], Transform[1, 1]) / (Math.PI / 180));
            var xScale = Transform[0, 0] / Math.Cos(rotation * (Math.PI / 180));
            var yScale = Transform[1, 1] / Math.Cos(rotation * (Math.PI / 180));
            scale = new System.Windows.Point(xScale, yScale);
        }

        internal override List<string> SaveToFile(string pathToDrm) {
            return new List<string>() { $"Transformation*rotate*{point.X}*{point.Y}*{angle}" };
        }
    }
}
