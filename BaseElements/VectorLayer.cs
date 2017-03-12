using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using BaseElements.Figures;
using System.Text.RegularExpressions;

namespace BaseElements
{
    public class VectorLayer : ILayer
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


        public VectorLayer() {

        }

        public virtual void Draw() {
            throw new NotImplementedException();
        }
    }
}
