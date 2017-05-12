using NLog;
using System.Windows.Media;


namespace DrawMoar.BaseElements
{
    public struct Color
    {
        byte A;
        byte R;
        byte G;
        byte B;


        public Color(System.Windows.Media.Color color) {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }


        public Color(System.Drawing.Color color) {
            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
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
            return new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, R, G, B));
        }
    }
}
