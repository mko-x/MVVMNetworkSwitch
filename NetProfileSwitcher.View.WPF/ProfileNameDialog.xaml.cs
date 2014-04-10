using System.Windows;
using NetProfileSwitcher.Interfaces.Tasks;
using NetProfileSwitcher.UIControls;

namespace NetProfileSwitcher.View.WPF
{
    /// <summary>
    /// Interaction logic for ProfileNameDialog.xaml
    /// </summary>
    public partial class ProfileNameDialog : Window
    {
        private IValueUpdateHandler<string> _newNameHandler;

        public ProfileNameDialog()
        {
            this.init();
        }

        public ProfileNameDialog(IValueUpdateHandler<string> handler)
        {
            this.init();
            _newNameHandler = handler;
        }

        #region - private area
        private void init()
        {
            InitializeComponent();

            this.Background = GradientFactory.PredefinedElegantBlack;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.handleNewProfileName();
        }

        private void tbProfileName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.handleNewProfileName();
            }
        }

        private void handleNewProfileName()
        {
            if (_newNameHandler != null)
            {
                string newName = tbProfileName.Text;
                _newNameHandler.update(newName);
                this.Close();
            }
        }
        #endregion - private area

        
    }
}
