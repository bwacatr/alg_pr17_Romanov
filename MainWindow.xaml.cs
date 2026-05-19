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
using System.IO;

namespace PR17_romanov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] txtArray;
        char[] separators = { ' ', '.', ',', ':' };

        
        public MainWindow()
        {
            InitializeComponent();
            
            

            
        }

        private void Task1_Click(object sender, RoutedEventArgs e)
        {
            if (Task2_Panel.Visibility == Visibility.Visible)
            {
                Task2_Panel.Visibility = Visibility.Hidden;
            }
            Task1_Panel.Visibility = Visibility.Visible;
            string fileName = "File.txt";
            string txt = File.ReadAllText(fileName);
            
            txtArray = txt.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            txtFromFile.Text = txt;
        }

        private void Task2_Click(object sender, RoutedEventArgs e)
        {
            if (Task1_Panel.Visibility == Visibility.Visible)
            {
                Task1_Panel.Visibility = Visibility.Hidden;
            }
            Task2_Panel.Visibility = Visibility.Visible;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Кнопка Поиск
        {
            if (!string.IsNullOrWhiteSpace(txtFromFile.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtInputWord.Text))
                {
                    string inputWord = txtInputWord.Text;
                    var result = from word in txtArray where word.ToLower() == inputWord.ToLower() select word;
                    txtResult.Text = $"Кол-во слов: {result.Count()}";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Кнопка Начать
        {
            
            
            
            bool foundSlash = false;

            if (!string.IsNullOrWhiteSpace(txtInputWord2.Text))
            {
                string[] lineArray = txtInputWord2.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var Digits = from symbol in txtInputWord2.Text where char.IsDigit(symbol) select symbol;

                var beforeSlash = txtInputWord2.Text.TakeWhile(x => x != '/');

                var afterSlash = txtInputWord2.Text.SkipWhile(x => x != '/').Skip(1);

                string lineAfterSlash = "";

                string lineBeforeSlash = "";

                foreach (char item in afterSlash)
                {
                    if (char.IsLower(item))
                    {
                        lineAfterSlash += char.ToUpper(item);
                    }
                    else
                    {
                        lineAfterSlash += char.ToLower(item);
                    }
                }

                foreach (char item in beforeSlash)
                {
                    lineBeforeSlash += item;
                }

                string[] afterSlashArray = lineAfterSlash.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] beforeSlashArray = lineBeforeSlash.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string[] fullArray = beforeSlashArray.Concat(afterSlashArray).ToArray();

                txtResult2.Text = $"Кол-во цифр: {Digits.Count()}";
                txtResult3.Text = $"До '/': {string.Join(" ",beforeSlashArray)}";
                txtResult4.Text = $"После '/': {string.Join(" ",afterSlashArray)}";

                using (StreamWriter sw = File.CreateText("Task2Results.txt"))
                {
                    foreach (var item in fullArray)
                    {
                        sw.WriteLine(item);
                    }
                }

            }

            
        }
    }
}
