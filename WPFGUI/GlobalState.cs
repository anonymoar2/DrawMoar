using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPFGUI
{
    /// <summary>
    /// Статический класс для хранения глобальных состояний редактора и инструментов
    /// </summary>
    internal static class GlobalState
    {
        public static bool PressLeftButton { get; set; }

        public static int СurrentFrameIndex { get; set; }

        public static Brush CurrentColor = Brushes.Black;

        public static Size BrushSize { get; set; }

        // текущий инструмент 

        // текущий слой, но с этим пока сложно
        
    }
    
}

