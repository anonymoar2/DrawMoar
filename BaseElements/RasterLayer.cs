using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BaseElements
{
    /// TODO: Реализовать растровый слой
    public class RasterLayer : ILayer
    {
        /// <summary>
        /// По сути сама картинка-растровый слой
        /// </summary>
        private Bitmap bitmap;


        /// <summary>
        /// Возвращат bitmap из картинки
        /// </summary>
        /// <returns>bitmap</returns>
        public Bitmap GetBitmap() {
            return bitmap;
        }


        public RasterLayer(Bitmap bitmap) {
            this.bitmap = bitmap;
        }


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


        /// <summary>
        /// true - слой видимый, false - невидимый
        /// </summary>
        private bool visible = true;
        public bool Visible {
            get {
                return visible;
            }
            set {
                visible = value;
            }
        }


        /// <summary>
        /// Место на холсте куда будет накладываться левый верхний угол изображения
        /// </summary>
        private Size position = new Size(0, 0);
        public Size Position {
            get {
                return position;
            }
            set {
                // Возможность принадлежности к холсту
                position = value;
            }
        }


        /// <summary>
        ///  Угол поворота изображения
        /// </summary>
        private float rotation = 0f;
        public float Rotation {
            get {
                return rotation;
            }
            set {
                rotation = value;
            }
        }


        /// <summary>
        /// Ну тут ничего в принципе
        /// </summary>
        public void Draw() {
        }
    }
}
