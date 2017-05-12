using System;


namespace DrawMoar.BaseElements
{
    class TranslateTransformation : Transformation
    {
        private Matrix<double> Transform;


        public TranslateTransformation(System.Windows.Point point) {
            Transform = new Matrix<double>(new double[3, 3] {
                { 1, 0, point.X },
                { 0, 1, point.Y },
                { 0, 0, 1 }
            });
        }


        public override System.Windows.Point Apply(System.Windows.Point point) {
            var rP = Transform * new Matrix<double>(new double[3, 1] {
                { point.X },
                { point.Y },
                { 1 }
            });
            return new System.Windows.Point(rP[0, 0], rP[1, 0]);
        }


        public override Picture Apply(Picture picture) {
            picture.Position = Apply(picture.Position);
            return picture;
        }


        public override void Decompose(out System.Windows.Point translation,
                                       out System.Windows.Point scale,
                                       out double rotation) {
            translation = new System.Windows.Point(Transform[0, 2], Transform[1, 2]);
            rotation = (Math.Atan2(Transform[1, 0], Transform[1, 1]) / (Math.PI / 180));
            var xScale = Transform[0, 0] / Math.Cos(rotation * (Math.PI / 180));
            var yScale = Transform[1, 1] / Math.Cos(rotation * (Math.PI / 180));
            scale = new System.Windows.Point(xScale, yScale);
        }
    }
}
