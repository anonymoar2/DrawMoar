using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawMoar
{
    /// <summary>
    /// Статический класс для хранения глобальных состояний редактора и инструментов
    /// </summary>
    internal static class GlobalState
    {
        public static bool PressLeftButton { get; set; }
        

        // Используется для изменения порядка слоев 
        public static int LayersCount { get; set; }

        // Испольузется для именования новых слоев
        public static int LayersIndexes { get; set; }

        public static Brush Color = Brushes.Black;
        
        

        private static Size _brushSize;
        public static Size BrushSize {
            get { return _brushSize; }
            set { _brushSize = value; }
        }
    }
    
}

