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
            
        }


        public ScaleTransformation(System.Windows.Point point1, System.Windows.Point point2, double scaleFactor) {

        }


        public override Picture Apply(Picture picture) {
            throw new NotImplementedException();
        }


        public override System.Windows.Point Apply(System.Windows.Point point) {
            throw new NotImplementedException();
        }
    }
}
