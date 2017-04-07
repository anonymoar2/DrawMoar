﻿using System;

using System.Text.RegularExpressions;
using System.Drawing;

using DrawMoar.BaseElements;
using DrawMoar.Shapes;
using System.Windows;
using System.Collections.Generic;

namespace DrawMoar
{
    public class VectorLayer : ILayer
    {
        /// <summary>
        /// true - видимый слой, false - невидимый
        /// </summary>
        public bool Visible { get; set; }


        /// <summary>
        /// Совокупность всех наших фигур и есть пикча - содержимое слоя в общем
        /// </summary>
        public CompoundShape Picture { get; set; }


        /// <summary>
        /// Название (имя) слоя
        /// </summary>
        private string name;
        public string Name {
            get { return name; }
            set {
                // TODO: Изменить регулярное выражение на более подходящее
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Название слоя должно состоять только " +
                                                "из латинских букв и цифр.");
                }
            }
        }


        private System.Windows.Point position = new System.Windows.Point(0, 0);
        public System.Windows.Point Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }

        public List<Text> Text { get; private set; }


        public VectorLayer() {
            name = "newVectorLayer";
            Visible = true;
            Picture = new CompoundShape();
            Text = new List<Text>();
        }


        public VectorLayer(string name) {
            this.name = name;
            Visible = true;
            Picture = new CompoundShape();
            Text = new List<Text>();
        }

        
        public void Draw(Graphics g) {
            Picture.Draw(g);
            foreach (var element in Text)
            {
                element.Draw(g);
            }
        }


        /// <summary>
        /// Тупо вывод на экране при переключении между слоями, но его не будет, если канвасы накладываем друг на друга
        /// </summary>
        /// <param name="bitmap"></param>
        public void Print() {
            // Проходим по фигурам, отрисовывая их на экране
        }


        public void Transform(Transformation transformation) {
            Picture.Transform(transformation);
        }


        public void AddShape(IShape shape) {
            Picture.shapes.Add(shape);
            // взм тут отрисовка на канвасе 
        }


        public RasterLayer ToRasterLayer() {
            var newLayer = new RasterLayer();
            var g = Graphics.FromImage(newLayer.Picture.Image);
            Picture.Draw(g);
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
            g.Clear(System.Drawing.Color.White);
            foreach (var sh in Picture.shapes) {
                sh.Draw(g);
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

            foreach (var element in Text)
            {
                buf.Text.Add(element);
            }

            return buf;
        }
    }
}
