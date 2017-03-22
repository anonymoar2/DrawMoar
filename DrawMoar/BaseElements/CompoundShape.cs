using System;
using System.Collections.Generic;

using System.Drawing;
using System.Windows.Controls;
using DrawMoar.Shapes;

namespace DrawMoar.BaseElements
{
    public class CompoundShape : IShape
    {
        public string Alias { get; set; }

        
        
        List<IShape> shapes = new List<IShape>();


        /// <summary>
        /// Отрисовка на холсте, и так же сразу на WPF, по сути передаем сюда и bitmap и canvas
        /// </summary>
        public void Draw(Canvas canvas) {
            //Проходим по фигурам, вызывая у них Draw
            // Можно разбить на два метода, один на канвасе, другой на bitmap (один принимает Graphics, другой Canvas)
        }

        
        public void Print()
        {

        }

        public void Transform(Transformation transformation) {
            foreach (var shape in shapes) {
                shape.Transform(transformation);
            }
        }
    }
}