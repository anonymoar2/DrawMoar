using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements
{
    class Color
    {
        int R;
        int G;
        int B;

        public Color() {
            R = 0;
            G = 0;
            B = 0;
        }

        public Color(System.Drawing.Color color) {
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public void Parse(string s) {
            // тут пытаемся распарсить строку которая выглядит "123456789" или "#123456789"
            // если не получается кидаем исключения
            // в конструкторе это делать не хорошо
        }

        public override string ToString() {
            return $"{R}{G}{B}";
        }
    }
}
