using System.Drawing;

using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System.Collections.Generic;
using System;
using System.Windows.Controls;

namespace DrawMoar
{
    public interface ILayer : ICloneable
    {
        /// <summary>
        /// Состояние видимости слоя. 
        /// true - слой видимый, false - невидимый.
        /// При экспорте кадра в картинку картинка получается только из видимых слоёв.
        /// </summary>
        bool Visible { get; set; }


        /// <summary>
        /// Название слоя.
        /// </summary>
        string Name { get; set; }

        void Print(Canvas canvas);


        void Draw(Graphics g);


        void Transform(Transformation transformation);


        void AddShape(IShape shape);


        System.Drawing.Image Miniature(int width, int height);


        System.Windows.Point Position { get; set; }
        List<Text> Text { get; }

        Bitmap GetImage(double width, double height);
    }
}
