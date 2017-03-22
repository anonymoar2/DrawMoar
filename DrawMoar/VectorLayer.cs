using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Drawing;

using DrawMoar.BaseElements;


namespace DrawMoar
{
    public class VectorLayer : ILayer
    {
        // TODO: Конструктор

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
        public bool Visible { get; set; }


        public Point Position { get; set; }


        /// <summary>
        /// Пока ничего
        /// </summary>
        public void Draw(Graphics g) {
            //Picture.Draw(g);
        }


        /// <summary>
        /// Тупо вывод на экране при переключении между слоями, но его не будет, если канвасы накладываем друг на друга
        /// </summary>
        /// <param name="bitmap"></param>
        public void Print() {
            // Проходим по фигурам, отрисовывая их на экране
        }

        public void Transform(Transformation transformation) {
            Picture.Transform(transformation);
        }


        public CompoundShape Picture { get; set; }
    }
}
