using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_Calculator
{
    public partial class Calculator : Form
    {
        #region Variables
        float? num1 = null;
        float? num2 = null;
        char? operand1 = null;
        char? operand2 = null;
        float? total = null;

        float? currentnumber = null;
        float? power = null;
        string historyStorage = "";
        #endregion
        public Calculator()
        {
            InitializeComponent();
        }
        #region Initialize Buttons
        private void buttonOne_Click(object sender, EventArgs e)
        {
            updateOutput("1");
        }

        private void buttonTwo_Click(object sender, EventArgs e)
        {
            updateOutput("2");
        }

        private void buttonThree_Click(object sender, EventArgs e)
        {
            updateOutput("3");
        }

        private void buttonFour_Click(object sender, EventArgs e)
        {
            updateOutput("4");
        }

        private void buttonFive_Click(object sender, EventArgs e)
        {
            updateOutput("5");
        }

        private void buttonSix_Click(object sender, EventArgs e)
        {
            updateOutput("6");
        }

        private void buttonSeven_Click(object sender, EventArgs e)
        {
            updateOutput("7");
        }

        private void buttonEight_Click(object sender, EventArgs e)
        {
            updateOutput("8");
        }

        private void buttonNine_Click(object sender, EventArgs e)
        {
            updateOutput("9");
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            updateOutput("0");
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            updateOperation("+");
        }

        private void buttonSubt_Click(object sender, EventArgs e)
        {
            updateOperation("-");
        }

        private void buttonMult_Click(object sender, EventArgs e)
        {
            updateOperation("*");
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            updateOperation("/");
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            updateOperation("=");
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            SpecialOperation("N");
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            SpecialOperation("D");
        }

        private void buttonClearEntry_Click(object sender, EventArgs e)
        {
            clearOperation("CE");
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            clearOperation("Clear");
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            clearOperation("Back");
        }

        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            SpecialOperation("R");
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            SpecialOperation("S");
        }

        private void buttonExp_Click(object sender, EventArgs e)
        {
            updateOperation("^");
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            SpecialOperation("I");
        }
        #endregion

        #region Update Display
        //Numbers
        private void updateOutput(string thing)
        {
            if ((operand1.ToString() == "^" || operand2.ToString() == "^") && power == null)
            {
                power = float.Parse(thing);
                textDisplay.Text = power.ToString();
                SpecialOperation("^");
            }
            else if ((operand1.ToString() == "^" || operand2.ToString() == "^") && power != null)
            {
                textDisplay.Text += thing;
                power = float.Parse(textDisplay.Text);
            }
            else if (num1 == null)
            {
                textDisplay.Text = thing;
                num1 = float.Parse(textDisplay.Text);
            }
            else if (num1 != null && operand1 == '@')
            {
                textDisplay.Text = thing;
                num1 = float.Parse(textDisplay.Text);
                operand1 = null;
            }
            else if (num1 != null && operand1 == null)
            {
                textDisplay.Text += thing;
                num1 = float.Parse(textDisplay.Text);
            }
            else if (num2 == null)
            {
                textDisplay.Text = thing;
                num2 = float.Parse(textDisplay.Text);
            }
            else if (num2 != null && operand2 == null)
            {
                textDisplay.Text += thing;
                num2 = float.Parse(textDisplay.Text);
            }
        }

        //Regular Operations
        private void updateOperation(string operation)
        {
            if (operand1 == null || operand1=='@')
            {
                operand1=char.Parse(operation);
                if (operand1 == '=')
                    performOperationOne();
            }
            else
            {
                operand2=char.Parse(operation);
                performOperationOne();
            }
        }

        #region Clear Options
        private void clearOperation(string cleartype)
        {
            switch (cleartype)
            {
                case "CE":
                    if (num2 == null)
                    {
                        num1 = null;
                        textDisplay.Text = "0";
                    }
                    else
                    {
                        num2 = null;
                        textDisplay.Text = "0";
                    }
                    break;
                case "Clear":
                    num1 = null;
                    num2 = null;
                    operand1 = null;
                    operand2 = null;
                    total = null;
                    power = null;
                    textDisplay.Text = "0";
                    break;
                case "Back":
                    textDisplay.Text = textDisplay.Text.Remove(textDisplay.Text.Length - 1);
                    if (num2 == null)
                    {
                        if (num1.ToString().Length == 1)
                            textDisplay.Text = "0";
                        else
                            num1 = float.Parse(textDisplay.Text);
                    }
                    else
                    {
                        if (num2.ToString().Length == 1)
                            textDisplay.Text = "0";
                        else
                            num2 = float.Parse(textDisplay.Text);
                    }
                    break;
            }
        }
        #endregion

        //Special Operations
        private void SpecialOperation(string operation)
        { 
            if (num2 == null)
                currentnumber = num1;
            else
                currentnumber = num2;

            switch (operation)
            {
                case "N":
                    currentnumber = currentnumber * -1;
                    textDisplay.Text = currentnumber.ToString();
                    break;
                case "D":
                    textDisplay.Text += ".1";
                    clearOperation("Back");
                    break;
                case "R":
                    if (currentnumber < 0)
                    {
                        clearOperation("Clear");
                        textDisplay.Text = "Error";
                    }
                    else
                    {
                        currentnumber = (float?)Math.Pow((double)currentnumber, .5);
                        textDisplay.Text = currentnumber.ToString();
                    }
                    break;
                case "S":
                    currentnumber = currentnumber * currentnumber;
                    textDisplay.Text = currentnumber.ToString();
                    break;
                case "^":
                    currentnumber = (float?)Math.Pow((double)currentnumber, (double)power);
                    total = currentnumber;
                    textDisplay.Text = currentnumber.ToString();
                    break;
                case "I":
                    currentnumber = 1 / currentnumber;
                    textDisplay.Text = currentnumber.ToString();
                    break;
            }
            updateHistory(operation);
            if (num2 == null)
            {
                num1 = currentnumber;
            }
            else
            {
                num2 = currentnumber;
            }
        }

        //Performing Operations
        private void performOperationOne()
        {
            switch (operand1)
            {
                case '+':
                    total = num1 + num2;
                    break;
                case '-':
                    total = num1 - num2;
                    break;
                case '*':
                    total=num1 * num2;
                    break;
                case '/':
                    if (num2.ToString()=="0")
                    {
                        clearOperation("Clear");
                        textDisplay.Text += "Error";
                    }
                    else
                        total=num1 / num2;
                    break;
            }

            if (operand2 == '=' || operand1 == '=')
            {
                if (num2 != null)
                {
                    textDisplay.Text = total.ToString();
                    updateHistory(operand2.ToString());
                    num1 = total;
                    operand1 = '@';
                    operand2 = null;
                    num2 = null;
                    total = null;
                    power = null;
                }
                else
                {
                    textDisplay.Text = num1.ToString();
                    updateHistory(num1.ToString());
                    operand1 = '@';
                    operand2 = null;
                    num2 = null;
                    total = null;
                    power = null;
                }   
            }
            else  //operand2 != '='
            {
                num1 = total;
                textDisplay.Text = num1.ToString();
                operand1 = operand2;
                operand2 = null;
                num2 = null;
                total = null;
                power = null;
            }
        }
        #endregion
        #region History
        //Updating History
        private void updateHistory(string newElement)
        {
            if (newElement == "=")
            {
                historyStorage += num1.ToString();
                historyStorage += operand1.ToString();
                historyStorage += num2.ToString();
                historyStorage += "=";
                historyStorage += total.ToString();
                historyStorage += System.Environment.NewLine;
                textHistory.Text = historyStorage;
            }
            else if (newElement == "S")
            {
                historyStorage += num1.ToString();
                historyStorage += "^2=";
                historyStorage += currentnumber.ToString();
                historyStorage += System.Environment.NewLine;
                textHistory.Text = historyStorage;
            }
            else if (newElement == "R")
            {
                historyStorage += "SQRT(";
                historyStorage += num1.ToString();
                historyStorage += ")=";
                historyStorage += currentnumber.ToString();
                historyStorage += System.Environment.NewLine;
                textHistory.Text = historyStorage;
            }
            else if (newElement=="N")
            {
                historyStorage += "-";
                historyStorage+=num1.ToString();
                historyStorage += "=";
                historyStorage+= currentnumber.ToString();
                historyStorage += System.Environment.NewLine;
                textHistory.Text = historyStorage;
            }
            else if (newElement=="^")
            {
                historyStorage += num1.ToString();
                historyStorage += "^";
                historyStorage += power.ToString();
                historyStorage += "=";
                historyStorage += currentnumber.ToString();
                historyStorage += System.Environment.NewLine;
                textHistory.Text = historyStorage;
            }
            else if (newElement == "I")
            {
                historyStorage += "1/";
                historyStorage += num1.ToString();
                historyStorage += "=";
                historyStorage += currentnumber.ToString();
                historyStorage += System.Environment.NewLine;
                textHistory.Text = historyStorage;
            }
            else if (float.Parse(newElement)>=0 || float.Parse(newElement)<0)
            {
                historyStorage += newElement;
                historyStorage += "=";
                historyStorage += newElement;
                historyStorage += System.Environment.NewLine;
                textHistory.Text = historyStorage;
            }
        }
        #endregion

        #region Key Presses
        //Mapping key presses
        private void Calculator_Load(object sender, EventArgs e)
        {
            KeyPress += Calculator_KeyPress;
        }
        
        void Calculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '0')
                updateOutput(e.KeyChar.ToString());
            else if (e.KeyChar >= '1' && e.KeyChar <= '9')
                updateOutput(e.KeyChar.ToString());
            else if (e.KeyChar == '+')
                updateOperation(e.KeyChar.ToString());
            else if (e.KeyChar == '-')
                updateOperation(e.KeyChar.ToString());
            else if (e.KeyChar == '/')
                updateOperation(e.KeyChar.ToString());
            else if (e.KeyChar == '*')
                updateOperation(e.KeyChar.ToString());
            else if (e.KeyChar == '=')
                updateOperation(e.KeyChar.ToString());
            else if (e.KeyChar == 94)
                updateOperation(e.KeyChar.ToString());
            else if (e.KeyChar == 8)
                clearOperation("Back");
            else if (e.KeyChar == '.')
                SpecialOperation("D");
            else if (e.KeyChar == 'c' || e.KeyChar == 'C')
                clearOperation("Clear");
        }
        #endregion
    }
}
