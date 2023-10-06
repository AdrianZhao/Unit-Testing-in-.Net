using System.Reflection.Metadata.Ecma335;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Calculator_Desktop_Application
{
    public partial class CalculatorDesktopApplicationForm : Form
    {
        private Calculator calculator;
        bool isNewOperand = true;

        public CalculatorDesktopApplicationForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += CalculatorDesktopApplicationForm_KeyDown;
            calculator = new Calculator();
        }
        private void Calcuolator_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            ChangeFocus();
        }
        private void ChangeFocus()
        {
            foreach (Control control in this.Controls)
            {
                control.TabStop = false;
            }

            this.TabStop = true;

            this.ActiveControl = null;

            this.Focus();
        }
        private void CalculatorDesktopApplicationForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool shiftPressed = e.Shift;
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    DigitButton_Click(Number0, EventArgs.Empty);
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    DigitButton_Click(Number1, EventArgs.Empty);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    DigitButton_Click(Number2, EventArgs.Empty);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    DigitButton_Click(Number3, EventArgs.Empty);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    DigitButton_Click(Number4, EventArgs.Empty);
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    DigitButton_Click(Number5, EventArgs.Empty);
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    DigitButton_Click(Number6, EventArgs.Empty);
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    DigitButton_Click(Number7, EventArgs.Empty);
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    if (shiftPressed)
                    {
                        OperatorButton_Click(TimesButton, EventArgs.Empty);
                        break;
                    }
                    DigitButton_Click(Number8, EventArgs.Empty);
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    DigitButton_Click(Number9, EventArgs.Empty);
                    break;
                case Keys.Add:
                    OperatorButton_Click(PlusButton, EventArgs.Empty);
                    break;
                case Keys.Oemplus:
                    if (shiftPressed)
                    {
                        OperatorButton_Click(PlusButton, EventArgs.Empty);
                        break;
                    }
                    EqualsButton_Click(EqualSign, EventArgs.Empty);
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    OperatorButton_Click(MinusButton, EventArgs.Empty);
                    break;
                case Keys.Multiply:
                    break;
                case Keys.Divide:
                case Keys.OemQuestion:
                    OperatorButton_Click(DivideButton, EventArgs.Empty);
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    DecimalButton_Click(DecimalPoint, EventArgs.Empty);
                    break;
                case Keys.Back:
                    ClearButton_Click(ClearButton, EventArgs.Empty);
                    break;
                case Keys.Enter:
                    EqualsButton_Click(EqualSign, EventArgs.Empty);
                    break;
            }
        }
        private void DigitButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Text;

            if (isNewOperand)
            {
                ConvertScreen.Text = "";
            }
            string result = calculator.DigitButtonPressed(double.Parse(digit)).ToString();
            OutputScreen.Text = result;
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            if (OutputScreen.Text == "Division by zero is not allowed.")
            {
                OutputScreen.Text = "0";
            }
            Button button = (Button)sender;
            char operation = button.Text[0];
            calculator.OperatorButtonPressed(operation);
            OperationScreen.Text = operation.ToString();
        }
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (OutputScreen.Text != "Division by zero is not allowed.")
            {
                OutputScreen.Text = calculator.EqualsButtonPressed();
            }
            else
            {
                ClearAll();
            }
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            calculator.ClearAll();
            OutputScreen.Text = "0";
            OperationScreen.Text = "";
            ConvertScreen.Text = "";
        }
        private void DecimalButton_Click(object sender, EventArgs e)
        {
            OutputScreen.Text = calculator.DecimalButtonPressed(OutputScreen.Text);
        }
        private void BinaryButton_Click(object sender, EventArgs e)
        {
            ConvertScreen.Text = calculator.ConvertToBinary(double.Parse(OutputScreen.Text));
        }
        private void HexadecimalButton_Click(object sender, EventArgs e)
        {
            ConvertScreen.Text = calculator.ConvertToHexadecimal(double.Parse(OutputScreen.Text));
        }
    }
    public class Calculator
    {
        private double currentOperand = 0;
        private double storedOperand = 0;
        private char storedOperation = ' ';
        private bool isNewOperand = true;
        private string binary = "";
        private string hexadecimal = "";

        public double CurrentOperand => currentOperand;
        public double StoredOperand => storedOperand;
        public char StoredOperation => storedOperation;
        public bool IsNewOperand => isNewOperand;
        public string Binary => binary;
        public string Hexadecimal => hexadecimal;

        public double DigitButtonPressed(double digit)
        {
            double result;
            if (isNewOperand)
            {
                result = digit;
                isNewOperand = false;
            }
            else
            {
                result = currentOperand * 10 + digit;
            }
            currentOperand = result;
            return currentOperand;
        }

        public void OperatorButtonPressed(char operation)
        {
            if (!isNewOperand)
            {
                if (storedOperation != ' ')
                {
                    PerformOperation();
                }
                storedOperand = currentOperand;
                storedOperation = operation;
                isNewOperand = true;
            }
            else
            {
                storedOperation = operation;
            }
        }

        public string EqualsButtonPressed()
        {
            string screenOutput = "";
            if (!isNewOperand && storedOperation != ' ')
            {
                screenOutput = PerformOperation();
                if (screenOutput != "Division by zero is not allowed.")
                {
                    storedOperand = currentOperand;
                    storedOperation = ' ';
                    isNewOperand = true;
                }
            }
            return screenOutput;
        }

        public void ClearButtonPressed()
        {
            currentOperand = 0;
            storedOperand = 0;
            storedOperation = ' ';
            isNewOperand = true;
        }

        public string DecimalButtonPressed(string currentOperand)
        {
            if (!currentOperand.Contains("."))
            {
                currentOperand += ".";
            }
            return currentOperand;
        }

        public string ConvertToBinary(double currentOperand)
        {
            string convertScreenOutput;
            if (currentOperand.ToString().Contains("."))
            {
                convertScreenOutput = "CANNOT CONVERT WITH DECIMAL POINT";
            }
            else
            {
                if (currentOperand > 255 || currentOperand < 0)
                {
                    convertScreenOutput = "OUT OF RANGE";
                }
                else
                {
                    string binary = Convert.ToString((int)currentOperand, 2);
                    convertScreenOutput = binary;
                }
            }
            binary = convertScreenOutput;
            return convertScreenOutput;
        }

        public string ConvertToHexadecimal(double currentOperand)
        {
            string convertScreenOutput;
            if (currentOperand.ToString().Contains("."))
            {
                convertScreenOutput = "CANNOT CONVERT WITH DECIMAL POINT";
            }
            else
            {
                if (currentOperand > 4294967295 || currentOperand < 0)
                {
                    convertScreenOutput = "OUT OF RANGE";
                }
                else
                {
                    string hexadecimal = Convert.ToString((uint)currentOperand, 16).ToUpper();
                    convertScreenOutput = hexadecimal;
                }
            }
            hexadecimal = convertScreenOutput;
            return convertScreenOutput;
        }

        private string PerformOperation()
        {
            string resultScreenOutput = "";
            double current = currentOperand;
            switch (storedOperation)
            {
                case '+':
                    currentOperand = storedOperand + current;
                    break;
                case '-':
                    currentOperand = storedOperand - current;
                    break;
                case '*':
                    currentOperand = storedOperand * current;
                    break;
                case '/':
                    if (current != 0)
                    {
                        currentOperand = storedOperand / current;
                    }
                    else
                    {
                        ClearAll();
                        resultScreenOutput = "Division by zero is not allowed.";
                    }
                    break;
            }

            if (resultScreenOutput != "Division by zero is not allowed.")
            {
                resultScreenOutput = currentOperand.ToString();
            }
            return resultScreenOutput;
        }

        public void ClearAll()
        {
            currentOperand = 0;
            storedOperand = 0;
            storedOperation = ' ';
            isNewOperand = true;
        }
    }
}