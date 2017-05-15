using System;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using DrawMoar.Drawing;

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

        public float duration { get;set; }

        public List<Animation> animations = new List<Animation>();


        public Frame() {
            name = $"Frame_{Cartoon.CurrentScene.frames.Count}";
            animations.Add(new Animation(new VectorLayer("Vector_Layer_0"), new List<Transformation>()));
            duration = 0.04F;
        }


        public Frame(string name) {
            this.name = name;
            animations.Add(new Animation(new VectorLayer("Vector_Layer_0"), new List<Transformation>()));
            duration = 0.04F;
        }
        
        
        public System.Drawing.Bitmap Join() {
            var bm = new Bitmap(Cartoon.Width, Cartoon.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            g.CompositingMode = CompositingMode.SourceOver;
            g.Clear(System.Drawing.Color.White);
            foreach (var a in animations) {
                a.layer.Draw(new GraphicsDrawer(g));
            }
            g.Dispose();
            return bm;
        }


        public Frame GetByTime(int time) {
            var newFrame = new Frame();
            foreach(var a in animations) {
                newFrame.animations.Add(new Animation(a.GetByTime(time), new List<Transformation>()));
            }
            newFrame.animations.RemoveAt(0);
            return newFrame;
        }

        
        public object Clone()
        {
            var buf = new Frame(Name);
            buf.duration = duration;

            foreach(var a in animations)
            {
                buf.animations.Add(new Animation((ILayer)(a.layer).Clone(), new List<Transformation>()));
            }

            return buf;
        }
    }
}