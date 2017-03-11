using BaseElements;
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
                    this.Hide();
                    MainWindow mw = (MainWindow)this.Owner;
                    var cartoon = new Cartoon(CartoonName, CartoonWidth, CartoonHeight,/*этот параметр потом будет путь до папки temp, но пока не так важно*/ @"C:\Users\Никита\Desktop\temp");
                    mw.Success(cartoon);

                }
                catch(FormatException)
                {
                    MessageBox.Show("Enter integer height and width bigger than zero");
                }
                //по идее, здесь должен быть еще один catch, но не слишком ли много для этой кнопки? мб ловить Argument в другом месте?
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
