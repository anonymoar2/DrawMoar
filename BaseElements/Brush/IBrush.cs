using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Brush
{
    public interface IBrush
    {
        // Здесь будут индикаторы, ссылочки ниже, почитайте
        // https://msdn.microsoft.com/ru-ru/library/mt653979.aspx
        // https://habrahabr.ru/post/140842/
        // http://erpandcrm.ru/vipprogr.ru/ls17_3_3.html
        // Будем использовать для того чтобы в WPF проекте получать для каждого инструмента
        // список его параметров, потому что например у кисти он один - size, а у какого-то другого
        // инструмента их может быть и 0 и 3 и названия они могут иметь совершенно рандомные
        // Пока нет времени, но сегодня постараюсь сделать
        int size { get; set; }
    }
}
