using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private bool buttonEnabling = true;
        public MainPage()
        {
            InitializeComponent();
            resultText.Text = "0";
            resultText.FontSize = 40;
        }
        void LabelSize()
        {
            if (resultText.Text.Length >= 15) resultText.FontSize = 30;
            else resultText.FontSize = 40;
        }

        void SelectedNumber(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var number = button.Text;
            var result = resultText.Text;
            if (result.Length == 1 && int.Parse(result) == 0 || Regex.IsMatch(result, @"[a-zA-Z]"))
                resultText.Text = "";
            LabelSize();
            resultText.Text += number;
            buttonEnabling = true;
        }

        void SelectedOperator(object sender, EventArgs e)
        {
            if (buttonEnabling)
            {
                if (Regex.IsMatch(resultText.Text, @"[a-zA-Z]")) resultText.Text = "0";
                var button = (Button)sender;
                var operation = button.Text;
                LabelSize();
                resultText.Text += operation;
            }
            buttonEnabling = false;
        }

        void OperationResult(object sender, EventArgs e)
        {
            var result = resultText.Text.Replace("×", "*").Replace("÷", "/");
            if (char.IsDigit(result.Last()))
            {
                try
                {
                    var answer = new DataTable().Compute(result, "");
                    resultText.Text = answer.ToString();
                    LabelSize();
                }
                catch (Exception) { resultText.Text = "Error"; }
            }
        }

        void DeleteResult(object sender, EventArgs e)
        { 
            resultText.Text = "0";
            LabelSize();
        }

        void ClearCharacter(object sender, EventArgs e)
        {
            var result = resultText.Text;
            if (result.Length >= 1 && result != "0")
                resultText.Text = result.Remove(result.Length - 1);
            if (resultText.Text == "") resultText.Text = "0";
            LabelSize();
        }
    }
}
