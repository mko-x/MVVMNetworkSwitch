using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace NetProfileSwitcher.UIControls
{
    public class LabelTextBlock : TextBlock
    {
        public LabelTextBlock()
        {
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //this.Background = GradientFactory.PredefinedElegantBlack;
            this.Background = Brushes.Transparent;
            this.Foreground = Brushes.White;
            //this.Background.Opacity = .3;
        }
    }
}
