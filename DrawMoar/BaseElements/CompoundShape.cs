using System;
using System.Collections.Generic;

using System.Drawing;
using DrawMoar.Shapes;
using System.Windows.Controls;

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
        public void Draw(Canvas canvas) {

        }


        /// <summary>
        /// Отрисовка на bitmap-e
        /// </summary>
        public void Print() {

        }
        
    }
}