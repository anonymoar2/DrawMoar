using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using BaseElements.Instruments;

namespace DrawMoar
{
    /// <summary>
    /// Статический класс для хранения глобальных состояний редактора и инструментов
    /// </summary>
    internal static class GlobalState
    {
        public static bool PressLeftButton { get; set; }

        public static event EventHandler ChangeInstrument;
        public static event EventHandler ChangeColor;

        // Используется для изменения порядка слоев 
        public static int FramesCount { get; set; }

        // Испольузется для именования новых слоев
        public static int LayersIndexes { get; set; }
        public static Size canvSize { get; set; }

        private static Brush _color = Brushes.Black;
        public static Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        private static Instruments _currentTool = Instruments.Arrow;
        public static Instruments CurrentTool
        {
            get
            {
                return _currentTool;
            }
            set
            {
                _currentTool = value;
                ChangeInstrument(value, null);
            }
        }

        private static Size _brushSize;
        public static Size BrushSize
        {
            get { return _brushSize; }
            set { _brushSize = value; }
        }

    }

}

