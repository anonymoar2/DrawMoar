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
        int Size { get; set; }


        // true - фигура "заливается" дополнительным цветом, false - только контур основным цветом
        bool Filling { get; set; }

        // тут ещё два свойства: основной цвет и дополнительный, пока не знаю как именно их хранить
        // возможно какой-то строкой даже
    }
}
