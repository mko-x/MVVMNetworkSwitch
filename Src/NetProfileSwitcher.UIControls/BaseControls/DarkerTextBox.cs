using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using NetProfileSwitcher.UIControls;

namespace NetProfileSwitcher.UIControls
{
    public class DarkerTextBox : TextBox
    {
        public DarkerTextBox()
        {
            this.Background = GradientFactory.PredefinedElegantBlack;
            this.Foreground = Brushes.White;
        }
    }
}
