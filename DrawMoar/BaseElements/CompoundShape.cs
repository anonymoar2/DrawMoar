using System;
using System.Collections.Generic;

using System.Drawing;


namespace DrawMoar.BaseElements
{
    public class CompoundShape : IShape
    {
        public string Alias { get; set; }

        public void Transform(ITransformation trans) {
            throw new NotImplementedException();
        }
        
        List<IShape> shapes = new List<IShape>();


        /// <summary>
        /// Отрисовка на холсте, с параметрами ещё неизвестно
        /// </summary>
        public void Draw() {

        }


        /// <summary>
        /// Отрисовка на bitmap-e
        /// </summary>
        public void Print(Bitmap bitmap) {

        }
        
    }
}