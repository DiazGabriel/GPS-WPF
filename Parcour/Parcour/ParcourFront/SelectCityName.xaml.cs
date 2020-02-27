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

namespace ParcourFront
{
    /// <summary>
    /// Logique d'interaction pour SelectCityName.xaml
    /// </summary>
    public partial class SelectCityName : Window
    {
		public bool isAnswerEmpty = false;
		public SelectCityName(string defaultAnswer = "")
		{
			InitializeComponent();
			txtAnswer.Text = defaultAnswer;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtAnswer.SelectAll();
			txtAnswer.Focus();
		}

		public string Answer
		{
			get { return txtAnswer.Text; }
			set { txtAnswer.Text = value; }
		}

		public string Question
		{
			set { lblQuestion.Content = value; }
		}

		public void inputAnswer (object sender, TextChangedEventArgs e)
		{
			if(txtAnswer.Text != "")
			{
				isAnswerEmpty = true;
			} else
			{
				isAnswerEmpty = false;
			}
			EnableSubmitButton();
		}

		public void EnableSubmitButton()
		{
			// Si les deux input sont renseigné, on dégrise le bouton run
			if (isAnswerEmpty == true)
			{
				submitBtn.IsEnabled = true;
			}
			else
			{
				submitBtn.IsEnabled = false;
			}
		}
	}
}
