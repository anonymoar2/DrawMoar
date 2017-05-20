using System;

using System.Drawing;
using System.Windows.Controls;

using DrawMoar.Shapes;
using DrawMoar.BaseElements;
using DrawMoar.Drawing;
using System.Collections.Generic;

namespace DrawMoar
{
    public class VectorLayer : ILayer
    {
        public bool Visible { get; set; }

        public CompoundShape Picture { get; set; }

        public string Name {get; set; }


        private System.Windows.Point position = new System.Windows.Point(0, 0);
        public System.Windows.Point Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }

        public VectorLayer() {
            Name = "newVectorLayer";
            Visible = true;
            Picture = new CompoundShape();
        }


        public VectorLayer(string name) {
            this.Name = name;
            Visible = true;
            Picture = new CompoundShape();
        }


        public void Draw(IDrawer drawer) {
            Picture.Draw(drawer);          
        }

        public void Transform(Transformation transformation) {
            Picture.Transform(transformation);
        }

        public void AddShape(IShape shape) {
            Picture.shapes.Add(shape);
        }

        public RasterLayer ToRasterLayer() {
            var newLayer = new RasterLayer();
            var g = Graphics.FromImage(newLayer.Picture.Image);
            Picture.Draw(new GraphicsDrawer(g));
            return newLayer;
        }

        public bool ThumbnailCallback() {
            return false;
        }

        public System.Drawing.Image Miniature(int width, int height) {
            var rasterLayer = ToRasterLayer();
            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            var newImage = rasterLayer.Picture.Image.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
            return newImage;
        }

        public System.Drawing.Bitmap GetImage(double width, double height) {
            Bitmap b = new Bitmap((int)width, (int)height);
            var g = Graphics.FromImage(b);
            
            foreach (var sh in Picture.shapes) {
                sh.Draw(new GraphicsDrawer(g));
            }           
            return b;
        }

        public object Clone()
        {
            var buf = new VectorLayer();
            buf.Visible = Visible;
            buf.Picture = (CompoundShape)Picture.Clone();
            buf.Name = Name;
            buf.Position = Position;

            return buf;
        }

        public List<string> SaveToFile(string pathToDrm) {
            List<string> lines = new List<string>();
            lines.Add($"\t\t\tLayer**{Name}*v");
            lines.AddRange(Picture.SaveToFile(pathToDrm));
            return lines;
        }
    }
}
