
using System.Windows.Controls;
using System.Windows.Media;
namespace NetProfileSwitcher.UIControls
{
    public class DarkComboBox : ComboBox
    {

        public DarkComboBox()
        {
            this.Background = GradientFactory.PredefinedElegantBlack;
            this.Foreground = Brushes.White;
        }

    }
}
