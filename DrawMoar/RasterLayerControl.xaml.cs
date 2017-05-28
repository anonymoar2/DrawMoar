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

namespace DrawMoar {
    /// <summary>
    /// Логика взаимодействия для RasterLayerControl.xaml
    /// </summary>
    public partial class RasterLayerControl : UserControl {
        public RasterLayerControl() {
            InitializeComponent();
        }

        public void Rotate(float angle) {
            this.RenderTransform = new RotateTransform(angle);
        }
    }
}
