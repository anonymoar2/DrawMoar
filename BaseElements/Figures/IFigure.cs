using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Figures
{
    // May be it must be abstract class
    interface IFigure
    {
        // Это толщина но не знаю как на англ правильно
        // тут тоже минимум и максимум потом определим
        int Width { get; set; }


        // true - фигура "заливается" дополнительным цветом, false - только контур основным цветом
        // bool Filling { get; set; } будет только в тех фигурах где он нужен


        BaseElements.Color MainColor { get; set; } // основной
        //BaseElements.Color furtherColor { get; set; } // дополнительный цвет будет только в самих фигурах где он нужен
    }
}
