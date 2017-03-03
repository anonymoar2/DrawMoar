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

namespace WPFGUI
{
    /// <summary>
    /// Контрол, представляющий экземпляр слоя для рисования
    /// </summary>
    public partial class LayerControl : UserControl, INotifyPropertyChanged
    {
        #region Свойства, переменные, события

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
                OnPropertyChanged("LayerName");
            }
        }

        public int LayerIndex { get; set; }
        public bool LayerFocus { get; set; }
        internal LayerWidget Widget { get; set; }

        private Point _clickPosition;
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public LayerControl() {
            InitializeComponent();
            LayerIndex = GlobalState.LayersCount++;
            // Устанавливаем самый большой индекс, для отображения поверх всех существующих слоев
            Panel.SetZIndex(this, LayerIndex);

            Widget = new LayerWidget(this);
            LayerName = String.Format("{0}_{1}", "NewLayer", GlobalState.LayersIndexes + 1);

            Widget.WidgetCheckBox.Checked += SetLayerVisibility;
            Widget.WidgetCheckBox.Unchecked += SetLayerVisibility;
            Widget.WidgetDel.Click += Del;

        }

        /// <summary>
        /// Восстановить видимость слоя, в зависимости от состояния виджета
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetLayerVisibility(Object sender, EventArgs e) {
            
                    Visibility = Visibility.Visible;
                    CheckedChanged(this, new CheckedEventArgs { IsChecked = true });
                    
                    Visibility = Visibility.Hidden;
                    CheckedChanged(this, new CheckedEventArgs { IsChecked = false });
                    
            }

        }

        

    }

