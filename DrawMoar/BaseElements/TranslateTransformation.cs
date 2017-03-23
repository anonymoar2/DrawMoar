using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawMoar.BaseElements
{
    class TranslateTransformation : Transformation
    {
        private Matrix<double> Transform;


        public TranslateTransformation(System.Windows.Point point) {
            Transform = new Matrix<double>(new double[3, 3] { { 1, 0, point.X }, { 0, 1, point.Y }, { 0, 0, 1 } });
        }


        public override System.Windows.Point Apply(System.Windows.Point point) {
            var rP = Transform * new Matrix<double>(new double[3, 1] { { point.X }, { point.Y }, { 1 } });
            return new System.Windows.Point(rP[0, 0], rP[1, 0]);
        }


        /// <summary>
        /// TODO: Доделать метод
        /// По сути просто позицию переносим куда надо у картинки
        /// </summary>
        /// <param name="picture">картинка с растрового слоя</param>
        /// <returns></returns>
        public override Picture Apply(Picture picture) {
            // Вовращает копию трансформированной пикчи, ну или не совсем копию, пока вроде насрать
            picture.Position = Apply(picture.Position);
            return picture;
        }
    }
}
