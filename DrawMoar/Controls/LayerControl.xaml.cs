using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaseElements;
using System.ComponentModel;
using BaseElements.Instruments;

namespace DrawMoar
{
    /// <summary>
    /// Контрол, представляющий экземпляр слоя для рисования
    /// </summary>
    public partial class LayerControl : UserControl, INotifyPropertyChanged
    {

        public event EventHandler Delete;

        //public event EventHandler<CheckedEventArgs> CheckedChanged;
        public class CheckedEventArgs : EventArgs
        {
            public bool IsChecked { get; set; }
        }

        private string _layerName;
        public string LayerName
        {
            get
            {
                return _layerName;
            }
            set
            {
                _layerName = value;
                OnPropertyChanged("LayerName");
            }
        }

        public int LayerIndex { get; set; }
        public bool LayerFocus { get; set; }

        private Point _clickPosition;

        /// <summary>
        /// Конструктор
        /// </summary>
        public LayerControl()
        {
            InitializeComponent();
            LayerIndex = GlobalState.FramesCount++;
            // Устанавливаем самый большой индекс, для отображения поверх всех существующих слоев
            Panel.SetZIndex(this, LayerIndex);

            LayerName = String.Format("{0}_{1}", "NewLayer", GlobalState.LayersIndexes + 1);

        }


        private void Del(Object sender, EventArgs e)
        {
            //GlobalState.LayerCount--;
            Delete(this, e);
        }

        /// <summary>
        /// Фиксация состояния, для начала перемещения слоя по холсту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*if (GlobalState.CurrentTool == Instruments.Arrow)
            {
                GlobalState.PressLeftButton = true;
                var draggableControl = (UserControl)sender;
                _clickPosition = e.GetPosition(this);
                draggableControl.CaptureMouse();
            }*/
        }

        /// <summary>
        /// Фиксация состояния при окончании перемещения слоя по холсту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*if (GlobalState.CurrentTool == Instruments.Arrow)
            {
                GlobalState.PressLeftButton = false;
                var draggable = (UserControl)sender;
                draggable.ReleaseMouseCapture();
            }*/
        }

        /// <summary>
        /// Перемещение слоя при зажатой левой кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerControl_MouseMove(object sender, MouseEventArgs e)
        {
            /*if (GlobalState.CurrentTool == Instruments.Arrow)
            {
                var draggableControl = (UserControl)sender;

                if (GlobalState.PressLeftButton && draggableControl != null)
                {
                    Point currentPosition = e.GetPosition((UIElement)Parent);

                    var transform = draggableControl.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        draggableControl.RenderTransform = transform;
                    }

                    transform.X = currentPosition.X - _clickPosition.X;
                    transform.Y = currentPosition.Y - _clickPosition.Y;
                }
            }*/
        }

        /// <summary>
        /// Выделение границ слоя при фокусе виджета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            VisualHost.IsFocused = true;
            VisualHost.FocusSpace();
        }

        /// <summary>
        /// Возвращение состояния границ слоя при потере фокуса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NonFocus(object sender, RoutedEventArgs e)
        {
            VisualHost.IsFocused = false;
            VisualHost.UnFocusSpace();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
