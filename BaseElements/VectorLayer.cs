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
        /// Название слоя
        /// </summary>
        private string name;
        public string Name {
            get {
                return name;
            }

            set {
                name = value;
            }
        }
        

        public bool Visible {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public void Draw() {
            throw new NotImplementedException();
        }
    }
}
