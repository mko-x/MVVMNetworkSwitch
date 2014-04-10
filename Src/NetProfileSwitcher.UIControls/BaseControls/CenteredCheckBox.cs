using System.Windows.Controls;
using System.Windows.Media;

namespace NetProfileSwitcher.UIControls
{
    public class CenteredCheckBox : CheckBox
    {
        public CenteredCheckBox()
        {
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.Background = GradientFactory.PredefinedElegantBlack;
            this.Foreground = Brushes.White;
        }
    }
}
