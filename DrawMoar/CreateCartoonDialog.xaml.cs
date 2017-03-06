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

namespace DrawMoar
{
    /// <summary>
    /// Логика взаимодействия для CreateCartoonDialog.xaml
    /// </summary>
    public partial class CreateCartoonDialog : Window
    {
        public CreateCartoonDialog()
        {
            InitializeComponent();
        }

            //нужно грамотно реализовать инкапсуляцию и бросание/ловлю исключений
            //бросать в свойстве, ловить в МейнФорме - ?
            //часть проверок есть в Cartoon
            //названия элементов этой формы - под вопросом (во избежание путанницы будем менять)
        private void creating_Click(object sender, RoutedEventArgs e)
        {

        }

        private void abortion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
