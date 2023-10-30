using System;
using System.Collections.Generic;
using System.IO;
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
using Viktorina26042022.Database_Clases;

namespace Viktorina26042022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        int currentQuestion = 0;
        int currentAnswer = 0;
        int[] numArr = new int[4];
        short? countofAnsvers;
        string[] ansvers;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Text = null;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Password = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StaticQuestion.viktorina = new QuestionsDB();
            numArr[0] = 0;
            numArr[1] = 1;
            numArr[2] = 2;
            numArr[3] = 3;
            Update();
        }
        private void Update()
        {
            countofAnsvers = StaticQuestion.viktorina.getCountOfAnsvers(currentQuestion);
            lblquestion.Content = StaticQuestion.viktorina.getQuestion(currentQuestion);
            if (countofAnsvers == null || countofAnsvers == 1)
            {
                ansvers = new string[4];
                Shuffle(numArr);
                currentAnswer = numArr[0];
                ansvers[numArr[0]] = StaticQuestion.viktorina.getAnswerTrue(currentQuestion);
                ansvers[numArr[1]] = StaticQuestion.viktorina.getAnswer1(currentQuestion);
                ansvers[numArr[2]] = StaticQuestion.viktorina.getAnswer2(currentQuestion);
                ansvers[numArr[3]] = StaticQuestion.viktorina.getAnswer3(currentQuestion);
                RadioButtonQuest(ansvers);
            }
            else
            {
                ansvers = new string[4];
                Shuffle(numArr);
                ansvers[numArr[0]] = StaticQuestion.viktorina.getAnswerTrue(currentQuestion);
                ansvers[numArr[1]] = StaticQuestion.viktorina.getAnswer1(currentQuestion);
                ansvers[numArr[2]] = StaticQuestion.viktorina.getAnswer2(currentQuestion);
                ansvers[numArr[3]] = StaticQuestion.viktorina.getAnswer3(currentQuestion);
                CheckBoxQuest(ansvers, countofAnsvers);
            }
            if(StaticQuestion.viktorina.getImg(currentQuestion) != null) img.Source = LoadImage(StaticQuestion.viktorina.getImg(currentQuestion));
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void CheckBoxQuest(string[] Ansvers, short? countofAnsvers)
        {
            rb1.IsEnabled = false;
            rb1.Visibility = Visibility.Hidden;

            rb2.IsEnabled = false;
            rb2.Visibility = Visibility.Hidden;

            rb3.IsEnabled = false;
            rb3.Visibility = Visibility.Hidden;

            rb4.IsEnabled = false;
            rb4.Visibility = Visibility.Hidden;

            cb1.IsEnabled = true;
            cb1.Visibility = Visibility.Visible;
            cb1.Content = Ansvers[0];

            cb2.IsEnabled = true;
            cb2.Visibility = Visibility.Visible;
            cb2.Content = Ansvers[1];

            cb3.IsEnabled = true;
            cb3.Visibility = Visibility.Visible;
            cb3.Content = Ansvers[2];

            cb4.IsEnabled = true;
            cb4.Visibility = Visibility.Visible;
            cb4.Content = Ansvers[3];
        }

        private void RadioButtonQuest(string[] Ansvers)
        {
            cb1.IsEnabled = false;
            cb1.Visibility = Visibility.Hidden;

            cb2.IsEnabled = false;
            cb2.Visibility = Visibility.Hidden;

            cb3.IsEnabled = false;
            cb3.Visibility = Visibility.Hidden;

            cb4.IsEnabled = false;
            cb4.Visibility = Visibility.Hidden;

            rb1.IsEnabled = true;
            rb1.Visibility = Visibility.Visible;
            rb1.Content = Ansvers[0];

            rb2.IsEnabled = true;
            rb2.Visibility = Visibility.Visible;
            rb2.Content = Ansvers[1];

            rb3.IsEnabled = true;
            rb3.Visibility = Visibility.Visible;
            rb3.Content = Ansvers[2];

            rb4.IsEnabled = true;
            rb4.Visibility = Visibility.Visible;
            rb4.Content = Ansvers[3];
        }
        public static void Shuffle(int[] arr)
        {
            Random rand = new Random();

            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                int tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (rb1.IsEnabled == true)
            {
                if (rb1.IsChecked == true) RbAnsvertry(0);
                else if (rb2.IsChecked == true) RbAnsvertry(1);
                else if (rb3.IsChecked == true) RbAnsvertry(2);
                else if (rb4.IsChecked == true) RbAnsvertry(3);
                else MessageBox.Show("Выберете ответ");
            }
            else if (cb1.IsEnabled == true)
            {
                int[] indexOfAnsver = { -1, -1, -1, -1 };
                if (cb1.IsChecked == true) indexOfAnsver[0] = 0;
                if (cb2.IsChecked == true) indexOfAnsver[1] = 1;
                if (cb3.IsChecked == true) indexOfAnsver[2] = 2;
                if (cb4.IsChecked == true) indexOfAnsver[3] = 3;
                CBAnsvertry(indexOfAnsver);
            }
            else MessageBox.Show("Eror Button_Click");
        }
        private void RbAnsvertry(int indexOfAnsver)
        {
            if (indexOfAnsver == currentAnswer)
            {
                NextQuest();
            }
            else MessageBox.Show("Ответ Не врный");
        }
        private void CBAnsvertry(int[] indexOfAnsvers)
        {
            int countOFTRYes = 0;
            int countOfLoses = 0;
            for (int i = 0; i < 4; i++)
            {
                if (indexOfAnsvers[i] != -1)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (numArr[j] == indexOfAnsvers[i])
                        {
                            for (int n = 0; n < countofAnsvers; n++)
                            {
                                if (j == n)
                                {
                                    countOFTRYes++;
                                    j = 4;
                                }
                            }
                        }
                    }
                }
                else countOfLoses++;

            }
            if (countOfLoses != 4 - countofAnsvers)
            {
                MessageBox.Show("Ответ Не врный");
                return;
            }
            if (countOFTRYes == countofAnsvers)
            {
                NextQuest();
            }
            else MessageBox.Show("Ответ Не врный");
        }
        private void NextQuest()
        {
            if (StaticQuestion.viktorina.getCountOfQuestions() != currentQuestion + 1)
            {
                currentQuestion++;
                Update();
            }
            else
            {
                MessageBox.Show("You Win!");
                Close();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            using (var sr = new StreamReader("log.txt"))
            {
                if (login.Text == sr.ReadLine() && password.Password == sr.ReadLine())
                {
                    btnEditQuestion.IsEnabled = true;
                    btnChangeLogPass.IsEnabled = true;
                    btnLogin.IsEnabled = false;
                    lblfor.Content = "Welcome";
                }
                else
                {
                    MessageBox.Show("Eror Login of Password");
                }
            }
        }

        private void btnChangeLogPass_Click(object sender, RoutedEventArgs e)
        {
            lblfor.Content = "Enter new";
            btnConfirm.IsEnabled = true;
            btnConfirm.Visibility = Visibility.Visible;
            btnChangeLogPass.IsEnabled = false;
            btnEditQuestion.IsEnabled = false;
            
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            using (var sr = new StreamWriter("log.txt"))
            {
                lblfor.Content = "Welcome";
                sr.WriteLine(login.Text);
                sr.WriteLine(password.Password);
                btnConfirm.IsEnabled = false;
                btnConfirm.Visibility = Visibility.Hidden;
                btnChangeLogPass.IsEnabled = true;
                btnEditQuestion.IsEnabled = true;
            }
        }

        private void btnEditQuestion_Click(object sender, RoutedEventArgs e)
        {
            var window = new ChangeWindow(currentQuestion);
            window.ShowDialog();
            Update();
        }
    }
}
