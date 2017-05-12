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

        public static event EventHandler ChangeInstrument;

        public static string WorkingDirectory { get; set; }

        public static int TotalTime { get; set; }

        public static Tuple<ILayer, List<Transformation>, int> CurrentLayer { get; set; }
        public static Frame CurrentFrame { get; set; }
        public static Scene CurrentScene { get; set; }

        public static Size canvSize { get; set; }

        private static Brush color = Brushes.Red;
        public static Brush Color {
            get {
                return color;
            }
            set {
                color = value;
            }
        }

        private static Instrument currentTool = Instrument.Arrow;
        public static Instrument CurrentTool {
            get {
                return currentTool;
            }
            set {
                currentTool = value;
                ChangeInstrument(value, null);
            }
        }

        private static Size brushSize;
        public static Size BrushSize {
            get { return brushSize; }
            set { brushSize = value; }
        }
    }
}

