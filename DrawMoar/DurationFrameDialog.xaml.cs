using System;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using DrawMoar.BaseElements;


namespace DrawMoar
{
    /// <summary>
    /// Логика взаимодействия для DurationFrameDialog.xaml
    /// </summary>
    public partial class DurationFrameDialog : Window
    {
        ListBox framesList;
        public DurationFrameDialog(ListBox frameList) {
            InitializeComponent();
            framesList = frameList;
        }


        private void cancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void addFrame_Click(object sender, RoutedEventArgs e) {
            if (periodOfTime.Text == "") {
                MessageBox.Show("You haven't entered the name");
            }
            else {
                try {
                    var time = float.Parse(periodOfTime.Text);
                    if (time <= 0) {
                        throw new FormatException();
                    }

                    BaseElements.Frame frame = new BaseElements.Frame() {
                        duration = time
                    };
                    Cartoon.CurrentScene.frames.Add(frame);
                    Cartoon.CurrentFrame = Cartoon.CurrentScene.frames.Last();
                    Cartoon.CurrentLayer = Cartoon.CurrentFrame.layers.Last();
                    var frames = Cartoon.CurrentScene.frames;

                    var lbl = new Label() {
                        Content = $"{Cartoon.CurrentFrame.Name} = {frame.duration} sec"
                    };
                    framesList.Items.Add(lbl);
                    framesList.SelectedIndex = framesList.Items.Count - 1;

                    Hide();
                }
                catch (FormatException) {
                    MessageBox.Show("Время должны быть больше 0.");
                }
            }
        }
    }
}