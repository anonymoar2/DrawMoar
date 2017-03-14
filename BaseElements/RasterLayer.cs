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
        /// Место на холсте куда будет накладываться левый верхний угол изображения
        /// </summary>
        Size Position { get; set; }

        /// <summary>
        ///  Угол поворота изображения
        /// </summary>
        float Rotation { get; set; }


        public void Draw() {
            throw new NotImplementedException();
        }
    }
}
