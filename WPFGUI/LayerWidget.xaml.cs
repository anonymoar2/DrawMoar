using System;
using System.Collections.Generic;
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
    /// Контрол для отображения слоев в каком-либо списке
    /// </summary>
    public partial class LayerWidget : UserControl
    {
        /// <summary>
        /// Ссылка на экземпляр родительского слоя
        /// </summary>
        public readonly LayerControl ThisLayer;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="layerControl"></param>
        public LayerWidget(LayerControl layerControl) {
            ThisLayer = layerControl;
            DataContext = ThisLayer;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            
        }

        

        

        

        /// <summary>
        /// Изменение фоновй заливки слоя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeFillColor(object sender, RoutedEventArgs e) {
            ThisLayer.VisualHost.ChangeFill(((Button)sender).Background);
        }

        


    }
}
