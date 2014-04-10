
using System.Windows.Controls;
using System.Windows.Media;
namespace NetProfileSwitcher.UIControls
{
    public class ButtonDark : Button
    {
        public ButtonDark()
        {
            this.Background = GradientFactory.PredefinedElegantBlack;
            this.Foreground = Brushes.White;
        }
    }

}
