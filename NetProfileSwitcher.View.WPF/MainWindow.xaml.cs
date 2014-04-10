using System.Windows;
using NetProfileSwitcher.ViewModel;

using NetProfileSwitcher.UIControls;
using NetProfileSwitcher.Interfaces.Tasks;
using NetProfileSwitcher.Util.DelegateCommands;

namespace NetProfileSwitcher.View.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DelegateCloseCommand closeCommand = new DelegateCloseCommand(OnClose);

            MainViewModel viewModel = new MainViewModel();
            viewModel.Inject(closeCommand);
            DataContext = viewModel;

            this.Background = GradientFactory.PredefinedElegantBlack;
        }

        #region - handler
        /// <summary>
        /// Handles lost focus in profile combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Update(object sender, RoutedEventArgs e)
        {
            updateProfileName();
        }

        /// <summary>
        /// Waits for an Key.Enter in profile combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                updateProfileName();
            }
        }

        /// <summary>
        /// Create a new profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_New_Click(object sender, RoutedEventArgs e)
        {
            IValueUpdateHandler<string> nameUpdateHandler = ((IValueUpdateHandler<string>)DataContext);

            ProfileNameDialog dialog = new ProfileNameDialog(nameUpdateHandler);
            dialog.Show();
        }
        #endregion - handler

        #region - internal view model update methods
        private void updateProfileName()
        {
            string currenName = this.cbProfiles.Text;
            ((MainViewModel)DataContext).updateProfileName(currenName);
        }

        private void OnClose(int exitCode)
        {
            this.Close();
        }
        #endregion - internal view model update methods

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private MainViewModel ModelData()
        {
            return ((MainViewModel)DataContext);
        }
    }
}
