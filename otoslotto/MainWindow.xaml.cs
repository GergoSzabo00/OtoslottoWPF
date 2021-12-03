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

namespace otoslotto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, Page> pages = new Dictionary<string, Page>();
        public SetNamePage setNamePage = new SetNamePage();

        public MainWindow()
        {
            InitializeComponent();
            NavigationHolder.Content = setNamePage;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pages.ContainsKey("GamePage"))
            {
                bool isGameInProgress = ((GamePage)pages["GamePage"]).isGameInProgress;
                if (isGameInProgress)
                {
                    MessageBoxResult result = MessageBox.Show("A sorsolás épp folyamatban van, eközben nem léphetsz ki!",
                    "Kilépés", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    if (result == MessageBoxResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
