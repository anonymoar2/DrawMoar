using System;

using System.Drawing;

using DrawMoar.Shapes;
using DrawMoar.BaseElements;
using DrawMoar.Drawing;


namespace DrawMoar
{
    public class VectorLayer : ILayer
    {
        public bool Visible { get; set; }

        public CompoundShape Picture { get; set; }

        public string Name { get; set; }


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
            Name = name;
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


        public Image Miniature(int width, int height) {
            var rasterLayer = ToRasterLayer();
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            var newImage = rasterLayer.Picture.Image.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
            return newImage;
        }


        public Bitmap GetImage(double width, double height) {
            Bitmap bitmap = new Bitmap((int)width, (int)height);
            var graphics = Graphics.FromImage(bitmap);

            foreach (var shape in Picture.shapes) {
                shape.Draw(new GraphicsDrawer(graphics));
            }
            return bitmap;
        }


        public object Clone() {
            var buf = new VectorLayer() {
                Visible = Visible,
                Picture = (CompoundShape)Picture.Clone(),
                Name = Name,
                Position = Position
            };
            return buf;
        }
    }
}
