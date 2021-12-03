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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        string name;
        public HomePage(string name)
        {
            InitializeComponent();
            this.name = name;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GotoGamePageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            if (!mainWindow.pages.ContainsKey("GamePage"))
            {
                GamePage gamePage = new GamePage(name);
                mainWindow.pages.Add("GamePage", gamePage);
                mainWindow.NavigationHolder.Content = gamePage;
            }
            else 
            {
                mainWindow.NavigationHolder.Content = mainWindow.pages["GamePage"];
            }
            
        }

        private void GotoPrizesPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            if (!mainWindow.pages.ContainsKey("PrizesPage"))
            {
                PrizesPage prizesPage = new PrizesPage();
                mainWindow.pages.Add("PrizesPage", prizesPage);
                mainWindow.NavigationHolder.Content = prizesPage;
            }
            else
            {
                mainWindow.NavigationHolder.Content = mainWindow.pages["PrizesPage"];
            }
        }

    }
}
