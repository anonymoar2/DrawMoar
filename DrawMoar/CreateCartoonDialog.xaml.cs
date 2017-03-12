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
        public CreateCartoonDialog() {
            InitializeComponent();
        }

        //!!!ОЧЕНЬ КРИВО!!!
        //часть проверок есть в Cartoon
        private void creating_Click(object sender, RoutedEventArgs e) {
            //после презентации, когда будет время сделаю нормально
            if (getName.Text == "") MessageBox.Show("You haven't entered the name");
            else if (getHeight.Text == "") MessageBox.Show("You haven't entered the height");
            else if (getWidth.Text == "") MessageBox.Show("You haven't entered the width");
            else {
                try {
                    var CartoonName = getName.Text;
                    var CartoonHeight = Int32.Parse(getHeight.Text);
                    var CartoonWidth = Int32.Parse(getWidth.Text);
                    if (CartoonHeight <= 0 || CartoonWidth <= 0) throw new FormatException();
                    Hide();
                    MainWindow mw = (MainWindow)Owner;

                    // TODO: 1) Спросить у пользователя через диалог в каком месте создавать
                    // рабочую директорию мультфильма. 2) Проверить возможность создания 
                    // такой директории. 3) Создать или выдать предупреждение и попросить
                    // попробовать снова.
                    var dummyPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Desktop\test");
                    var cartoon = new Cartoon(CartoonName, CartoonWidth, CartoonHeight, dummyPath);
                    mw.Success(cartoon);

                }
                catch (FormatException) {
                    MessageBox.Show("Ширина и высота холста должны быть больше 0.");
                }
            }
        }

        private void abortion_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        }
    }
}
