using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
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
        System.Timers.Timer timer1 = new System.Timers.Timer();
        System.Timers.Timer timer2 = new System.Timers.Timer();
        System.Timers.Timer timer3 = new System.Timers.Timer();
        System.Timers.Timer timer4 = new System.Timers.Timer();
        System.Timers.Timer timer5 = new System.Timers.Timer();

        int times = 0;

        List<int> playerNumbers = new List<int>();
        List<int> randomNumbers = new List<int>();

        string randomNumbersString;
        int hits = 0;
        string prize = "";

        

        bool isFirstNumberValid;
        bool isSecondNumberValid;
        bool isThirdNumberValid;
        bool isFourthNumberValid;
        bool isFifthNumberValid;

        bool isGameInProgress;

        SolidColorBrush redBrush = new SolidColorBrush(Color.FromArgb(255, 255, 20, 147));
        SolidColorBrush normalBrush = new SolidColorBrush(Color.FromArgb(255,160, 160, 160));

        public GamePage()
        {
            InitializeComponent();
            timer1.Interval = 200;
            timer2.Interval = 200;
            timer3.Interval = 200;
            timer4.Interval = 200;
            timer5.Interval = 200;

            timer1.Elapsed += Timer1_Elapsed;
            timer2.Elapsed += Timer2_Elapsed;
            timer3.Elapsed += Timer3_Elapsed;
            timer4.Elapsed += Timer4_Elapsed;
            timer5.Elapsed += Timer5_Elapsed;
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (times == 10)
            {
                timer1.Enabled = false;
                times = 0;
                GenerateTrueRandom(firstBallNumber);
                timer2.Enabled = true;
            }
            else 
            {
                GenerateRandomNumber(firstBallNumber);
                times++;
            }
        }

        private void Timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (times == 10)
            {
                timer2.Enabled = false;
                times = 0;
                GenerateTrueRandom(secondBallNumber);
                timer3.Enabled = true;
            }
            else 
            {
                GenerateRandomNumber(secondBallNumber);
                times++;
            }
        }


        private void Timer3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (times == 10)
            {
                timer3.Enabled = false;
                times = 0;
                GenerateTrueRandom(thirdBallNumber);
                timer4.Enabled = true;
            }
            else 
            {
                GenerateRandomNumber(thirdBallNumber);
                times++;
            }
        }

        private void Timer4_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (times == 10)
            {
                timer4.Enabled = false;
                times = 0;
                GenerateTrueRandom(fourthBallNumber);
                timer5.Enabled = true;
            }
            else 
            {
                GenerateRandomNumber(fourthBallNumber);
                times++;
            }
        }

        private void Timer5_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (times == 10)
            {
                timer5.Enabled = false;
                times = 0;
                GenerateTrueRandom(fifthBallNumber);
                CheckForHits();
            }
            else 
            {
                GenerateRandomNumber(fifthBallNumber);
                times++;
            }
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

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = (e.Key == Key.Space);
        }

        private bool IsValid(string str) 
        {
            int number;
            return int.TryParse(str, out number) && number > 0 && number < 91;
        }

        private void firstPlayerNumberField_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text) ||
                textBox.Text == secondPlayerNumberField.Text ||
                textBox.Text == thirdPlayerNumberField.Text ||
                textBox.Text == fourthPlayerNumberField.Text ||
                textBox.Text == fifthPlayerNumberField.Text)
            {
                isFirstNumberValid = false;
                textBox.BorderBrush = redBrush;
            }
            else 
            {
                isFirstNumberValid = true;
                textBox.BorderBrush = normalBrush;
            }

            CheckIfEverythingValid();

        }

        private void secondPlayerNumberField_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text) ||
                textBox.Text == firstPlayerNumberField.Text  ||
                textBox.Text == thirdPlayerNumberField.Text  ||
                textBox.Text == fourthPlayerNumberField.Text ||
                textBox.Text == fifthPlayerNumberField.Text)
            {
                isSecondNumberValid = false;
                textBox.BorderBrush = redBrush;
            }
            else 
            {
                isSecondNumberValid = true;
                textBox.BorderBrush = normalBrush;
            }

            CheckIfEverythingValid();

        }

        private void thirdPlayerNumberField_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text) ||
                textBox.Text == firstPlayerNumberField.Text  ||
                textBox.Text == secondPlayerNumberField.Text ||
                textBox.Text == fourthPlayerNumberField.Text ||
                textBox.Text == fifthPlayerNumberField.Text)
            {
                isThirdNumberValid = false;
                textBox.BorderBrush = redBrush;
            }
            else 
            {
                isThirdNumberValid = true;
                textBox.BorderBrush = normalBrush;
            }

            CheckIfEverythingValid();

        }

        private void fourthPlayerNumberField_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text) ||
                textBox.Text == firstPlayerNumberField.Text  ||
                textBox.Text == secondPlayerNumberField.Text ||
                textBox.Text == thirdPlayerNumberField.Text  ||
                textBox.Text == fifthPlayerNumberField.Text)
            {
                isFourthNumberValid = false;
                textBox.BorderBrush = redBrush;
            }
            else 
            {
                isFourthNumberValid = true;
                textBox.BorderBrush = normalBrush;
            }

            CheckIfEverythingValid();

        }

        private void fifthPlayerNumberField_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (string.IsNullOrEmpty(textBox.Text) ||
                textBox.Text == firstPlayerNumberField.Text  ||
                textBox.Text == secondPlayerNumberField.Text ||
                textBox.Text == thirdPlayerNumberField.Text  ||
                textBox.Text == fourthPlayerNumberField.Text)
            {
                isFifthNumberValid = false;
                textBox.BorderBrush = redBrush;
            }
            else 
            {
                isFifthNumberValid = true;
                textBox.BorderBrush = normalBrush;
            }

            CheckIfEverythingValid();

        }


        private void CheckIfEverythingValid() 
        {
            if (isFirstNumberValid && isSecondNumberValid && isThirdNumberValid && isFourthNumberValid && isFifthNumberValid)
            {
                StartGameButton.IsEnabled = true;
            }
            else 
            {
                StartGameButton.IsEnabled = false;
            }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void StartGame() 
        {
            ToggleInputs(false);
            isGameInProgress = true;
            ClearWinnerNumbersText();
            ClearHitsAndPrizeText();
            ClearBallNumbers();
            ClearRandomNumbersList();
            SetPlayerNumbers();
            SetPlayerNumbersText();
            StartNumberGeneration();
        }

        private void ClearWinnerNumbersText() 
        {
            winnerNumbersText.Text = "";
        }

        private void ClearHitsAndPrizeText()
        {
            hits = 0;
            numberOfHitsText.Text = "";
            prizeText.Text = "";
        }

        private void StartNumberGeneration() 
        {
            timer1.Enabled = true;
        }

        private void CheckForHits() 
        {
            foreach (int playerNumber in playerNumbers)
            {
                if (randomNumbers.Contains(playerNumber))
                {
                    hits++;
                }   
            }

            SetHitsAndPrizeText();

        }

        private void SetHitsAndPrizeText() 
        {
            switch (hits)
            {
                case 0:
                    prize = "0 Ft";
                    break;
                case 1:
                    prize = "0 Ft"; 
                    break;
                case 2:
                    prize = "2.000 Ft";
                    break;
                case 3:
                    prize = "100.000 Ft";
                    break;
                case 4:
                    prize = "5.000.000 Ft";
                    break;
                case 5:
                    prize = "1.250.000.000 Ft";
                    break;
                default:
                    break;
            }


            Dispatcher.Invoke(new Action(() => {
                numberOfHitsText.Text = hits.ToString() + " találat!";
                prizeText.Text = prize;

                EndGame();

            }));

        }

        private void EndGame() 
        {
            isGameInProgress = false;
            ToggleInputs(true);
        }

        private void ClearRandomNumbersList() 
        {
            randomNumbers.Clear();
        }

        private void ClearBallNumbers() 
        {
            firstBallNumber.Content = "";
            secondBallNumber.Content = "";
            thirdBallNumber.Content = "";
            fourthBallNumber.Content = "";
            fifthBallNumber.Content = "";
        }

        private void SetPlayerNumbers() 
        {
            playerNumbers.Clear();
            playerNumbers.Add(int.Parse(firstPlayerNumberField.Text));
            playerNumbers.Add(int.Parse(secondPlayerNumberField.Text));
            playerNumbers.Add(int.Parse(thirdPlayerNumberField.Text));
            playerNumbers.Add(int.Parse(fourthPlayerNumberField.Text));
            playerNumbers.Add(int.Parse(fifthPlayerNumberField.Text));

            playerNumbers.Sort();

        }

        private void SetPlayerNumbersText() 
        {
            string playerNumbersString = String.Join(", ", playerNumbers);
            playerNumbersText.Text = playerNumbersString;
        }

        private void GenerateTrueRandom(Label label)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 91);

            while (randomNumbers.Contains(number))
            {
                number = rnd.Next(1, 91);
            }

            randomNumbers.Add(number);
            randomNumbers.Sort();

            randomNumbersString = "";

            for (int i = 0; i < randomNumbers.Count; i++)
            {
                if (i == randomNumbers.Count - 1)
                {
                    randomNumbersString += randomNumbers[i];
                }
                else 
                {
                    randomNumbersString += randomNumbers[i] + ", ";
                }
            }

            Dispatcher.Invoke(new Action(() => {
                label.Content = number.ToString();
                winnerNumbersText.Text = randomNumbersString;
            }));
        }

        private void GenerateRandomNumber(Label label) 
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 91);

            Dispatcher.Invoke(new Action(() => { label.Content = number.ToString(); }));
        }

        private void ToggleInputs(bool isEnabled) 
        {
            firstPlayerNumberField.IsEnabled = isEnabled;
            secondPlayerNumberField.IsEnabled = isEnabled;
            thirdPlayerNumberField.IsEnabled = isEnabled;
            fourthPlayerNumberField.IsEnabled = isEnabled;
            fifthPlayerNumberField.IsEnabled = isEnabled;

            StartGameButton.IsEnabled = isEnabled;
            BackButton.IsEnabled = isEnabled;
            ExitButton.IsEnabled = isEnabled;

        }

    }
}
