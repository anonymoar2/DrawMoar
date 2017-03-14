using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseElements;
using System.Text.RegularExpressions;
using System.Drawing;

namespace DrawMoar
{
    class RasterLayerView : RasterLayer
    {
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
        /// True - слой видимый, false - невидимый
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
        /// 
        /// </summary>
        /// 
        private Size position;
        public Size Position {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Draw() {
            throw new NotImplementedException();
        }
    }
}
