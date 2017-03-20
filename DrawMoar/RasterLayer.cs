using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DrawMoar
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
                if (Regex.IsMatch(value, @"\w")) {
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
        public bool Visible { get; set; }


        /// <summary>
        /// Место на холсте куда будет накладываться левый верхний угол изображения
        /// </summary>
        public Point Position { get; set; }


        /// <summary>
        /// Ну тут ничего в принципе
        /// </summary>
        public void Draw(Graphics g) {
            g.DrawImage(Image, Position.X, Position.Y);
        }


        public Image Image { get;  set; }

    }
}
