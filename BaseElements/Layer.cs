using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseElements
{
    // I don't yet know why it's internal 
    internal abstract class Layer
    {
        // true - слой видимый, false - невидимый, при экспорте кадра в картинку 
        // картинка получается только из видимых слоёв
        public bool Visible { get; set; }
        private string name;

        /// <summary>
        /// Layer's name.
        /// </summary>
        public string Name {
            get { return name; }
            private set {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Cartoon name must contain only letters and numbers.");
                }
            }
        }

        // Какие-то ещё параметры в будущем возможно

        
        /// <summary>
        /// Save layer in WorkingDirectory
        /// </summary>
        /// <returns></returns>
        public virtual void Save(string WorkingDirectory) {
            // xz
        }
        
        // ещё тут будет transformation, чуть позже добавлю, пока думаю и так работы хватит
        
        
    }
}
