using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPFEDITOR
{
    /// <summary>
    /// Контрол, представляющий экземпляр кадра
    /// </summary>
    public partial class FrameControl : UserControl, INotifyPropertyChanged
    {

        public event EventHandler Delete;

        public event EventHandler<CheckedEventArgs> CheckedChanged;
        public class CheckedEventArgs : EventArgs
        {
            public bool IsChecked { get; set; }
        }

        private string _layerName;
        public string LayerName {
            get {
                return _layerName;
            }
            set {
                _layerName = value;
                OnPropertyChanged("FrameName");
            }
        }

        internal FrameWidget Widget { get; set; }

        public int FrameIndex { get; set; }
        public bool LayerFocus { get; set; }
        //internal LayerWidget Widget { get; set; }

        private Point _clickPosition;

        public FrameControl() {

            InitializeComponent();
            FrameIndex = GlobalState.FramesCount++;
            // Устанавливаем самый большой индекс, для отображения поверх всех существующих слоев
            Panel.SetZIndex(this, FrameIndex);

            Widget = new FrameWidget(this);
            LayerName = String.Format("{0}_{1}", "NewLayer", GlobalState.FramesIndexes + 1);

            //Widget.WidgetCheckBox.Checked += SetLayerVisibility;
            //Widget.WidgetCheckBox.Unchecked += SetLayerVisibility;
            //Widget.WidgetDel.Click += Del;
        }

        /// <summary>
        /// Восстановить видимость слоя, в зависимости от состояния виджета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void SetLayerVisibility(Object sender, EventArgs e) {
        //    switch (Widget.WidgetCheckBox.IsChecked) {
        //        case true:
        //            Visibility = Visibility.Visible;
        //            CheckedChanged(this, new CheckedEventArgs { IsChecked = true });
        //            break;
        //        case false:
        //            Visibility = Visibility.Hidden;
        //            CheckedChanged(this, new CheckedEventArgs { IsChecked = false });
        //            break;
        //    }

        //}

        private void Del(Object sender, EventArgs e) {
            GlobalState.FramesCount--;
            Delete(this, e);
        }

        

        /// <summary>
        /// Выделение границ слоя при фокусе виджета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_GotFocus(object sender, RoutedEventArgs e) {
            Draw.IsFocused = true;
            Draw.FocusSpace();
        }

        /// <summary>
        /// Возвращение состояния границ слоя при потере фокуса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NonFocus(object sender, RoutedEventArgs e) {
            Draw.IsFocused = false;
            Draw.UnFocusSpace();
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
