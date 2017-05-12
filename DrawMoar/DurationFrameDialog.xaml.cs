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
    /// Логика взаимодействия для DurationFrameDialog.xaml
    /// </summary>
    public partial class DurationFrameDialog : Window
    {
        ListBox framesList;
        public DurationFrameDialog(ListBox frameList)
        {
            InitializeComponent();
            framesList = frameList;
        }


        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addFrame_Click(object sender, RoutedEventArgs e)
        {
            if (periodOfTime.Text == "")
            {
                System.Windows.MessageBox.Show("You haven't entered the name");
            }
            else
            {
                try
                {
                    var time = float.Parse(periodOfTime.Text);
                    if (time <= 0 ) throw new FormatException();

                    BaseElements.Frame frame = new BaseElements.Frame();
                    frame.duration = time;
                    GlobalState.CurrentScene.frames.Add(frame);
                    GlobalState.CurrentFrame = GlobalState.CurrentScene.frames.Last();
                    GlobalState.CurrentLayer = GlobalState.CurrentFrame.layers.Last();
                    var frames = GlobalState.CurrentScene.frames;
                                   
                    var lbl = new Label();
                    lbl.Content = $"{GlobalState.CurrentFrame.Name}  =  {frame.duration} sec";
                    framesList.Items.Add(lbl);
                    framesList.SelectedIndex = framesList.Items.Count - 1;

                    this.Hide();
                }
                catch (FormatException)
                {
                    System.Windows.MessageBox.Show("Время должны быть больше 0.");
                }
            }
        }
    }
}
