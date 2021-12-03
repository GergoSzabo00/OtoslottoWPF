using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for PrizesPage.xaml
    /// </summary>
    public partial class PrizesPage : Page
    {
        string name;
        private List<Prize> temporary = new List<Prize>();
        private ObservableCollection<Prize> prizes;
        public PrizesPage(string name)
        {
            InitializeComponent();
            this.name = name;
            prizes = new ObservableCollection<Prize>();
            GetPrizes();
        }

        public void GetPrizes() 
        {
            temporary.Clear();
            prizes.Clear();
            if (File.Exists("prizes.csv"))
            {
                StreamReader sr = new StreamReader("prizes.csv");

                while (!sr.EndOfStream)
                {
                    string[] values = sr.ReadLine().Split(';');
                    Prize prize = new Prize() 
                    { 
                        Name = values[0],
                        PlayerNumbers = values[1], 
                        WinnerNumbers = values[2],
                        PrizeText = values[3],
                        Hits = values[4]
                    };
                    temporary.Add(prize);
                }
                sr.Close();
            }
            temporary.Reverse();

            foreach (Prize item in temporary)
            {
                prizes.Add(item);
            }

            prizesHolder.ItemsSource = prizes;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            if (!mainWindow.pages.ContainsKey("HomePage"))
            {
                HomePage homePage = new HomePage(name);
                mainWindow.pages.Add("HomePage", homePage);
                mainWindow.NavigationHolder.Content = homePage;
            }
            else
            {
                mainWindow.NavigationHolder.Content = mainWindow.pages["HomePage"];
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        class Prize 
        {
            public string Name { get; set; }
            public string PlayerNumbers { get; set; }
            public string WinnerNumbers { get; set; }
            public string PrizeText { get; set; }
            public string Hits { get; set; }
        }

        private void prizesHolder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gridView = listView.View as GridView;

            double width = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;

            double col1 = 0.35;
            double col2 = 0.15;
            double col3 = 0.15;
            double col4 = 0.18;
            double col5 = 0.15;

            gridView.Columns[0].Width = width * col1;
            gridView.Columns[1].Width = width * col2;
            gridView.Columns[2].Width = width * col3;
            gridView.Columns[3].Width = width * col4;
            gridView.Columns[4].Width = width * col5;
        }
    }
}
