using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;

using DrawMoar.BaseElements;


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

        private void creating_Click(object sender, RoutedEventArgs e)
        {

            if (getName.Text == "")
            {
                System.Windows.MessageBox.Show("You haven't entered the name");
            }
            else if (getHeight.Text == "")
            {
                System.Windows.MessageBox.Show("You haven't entered the height");
            }
            else if (getWidth.Text == "")
            {
                System.Windows.MessageBox.Show("You haven't entered the width");
            }
            else
            {
                try
                {
                    var cartoonName = getName.Text;
                    var cartoonHeight = Int32.Parse(getHeight.Text);
                    var cartoonWidth = Int32.Parse(getWidth.Text);
                    if (cartoonHeight <= 0 || cartoonWidth <= 0) throw new FormatException();
                    var folderDDialog = new FolderBrowserDialog();
                    folderDDialog.ShowDialog();
                    string selectedDirectory = folderDDialog.SelectedPath;
                    if (selectedDirectory == "") System.Windows.MessageBox.Show("You haven't chosen the folder");
                    else
                    {
                        string workingDirectory = Path.Combine(selectedDirectory, cartoonName);
                        Directory.CreateDirectory(workingDirectory);
                        try
                        {
                            Editor.cartoon = new Cartoon(cartoonName, cartoonWidth, cartoonHeight, workingDirectory);
                            Editor.cartoon.CurrentScene = Editor.cartoon.scenes[0];
                            Editor.cartoon.WorkingDirectory = workingDirectory;
                            Editor.cartoon.Width = cartoonWidth;
                            Editor.cartoon.Height = cartoonHeight;
                            
                            MainWindow mw = (MainWindow)Owner;
                            mw.Success(Editor.cartoon);
                            Hide();
                        }
                        catch (ArgumentException exeption)
                        {
                            System.Windows.MessageBox.Show(exeption.Message);
                        }
                    }
                }
                catch (FormatException)
                {
                    System.Windows.MessageBox.Show("Ширина и высота холста должны быть больше 0.");
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
