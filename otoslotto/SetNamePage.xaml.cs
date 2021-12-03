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
    /// Interaction logic for SetNamePage.xaml
    /// </summary>
    public partial class SetNamePage : Page
    {
        private string name;
        public string Username
        {
            get { return name; }
            set { name = value; }
        }
        public SetNamePage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SetNameButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            Username = Username.Trim();
            mainWindow.NavigationHolder.Content = new HomePage(name);
        }
    }
}
