using System;
using System.Windows;

using BaseElements;

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
                    var CartoonName = getName.Text;
                    var CartoonHeight = Int32.Parse(getHeight.Text);
                    var CartoonWidth = Int32.Parse(getWidth.Text);
                    if (CartoonHeight <= 0 || CartoonWidth <= 0) throw new FormatException();
                    Hide();
                    MainWindow mw = (MainWindow)Owner;

                    var cartoon = new Cartoon(CartoonName, CartoonWidth, CartoonHeight, System.IO.Path.GetTempPath());
                    mw.Success(cartoon);

                }
                catch (FormatException)
                {
                    MessageBox.Show("Enter integer height and width bigger than zero");
                }
            }
        }

        private void abortion_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}
