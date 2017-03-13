using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements
{
    public class Color
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

        public Color (string s) {
            var color = Parse(s);
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public BaseElements.Color Parse(string s) {
            // тут пытаемся распарсить строку которая выглядит "#123#456#789"
            // а мб и "#0#1#345", для этого и #
            // если не получается кидаем исключения
            // в конструкторе это делать не хорошо
            BaseElements.Color color = new Color();
            // R = #123
            //...
            return color;
        }

        public override string ToString() {
            return $"#{R}#{G}#{B}";
        }
    }
}
