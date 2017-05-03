using System;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace DrawMoar.BaseElements
{
    public class Frame : ICloneable
    {
        private string name;
        public string Name {
            get { return name; }
            private set {            
                    name = value;               
            }
        }

        public List<Tuple<ILayer, List<Transformation>, int>> layers = new List<Tuple<ILayer, List<Transformation>, int>>();


        public Frame() {
            name = $"Frame_{GlobalState.CurrentScene.frames.Count}";
            layers.Add(new Tuple<ILayer, List<Transformation>, int>(new VectorLayer("Vector_Layer_0"), new List<Transformation>(), 0));
        }


        public Frame(string name) {
            this.name = name;
            layers.Add(new Tuple<ILayer, List<Transformation>, int>(new VectorLayer("Vector_Layer_0"), new List<Transformation>(), 0));
        }
        
        
        public System.Drawing.Bitmap Join() {
            var bm = new Bitmap(GlobalState.Width, GlobalState.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            g.CompositingMode = CompositingMode.SourceOver;
            g.Clear(System.Drawing.Color.White);
            foreach (var l in layers) {
                l.Item1.Draw(g);
            }
            g.Dispose();
            return bm;
        }


        public object Clone()
        {
            var buf = new Frame(Name);

            foreach(var layer in layers)
            {
                buf.layers.Add(new Tuple<ILayer, List<Transformation>, int>((ILayer)(layer.Item1).Clone(), new List<Transformation>(), 0));
            }

            return buf;
        }
    }
}