using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawMoar
{
    class Color
    {
        int R;
        int G;
        int B;

        public Color()
        {
            R = 0;
            G = 0;
            B = 0;
        }

        public Color(System.Windows.Media.Color color)
        {
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public System.Windows.Media.Color Convert(System.Drawing.Color color)
        {              
            System.Windows.Media.Color newColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
            return newColor;
        }
     
        public System.Drawing.Color Convert(System.Windows.Media.Color color)
        {
            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
            return newColor;
        }     
    }
}
