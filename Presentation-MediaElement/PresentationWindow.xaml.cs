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

namespace Presentation_MediaElement
{
    /// <summary>
    /// Interaction logic for PresentationWindow.xaml
    /// </summary>
    public partial class PresentationWindow : Window
    {
        private Uri _presentation;
        public Uri Presentation { get { return _presentation; } set { _presentation = value; this.Presenter.Source = _presentation; } }

        public PresentationWindow()
        {
            InitializeComponent();
        }

        public void Play()
        {
            this.Presenter.Play();
        }

        public void Pause()
        {
            this.Presenter.Pause();
        }

        public void Stop()
        {
            this.Presenter.Stop();
        }

        public void SetTime(TimeSpan time)
        {
            this.Presenter.Position = time;
        }

        public TimeSpan GetTime()
        {
            return this.Presenter.Position;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Presenter.Stop();
        }

        internal void ChangeMargin(Thickness margin)
        {
            this.Presenter.Margin = margin;
        }
    }
}
