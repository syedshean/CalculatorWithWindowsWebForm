 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{
    public partial class Form1 : Form
    {
        private bool equalButtonPressed = false;
        public Form1()
        {
            InitializeComponent();
        }  

        private void NumberButton(object sender, EventArgs e)
        {
            //Checks whether resultbutton contains 0 or result from a calculation
            if ((resultButton.Text == "0") || (equalButtonPressed))
            {
                resultButton.Clear();
            }
            Button b = (Button)sender;
            resultButton.Text = resultButton.Text + b.Text;
            equalButtonPressed = false;
        }
        private void DecimalButton(object sender, EventArgs e)
        {
            //Checks whether resultbutton contains result from a calculation
            if (equalButtonPressed)
            {
                resultButton.Text = "0";
            }
            Button b = (Button)sender;
            resultButton.Text = resultButton.Text + b.Text;
            equalButtonPressed = false;
        }

        private void ButtonCL(object sender, EventArgs e)
        {
            resultButton.Text = "0";
        }

        private void ButtonCE(object sender, EventArgs e)
        {
            if ((resultButton.Text == "Invalid double") || (resultButton.Text == "Division by zero") || 
                (resultButton.Text == "∞"))
            {
                resultButton.Text = "0";
            }
            else if (resultButton.Text != "0")
            {
                resultButton.Text = resultButton.Text.Remove(resultButton.Text.Length - 1, 1);
                resultButton.Text = (resultButton.Text.Length == 0) ? "0" : resultButton.Text;
            }
        }

        private void OperatorButton(object sender, EventArgs e)
        {
            if ((resultButton.Text == "Invalid double") || (resultButton.Text == "Division by zero") || 
                (resultButton.Text == "∞"))
            {
                resultButton.Text = "0";
            }
            Button b = (Button)sender;
            resultButton.Text = resultButton.Text + b.Text;
            equalButtonPressed = false;
        }

        private void EqualButton(object sender, EventArgs e)
        {
            Operations calc = new Operations();
            resultButton.Text = calc.CalculateTheInput(resultButton.Text);
            equalButtonPressed = true;
        }

       
    }
}
