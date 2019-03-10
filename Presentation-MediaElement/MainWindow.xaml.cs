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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation_MediaElement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Uri _presentation;
        public Uri Presentation { get { return _presentation; } set { _presentation = value; this.MediaPlayer.Source = _presentation; if (PWindow != null) PWindow.Presentation = _presentation; } }

        PresentationWindow PWindow = null;

        public MainWindow()
        {
            InitializeComponent();

            PWindow = new PresentationWindow();
            PWindow.Show();

            Presentation = new Uri(@"E:\TestVideos\1 Minute Test.mkv");
        }

        private void VideoPathTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Presentation = new Uri(VideoPathTB.Text.Replace("\"", ""));
        }

        private void SynchronisePositions()
        {
            this.MediaPlayer.Position = PWindow.GetTime();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (MediaPlayer.GetMediaState() == MediaState.Play)
            {
                this.MediaPlayer.Pause();
                PWindow.Pause();
            }
            else
            {
                this.MediaPlayer.Play();
                PWindow.Play();
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.MediaPlayer.Stop();
            PWindow.Stop();
        }

        private void NextChapterButton_Click(object sender, RoutedEventArgs e)
        {
            //this.VLCPlayer.MediaPlayer.Chapter.Next();
            PWindow.NextChapter();
        }

        private void PreviousChapterButton_Click(object sender, RoutedEventArgs e)
        {
            //this.VLCPlayer.MediaPlayer.Chapter.Previous();
            PWindow.PreviousChapter();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PWindow.Close();
            this.MediaPlayer.Stop();
        }

        private void FullscreenButton_Click(object sender, RoutedEventArgs e)
        {
            PWindow.WindowStyle = WindowStyle.None;
            PWindow.WindowState = WindowState.Maximized;
            PWindow.Topmost = true;
        }

        private int CheckTextBox(TextBox textBox)
        {
            if (textBox == null) return 0;
            if (!int.TryParse(textBox.Text, out int val))
            {
                val = 0;
                textBox.Text = "0";
            }
            return val;
        }

        private void MarginChanged(object sender, TextChangedEventArgs e)
        {
            if (PWindow == null) return;
            PWindow.ChangeMargin(new Thickness(CheckTextBox(LeftMargin), CheckTextBox(TopMargin), CheckTextBox(RightMargin), CheckTextBox(BottomMargin)));
        }

        private void JumpToChapter_Click(object sender, RoutedEventArgs e)
        {
            /*bool playing = false;
            if (this.VLCPlayer.MediaPlayer.IsPlaying)
            {
                playing = true;
                this.VLCPlayer.MediaPlayer.Pause();
                PWindow.Pause();
            }

            if (this.ChapterList.SelectedIndex < 0) return;
            int jumpCount = this.VLCPlayer.MediaPlayer.Chapter.Current - this.ChapterList.SelectedIndex;

            bool forward = jumpCount < 0;
            jumpCount = Math.Abs(jumpCount);

            forward = true;
            this.VLCPlayer.MediaPlayer.Time = 0;
            PWindow.SetTime(0);
            jumpCount = this.ChapterList.SelectedIndex;

            for (int i = 0; i < jumpCount; i++) { if (forward) { this.VLCPlayer.MediaPlayer.Chapter.Next(); PWindow.NextChapter(); } else { this.VLCPlayer.MediaPlayer.Chapter.Previous(); PWindow.PreviousChapter(); } }

            //PWindow.SetTime(this.VLCPlayer.MediaPlayer.Time);

            if (playing)
            {
                this.VLCPlayer.MediaPlayer.Play();
                PWindow.Play();
            }*/
        }

        private void LoadChapter_Click(object sender, RoutedEventArgs e)
        {
            /*this.ChapterList.Items.Clear();
            for (int i = 0; i < this.VLCPlayer.MediaPlayer.Chapter.Count; i++)
            {
                this.ChapterList.Items.Add(i.ToString());
            }*/
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            SynchronisePositions();
        }
    }
}
