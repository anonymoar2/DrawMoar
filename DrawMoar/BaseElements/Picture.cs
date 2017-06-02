using DrawMoar.Drawing;
using System;

using System.Drawing;
using System.IO;

namespace DrawMoar.BaseElements
{
    public class Picture : ICloneable
    {
        public System.Windows.Point Position { get; set; }
        public float Angle { get; set; }

        public Image Image { get; set; }


        public Picture() {
            Position = new System.Windows.Point(0, 0);
            Image = null;
        }

        

        public object Clone()
        {
            var buf = new Picture();
            buf.Position = Position;
            if(Image!=null)buf.Image = (Image)Image.Clone();
            return buf;
        }

        internal string SaveToFile(string pathToDrm, string name) {
            Image.Save(Path.Combine(pathToDrm, "images", $"{name}.png"));
            return Path.Combine(pathToDrm, "images", $"{name}.png");
        }
    }
}
