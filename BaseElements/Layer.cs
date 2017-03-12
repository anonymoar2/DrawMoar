using System;
using System.Text.RegularExpressions;

namespace BaseElements
{
    public abstract class Layer
    {
        /// <summary>
        /// Состояние видимости слоя. 
        /// true - слой видимый, false - невидимый.
        /// При экспорте кадра в картинку картинка получается только из видимых слоёв.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Название слоя.
        /// </summary>
        protected string name;

        /// <summary>
        /// Название слоя.
        /// </summary>
        public string Name {
            get { return name; }
            internal set {
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
        /// Сохранение слоя.
        /// </summary>
        /// <param name="WorkingDirectory">Директория в которую сохраняется слой.</param>
        public abstract void Save(string WorkingDirectory);
    }
}
