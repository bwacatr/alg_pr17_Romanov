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

        private void Task1_Click(object sender, RoutedEventArgs e) // кнопка задание 1
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

        private void Task2_Click(object sender, RoutedEventArgs e)  // кнопка задание 2
        {
            if (Task1_Panel.Visibility == Visibility.Visible)
            {
                Task1_Panel.Visibility = Visibility.Hidden;
            }
            Task2_Panel.Visibility = Visibility.Visible;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)  // кнопка выход
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
                    var result = from word in txtArray where word.ToLower() == inputWord.ToLower() select word; //сравнивает введенное слово со словом из массива и возвращает совпадение
                    txtResult.Text = $"Кол-во слов: {result.Count()}";
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Кнопка Начать
        {
            if (!string.IsNullOrWhiteSpace(txtInputWord2.Text))
            {
                string[] lineArray = txtInputWord2.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var Digits = from symbol in txtInputWord2.Text where char.IsDigit(symbol) select symbol; //возвращает цифры из txtInputWord2.Text

                var beforeSlash = txtInputWord2.Text.TakeWhile(x => x != '/'); //Возвращает символы до /

                var afterSlash = txtInputWord2.Text.SkipWhile(x => x != '/').Skip(1); //Возвращает символы после /

                string lineAfterSlash = "";

                string lineBeforeSlash = "";

                foreach (char item in afterSlash)       //Собирает строку из символов после /
                {
                    if (char.IsLower(item))
                    {
                        lineAfterSlash += char.ToUpper(item);
                    }
                    else                                        //Изменяет регистр символа
                    {
                        lineAfterSlash += char.ToLower(item);
                    }
                }

                foreach (char item in beforeSlash) //Собирает строку из символов до /
                {
                    lineBeforeSlash += item;
                }

                string[] afterSlashArray = lineAfterSlash.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string[] beforeSlashArray = lineBeforeSlash.Split(separators, StringSplitOptions.RemoveEmptyEntries); // разделяет слова, используя массив символов separators и ложит их в массив

                string[] fullArray = beforeSlashArray.Concat(afterSlashArray).ToArray(); // соединяет оба массива

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
