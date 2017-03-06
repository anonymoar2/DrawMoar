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

        public string CartoonName { get; set; }
        public int CartoonHeight { get; set; }
        public int CartoonWidth { get; set; }

        //!!!ОЧЕНЬ КРИВО!!!
        //часть проверок есть в Cartoon

        private void creating_Click(object sender, RoutedEventArgs e)
        {
            //после презентации, когда будет время сделаю нормально
            if (getName.Text == "") MessageBox.Show("You haven't entered the name");
            else if (getHeight.Text == "") MessageBox.Show("You haven't entered the height");
            else if (getWidth.Text == "") MessageBox.Show("You haven't entered the width");
            else
            {
                try
                {
                    this.CartoonName = getName.Text;
                    this.CartoonHeight = Int32.Parse(getHeight.Text);
                    this.CartoonWidth = Int32.Parse(getWidth.Text);
                    if (CartoonHeight <= 0 || CartoonWidth <= 0) throw new FormatException();
                    this.Hide();
                    MainWindow mw = (MainWindow)this.Owner;
                    mw.Success(CartoonName, CartoonHeight, CartoonWidth);
                }
                catch(FormatException)
                {
                    MessageBox.Show("Enter integer height and width bigger than zero");
                }
            }
        }

        private void abortion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
