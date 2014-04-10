
using System.Windows.Controls;
using System.Windows.Media;
namespace NetProfileSwitcher.UIControls.BaseControls
{
    public class Scroll : ScrollViewer
    {
        public Scroll()
        {
            this.Background = GradientFactory.PredefinedElegantBlack;
            this.Foreground = Brushes.White;
        }
    }
}
