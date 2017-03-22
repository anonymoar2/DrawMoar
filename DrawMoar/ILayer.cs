using System.Collections.Generic;

using System.Drawing;

using DrawMoar.BaseElements;


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


        /// <summary>
        /// Отображает содержимое слоя в консоли/на канвасе
        /// </summary>
        void Draw(Graphics g);


        Point Position { get; set; }


        ILayer Transform(List<ITransformation> transforations);


        /// <summary>
        /// Скорость слоя определеяется количеством кадров(копий этого слоя со смещением содержимого) 
        /// на всю длительность сцены. (конкретно тут хранится количество кадров (копий слоя))
        /// </summary>
        int Speed { get; set; }
    }
}
