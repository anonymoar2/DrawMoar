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
using System.Windows.Shapes;

namespace DrawMoar {
    /// <summary>
    /// Логика взаимодействия для GenTimeDialog.xaml
    /// </summary>
    public partial class GenTimeDialog : Window {
        public GenTimeDialog() {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Apply_Click(object sender, RoutedEventArgs e) {
            try {
                Editor.cartoon.CurrentScene.Generate(Editor.cartoon.CurrentFrame, Int32.Parse(TotalTime.Text), Int32.Parse(FrameDuration.Text));
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            MainWindow mw = (MainWindow)Owner;
            mw.scenesList_SelectionChanged(null, null);
            mw.Refresh();
            Hide();
        }
    }
}
