using System;

using System.Windows;
using System.Windows.Media;

using DrawMoar.BaseElements;
using System.Collections.Generic;

namespace DrawMoar
{
    /// <summary>
    /// Статический класс для хранения глобальных состояний редактора и инструментов
    /// </summary>
    internal static class GlobalState
    {
        public static bool PressLeftButton { get; set; }

        public static int Height { get; set; }
        public static int Width { get; set; }


        public static bool PressRightButton { get; set; }

        public static event EventHandler ChangeInstrument;

        public static string WorkingDirectory { get; set; }

        //public static Instruments.LightVector lightVector { get; set; }

        public static List<Transformation> CurrentTrans { get; set; }

        public static Tuple<ILayer, List<Transformation>, int> CurrentLayer { get; set; }
        public static Frame CurrentFrame { get; set; }
        public static Scene CurrentScene { get; set; }


        // Используется для изменения порядка слоев 
        public static int FramesCount { get; set; }

        // Испольузется для именования новых слоев
        public static int LayersIndexes { get; set; }
        public static Size canvSize { get; set; }

        private static Brush _color = Brushes.Red;
        public static Brush Color {
            get {
                return _color;
            }
            set {
                _color = value;
            }
        }

        private static Instrument _currentTool = Instrument.Arrow;
        public static Instrument CurrentTool {
            get {
                return _currentTool;
            }
            set {
                _currentTool = value;
                ChangeInstrument(value, null);
            }
        }


        private static Size _brushSize;
        public static Size BrushSize {
            get { return _brushSize; }
            set { _brushSize = value; }
        }

    }

}

