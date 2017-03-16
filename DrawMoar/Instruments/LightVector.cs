using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMoar.Instruments
{
    class LightVector : IBrush
    {
        private List<Size> points;


        /// <summary>
        /// Добавление точки в конец списка
        /// </summary>
        /// <param name="newPoint">координаты новой точки</param>
        public void Push(Size newPoint) {
            points.Add(newPoint);
        }


        /// <summary>
        /// TODO: Переписать красиво
        /// Проверка списка точек на пустоту
        /// </summary>
        /// <returns>true - список пустой, false - не пустой</returns>
        public bool IsEmpty() {
            if (points.Count != 0) {
                return false;
            }
            else {
                return true;
            }
        }


        /// <summary>
        /// Вызывать каждый раз после нажатия на холст после того как вызвали старт
        /// </summary>
        /// <param name="newPoint"></param>
        private void DrawOneSegment(Size newPoint) {
            if (this.active) {
                if (!this.IsEmpty()) {
                    /// Тут добавляем на канвас линию с координатами (points.Last(); newPoint)
                }
                this.Push(newPoint);
                /// Вызываем метод который будет тащить за собой линию от этой точки(последней в списке) до клика
                /// (метод тащит линию, а потом прекращается при клике на холст)
            }
        }


        public Color MainColor {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public int Width {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }


        private bool active = false;

        /// <summary>
        /// То что происходит при нажатии на кнопку нашего вектора
        /// </summary>
        public void Start() {
            active = true;
            /// Если текущий слой не LightVectorLayer
            /// Создаем новый слой типа LightVectorLayer
            /// 
            /// Если список точек НЕ пустой
            /// Вызываем метод который будет тащить за собой линию от этой точки до клика
        }
    }
}
