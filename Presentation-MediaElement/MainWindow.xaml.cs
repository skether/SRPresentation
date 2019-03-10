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
using System.Xml.Linq;

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

            //Presentation = new Uri(@"E:\TestVideos\1 Minute Test.mkv");
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

        private int GetCurrentChapter()
        {
            int c = 0;
            int i = 0;
            while (i < ChapterList.Items.Count && (ChapterList.Items[i] as Chapter).time <= this.MediaPlayer.Position)
            {
                c = i;
                i++;
            }
            return c;
        }

        private void NextChapterButton_Click(object sender, RoutedEventArgs e)
        {
            int c = GetCurrentChapter() + 1;
            if ( c< ChapterList.Items.Count)
            {
                Chapter chapter = ChapterList.Items[c] as Chapter;
                this.MediaPlayer.Position = chapter.time;
                PWindow.SetTime(chapter.time);
            }
        }

        private void PreviousChapterButton_Click(object sender, RoutedEventArgs e)
        {;
            int c = GetCurrentChapter() + -1;
            if (c >= 0)
            {
                Chapter chapter = ChapterList.Items[c] as Chapter;
                this.MediaPlayer.Position = chapter.time;
                PWindow.SetTime(chapter.time);
            }
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
            bool playing = false;
            if (this.MediaPlayer.GetMediaState() == MediaState.Play)
            {
                playing = true;
                this.MediaPlayer.Pause();
                PWindow.Pause();
            }

            if(this.ChapterList.SelectedItem != null)
            {
                this.MediaPlayer.Position = (this.ChapterList.SelectedItem as Chapter).time;
                PWindow.SetTime((this.ChapterList.SelectedItem as Chapter).time);
            }

            if (playing)
            {
                this.MediaPlayer.Play();
                PWindow.Play();
            }
        }

        private void LoadChapter_Click(object sender, RoutedEventArgs e)
        {
            XElement chaptersFromFile = XElement.Load(System.IO.Path.ChangeExtension(Presentation.ToString(), "xml"));
            XElement editionEntry = chaptersFromFile.FirstNode as XElement;
            XElement chapter = editionEntry.FirstNode.NextNode as XElement;

            ChapterList.Items.Clear();

            do
            {
                ChapterList.Items.Add(new Chapter(chapter));
            } while ((chapter = chapter.NextNode as XElement) != null);
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            SynchronisePositions();
        }
    }
}
