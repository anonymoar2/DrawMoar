﻿using System;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using DrawMoar.Drawing;


namespace DrawMoar.BaseElements {
    public class Frame : ICloneable {

        private string name;
        public string Name {
            get { return name; }
            private set {            
                    name = value;               
            }
        }

        public double duration { get;set; }

        public List<Animation> animations = new List<Animation>();


        public Frame() {
            name = $"Frame{Editor.cartoon.CurrentScene.frames.Count}";
            animations.Add(new Animation("Animation0", new VectorLayer("VectorLayer0"), new List<Transformation>()));
            duration = 0.04;
        }


        public Frame(string name) {
            this.name = name;
            animations.Add(new Animation("Animation0", new VectorLayer("VectorLayer0"), new List<Transformation>()));
            duration = 0.04;
        }
        
        
        public System.Drawing.Bitmap Join() {
            var bm = new Bitmap(Editor.cartoon.Width, Editor.cartoon.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            g.CompositingMode = CompositingMode.SourceOver;
            g.Clear(System.Drawing.Color.White);
            foreach (var a in animations) {
                a.layers[0].Draw(new GraphicsDrawer(g));
            }
            g.Dispose();
            return bm;
        }


        public Frame GetByTime(int time) {
            var newFrame = new Frame();
            foreach (var a in animations) {
                newFrame.animations.Add(new Animation(a.GetByTime(time), new List<Transformation>()));
            }
            newFrame.animations.RemoveAt(0);
            return newFrame;
        }

        
        public object Clone() {
            var buf = new Frame(Name);
            buf.duration = duration;

            buf.animations.Clear();

            foreach (var a in animations) {
                var newLayer = new List<ILayer>();
                foreach (var l in a.layers) {
                    newLayer.Add((ILayer)l.Clone());
                }
                buf.animations.Add(new Animation(newLayer, new List<Transformation>()));
            }

            return buf;
        }


        internal List<string> SaveToFile(string pathToDrm) {
            List<string> lines = new List<string>();
            lines.Add($"Frame*{Name}*{duration}");
            foreach (var animation in animations) {
                lines.AddRange(animation.SaveToFile(pathToDrm));
            }
            return lines;
        }
    }
}