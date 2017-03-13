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
        public Bitmap bitmap {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public string Name {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
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

        /// <summary>
        /// Место на холсте куда будет накладываться что-то, в общем важна величина "откуда мы рисуем" - смещение по X и Y
        /// </summary>
        Size Position { get; set; }

        /// <summary>
        /// Толщина линии/контура/всего крч, ибо тудем линиями отрисовывать
        /// </summary>
        int Width { get; set; }

        public void Draw() {
            throw new NotImplementedException();
        }
    }
}
