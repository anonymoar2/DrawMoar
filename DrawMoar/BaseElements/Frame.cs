using System;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using DrawMoar.Drawing;
using NLog;


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

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();


        public Frame() : this($"Frame_{Cartoon.CurrentScene.frames.Count}") {
        }


        public Frame(string name) {
            this.name = name;
            layers.Add(new Tuple<ILayer, List<Transformation>, int>(
                new VectorLayer("Vector_Layer_0"),
                new List<Transformation>(), 0)
            );
            logger.Debug($"Создан кадр {name}");
        }


        public Bitmap Join() {
            var bitmap = new Bitmap(Cartoon.Width, Cartoon.Height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bitmap)) {
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.Clear(System.Drawing.Color.White);
                foreach (var layer in layers) {
                    layer.Item1.Draw(new GraphicsDrawer(graphics));
                }
            }
            return bitmap;
        }


        public object Clone() {
            var buf = new Frame(Name);

            foreach (var layer in layers) {
                buf.layers.Add(new Tuple<ILayer, List<Transformation>, int>(
                    (ILayer)(layer.Item1).Clone(),
                    new List<Transformation>(), 0)
                );
            }

            return buf;
        }
    }
}