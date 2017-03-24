using System.Drawing;

using DrawMoar.BaseElements;
using DrawMoar.Shapes;


namespace DrawMoar
{
    public interface ILayer
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


        void Draw(Graphics g);


        void Transform(Transformation transformation);


        void AddShape(IShape shape);

        
        System.Drawing.Image Miniature(int width, int height);
    }
}
