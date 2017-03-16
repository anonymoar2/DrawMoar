using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BaseElements
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
        void Draw();


        /// <summary>
        /// Получает image из картинки
        /// </summary>
        /// <returns>image</returns>
        Bitmap GetBitmap();
    }
}
