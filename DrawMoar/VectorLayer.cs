using System;

using System.Text.RegularExpressions;
using System.Drawing;

using DrawMoar.BaseElements;
using DrawMoar.Shapes;

namespace DrawMoar
{
    public class VectorLayer : ILayer
    {
        /// <summary>
        /// true - видимый слой, false - невидимый
        /// </summary>
        public bool Visible { get; set; }


        /// <summary>
        /// Совокупность всех наших фигур и есть пикча - содержимое слоя в общем
        /// </summary>
        public CompoundShape Picture { get; set; }


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


        public VectorLayer() {
            name = "newVectorLayer";
            Visible = true;
            Picture = new CompoundShape();
        }


        public VectorLayer(string name) {
            this.name = name;
            Visible = true;
            Picture = new CompoundShape();
        }


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


        public void AddShape(IShape shape) {
            Picture.shapes.Add(shape);
            // взм тут отрисовка на канвасе 
        }
    }
}
