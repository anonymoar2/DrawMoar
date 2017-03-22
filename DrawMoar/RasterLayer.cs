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
        //TODO: Конструктор


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
        /// Тут пока только на самом Image рисует, надо ещё с canvas связать
        /// </summary>
        public void Draw(Graphics g) { 
            g.DrawImage(Image, Position.X, Position.Y);
        }


        //При переключении слоёв рисует содержимое Image на экране
        public void Print() {
            
        }


        public void Transform(Transformation transformation) {
            // TODO метод
        }
        

        public Image Image { get;  set; }


        public int Speed { get; set; }
    }
}
