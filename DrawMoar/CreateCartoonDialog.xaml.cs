using System;
using System.Windows;
using System.Windows.Forms;

using BaseElements;
using System.IO;
using System.Windows.Input;

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

        private void creating_Click(object sender, RoutedEventArgs e) {

            if (getName.Text == "") {
                System.Windows.MessageBox.Show("You haven't entered the name");
            }
            else if (getHeight.Text == "") {
                System.Windows.MessageBox.Show("You haven't entered the height");
            }
            else if (getWidth.Text == "") {
                System.Windows.MessageBox.Show("You haven't entered the width");
            }
            else {
                try {
                    var cartoonName = getName.Text;
                    var cartoonHeight = Int32.Parse(getHeight.Text);
                    var cartoonWidth = Int32.Parse(getWidth.Text);
                    if (cartoonHeight <= 0 || cartoonWidth <= 0) throw new FormatException();

                    var folderDDialog = new FolderBrowserDialog();
                    folderDDialog.ShowDialog();
                    string selectedDirectory = folderDDialog.SelectedPath;
                    if (selectedDirectory == "") System.Windows.MessageBox.Show("You haven't chosen the folder");
                    else {
                        string workingDirectory = Path.Combine(selectedDirectory, cartoonName);
                        Directory.CreateDirectory(workingDirectory);
                        try {
                            var cartoon = new Cartoon(cartoonName, cartoonWidth, cartoonHeight, workingDirectory);
                            MainWindow mw = (MainWindow)Owner;
                            mw.Success(cartoon);
                            Hide();
                        }
                        catch (ArgumentException exeption) {
                            System.Windows.MessageBox.Show(exeption.Message);
                        }
                    }
                }
                catch (FormatException) {
                    System.Windows.MessageBox.Show("Ширина и высота холста должны быть больше 0.");
                }
            }
        }

        private void abortion_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        }

        // PreviewTextInput="PreviewTextInput"

        //private void getHeight_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        //    if (!char.IsDigit(e.Text, e.Text.Length - 1))
        //        e.Handled = true;
        //}

        //private void getWidth_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        //    if (!char.IsDigit(e.Text, e.Text.Length - 1))
        //        e.Handled = true;
        //}

    }
}
