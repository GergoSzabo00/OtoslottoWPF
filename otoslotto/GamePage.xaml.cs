using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            if (!mainWindow.pages.ContainsKey("HomePage"))
            {
                HomePage homePage = new HomePage();
                mainWindow.pages.Add("HomePage", homePage);
                mainWindow.NavigationHolder.Content = homePage;
            }
            else
            {
                mainWindow.NavigationHolder.Content = mainWindow.pages["HomePage"];
            }
        }
    }
}
