using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrawMoar
{
    public class Scene
    {
        /// <summary>
        /// Название (имя) сцены
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
             set
            {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+"))
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException("Frame name must contain only letters and numbers.");
                }
            }
        }

        public List<Frame> frames = new List<Frame>();

    }
}
