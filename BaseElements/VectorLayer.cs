using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Drawing;

namespace BaseElements
{
    public class VectorLayer : ILayer
    {
        /// <summary>
        /// Нужен для того чтобы лепить видяшки
        /// </summary>
        private Bitmap bitmap = new Bitmap(0, 0);
        public Bitmap Bitmap {
            get {
                return bitmap;
            }

            set {
                /// Здесь и будет то как мы будем это конвертить в bitmap
                throw new NotImplementedException();
            }
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
        /// true - видимый слой, false - невидимый
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
        /// Пока ничего
        /// </summary>
        public void Draw() {
        }
    }
}
