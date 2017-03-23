using System;

using System.Drawing;
using System.Drawing.Drawing2D;


namespace DrawMoar.BaseElements
{
    /// <summary>
    /// TODO класс
    /// </summary>
    class RotateTransformation : Transformation
    {
        private Matrix<double> Transform;
        private double angle;

            
        public RotateTransformation(System.Windows.Point point, double angle) {
            this.angle = angle;
            var one = new Matrix<double>(new double[3, 3] { { 1, 0, point.X }, { 0, 1, point.Y }, { 0, 0, 1 } });
            var three = new Matrix<double>(new double[3, 3] { { 1, 0, -point.X }, { 0, 1, -point.Y }, { 0, 0, 1 } });
            var two = new Matrix<double>(new double[3, 3] {{ Math.Cos(angle * (Math.PI / 180)), -Math.Sin(angle * (Math.PI / 180)), 0 }, { Math.Sin(angle * (Math.PI / 180)),  Math.Cos(angle * (Math.PI / 180)), 0 }, {0,0,1 }});
            Transform = one * two * three;
        }


        public override System.Windows.Point Apply(System.Windows.Point point) {
            var rP = Transform * new Matrix<double>(new double[3, 1] { { point.X }, { point.Y }, { 1 } });
            return new System.Windows.Point(rP[0, 0], rP[1, 0]);
        }


        /// <summary>
        ///TODO: Решить проблему с double и float, ибо везде конверчу туда-сюда 
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public override Picture Apply(Picture picture) {
            // Не уверена в работе TODO: Потестить трансформации
            Bitmap bmp = new Bitmap(picture.Image.Width, picture.Image.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(Convert.ToSingle(angle));
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(picture.Image, new System.Drawing.Point(0, 0));
            gfx.Dispose();
            return picture;
        }


        // for Scale
        public Matrix<double> GetTransform() {
            return Transform;
        }
    }
}
