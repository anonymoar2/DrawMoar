using System.Windows.Media;


namespace DrawMoar.BaseElements {
    public struct Color {
        byte A;
        byte R;
        byte G;
        byte B;


        /*public Color() {
            A = 0;
            R = 0;
            G = 0;
            B = 0;
        }*/


        public Color(System.Windows.Media.Color color) {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }


        public Color(System.Drawing.Color color) {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }


        public Color(System.Windows.Media.Brush brush) {
            A = ((System.Windows.Media.Color)brush.GetValue(SolidColorBrush.ColorProperty)).A;
            R = ((System.Windows.Media.Color)brush.GetValue(SolidColorBrush.ColorProperty)).R;
            G = ((System.Windows.Media.Color)brush.GetValue(SolidColorBrush.ColorProperty)).G;
            B = ((System.Windows.Media.Color)brush.GetValue(SolidColorBrush.ColorProperty)).B;
        }


        public System.Windows.Media.Color ToMediaColor() {
            return System.Windows.Media.Color.FromArgb(A, R, G, B);
        }


        public System.Drawing.Color ToDrawingColor() {
            return System.Drawing.Color.FromArgb(A, R, G, B);
        }


        public Brush ToBrush() {
            return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)R, (byte)G, (byte)B));
        }
    }
}
