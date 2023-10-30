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
using System.Windows.Shapes;
using Viktorina26042022.Database_Clases;

namespace Viktorina26042022
{
    /// <summary>
    /// Interaction logic for ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        int currentQuestion;
        public ChangeWindow(int currentQuestion)
        {
            InitializeComponent();
            this.currentQuestion = currentQuestion;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbQuestion.Text = StaticQuestion.viktorina.getQuestion(currentQuestion);
            tb1.Text = StaticQuestion.viktorina.getAnswerTrue(currentQuestion);
            tb2.Text = StaticQuestion.viktorina.getAnswer1(currentQuestion);
            tb3.Text = StaticQuestion.viktorina.getAnswer2(currentQuestion);
            tb4.Text = StaticQuestion.viktorina.getAnswer3(currentQuestion);
            CountOfAnsvers(StaticQuestion.viktorina.getCountOfAnsvers(currentQuestion));
        }
        private void CountOfAnsvers(short? countOfAnsvers)
        {
            double anvers = 1;
            if (countOfAnsvers != null) anvers = Double.Parse(countOfAnsvers.ToString());
            re1.Fill = Brushes.LightGray;
            re2.Fill = Brushes.LightGray;
            re3.Fill = Brushes.LightGray;
            re4.Fill = Brushes.LightGray;
            slider.Value = anvers;
            if (anvers == 1) re1.Fill = Brushes.Green;
            else if (anvers == 2)
            {
                re1.Fill = Brushes.Green;
                re2.Fill = Brushes.Green;
            }
            else if (anvers == 3)
            {
                re1.Fill = Brushes.Green;
                re2.Fill = Brushes.Green;
                re3.Fill = Brushes.Green;
            }
            else if (anvers == 4)
            {
                re1.Fill = Brushes.Green;
                re2.Fill = Brushes.Green;
                re3.Fill = Brushes.Green;
                re4.Fill = Brushes.Green;
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (re1.IsLoaded) CountOfAnsvers(short.Parse(slider.Value.ToString()));
            }
            catch
            {
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            StaticQuestion.viktorina.ChangeQuestion(currentQuestion, new QuestionSet()
            {
                QuestionText = tbQuestion.Text,
                AnswerTrue = tb1.Text,
                Answer1 = tb2.Text,
                Answer2 = tb3.Text,
                Answer3 = tb4.Text,
                CountOfAnsvers = short.Parse(slider.Value.ToString()),
            });
            Close();
        }
    }
}
