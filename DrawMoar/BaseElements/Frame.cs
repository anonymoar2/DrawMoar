using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Frame name must contain only letters and numbers.");
                }
            }
        }


        public List<ILayer> layers = new List<ILayer>();


        public Frame() {
            name = "newFrame";
            layers.Add(new VectorLayer("Vector_Layer_0"));
        }


        public Frame(string name) {
            this.name = name;
            layers.Add(new VectorLayer("Vector_Layer_0"));
        }
        
        
        public System.Drawing.Bitmap Join() {
            var bm = new Bitmap(450, 450, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bm);
            g.CompositingMode = CompositingMode.SourceOver;
            foreach (var l in layers) {
                g.DrawImage(l.GetImage(450,450), 0, 0);
                g.Dispose();
            }
            return bm;
        }

        public object Clone()
        {
            var buf = new Frame(Name);

            foreach(var layer in layers)
            {
                buf.layers.Add((ILayer)layer.Clone());
            }

            return buf;
        }


        // Использовать только если для каждого фрейма будет своя директория.
        //private string workingDirectory;
        //public string WorkingDirectory {
        //    get {
        //        return workingDirectory;
        //    }
        //    private set {
        //        if (Directory.Exists(value)) {
        //            workingDirectory = value;
        //        }
        //        else if (Directory.Exists(Path.GetDirectoryName(value))) {
        //            // TODO: handle all possible exceptions here and rethrow ArgumentException.
        //            Directory.CreateDirectory(value);
        //            workingDirectory = value;
        //        }
        //        else {
        //            throw new ArgumentException($"Can't open directory \"{value}\".");
        //        }
        //    }
        //}
    }
}