using System;
using System.Collections.Generic;

using System.Drawing;
using System.Text.RegularExpressions;

using DrawMoar.BaseElements;
using DrawMoar.Extensions;


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


        /// <summary>
        /// TODO: запилить метод
        /// WARNING: Потенциальная ошибка мб здесь (возможно слой не копируется, а туда пихается на него ссылка и крч
        /// изменяется тот который типа мы хотели скопировать вместе с "копией")
        /// Nk_dev: Возможно меняется ориинал
        /// </summary>
        /// <param name="transforations">трансформации над слоем</param>
        /// <returns></returns>
        public ILayer Transform(List<ITransformation> transforations) {
            var layer = this.Clone();
            // var maxSpeed = transforations.MaxBy(x => x.Speed);
        }


        public Image Image { get;  set; }


        public int Speed { get; set; }
    }
}
