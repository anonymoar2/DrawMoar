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
        public RasterLayer() {

        }

        public virtual void Draw() {
            throw new NotImplementedException();
        }
    }
}
