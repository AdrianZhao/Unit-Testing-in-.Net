using Calculator_Desktop_Application;

namespace CalculatorUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Plus_ValidNumber()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.DigitButtonPressed(5);
            calculator.OperatorButtonPressed('+');
            calculator.DigitButtonPressed(3);
            calculator.EqualsButtonPressed();

            // Assert
            Assert.AreEqual(8.0, calculator.CurrentOperand);
        }

        [TestMethod]
        public void Subtraction_ValidNumber()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.DigitButtonPressed(8);
            calculator.OperatorButtonPressed('-');
            calculator.DigitButtonPressed(4);
            calculator.EqualsButtonPressed();

            // Assert
            Assert.AreEqual(4.0, calculator.CurrentOperand);
        }

        [TestMethod]
        public void Multiply_ValidNumber()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.DigitButtonPressed(6);
            calculator.OperatorButtonPressed('*');
            calculator.DigitButtonPressed(7);
            calculator.EqualsButtonPressed();

            // Assert
            Assert.AreEqual(42.0, calculator.CurrentOperand);
        }

        [TestMethod]
        public void Division_ValidNumber()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.DigitButtonPressed(9);
            calculator.OperatorButtonPressed('/');
            calculator.DigitButtonPressed(3);
            calculator.EqualsButtonPressed();

            // Assert
            Assert.AreEqual(3.0, calculator.CurrentOperand);
        }

        [TestMethod]
        public void Division_NotValidNumber()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.DigitButtonPressed(9);
            calculator.OperatorButtonPressed('/');
            calculator.DigitButtonPressed(0);
            calculator.EqualsButtonPressed();

            // Assert
            Assert.AreEqual(0, calculator.CurrentOperand);
        }

        [TestMethod]
        public void ConvertToBinary_ValidNumber()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.ConvertToBinary(100);

            // Assert
            Assert.AreEqual("1100100", calculator.Binary);
        }

        [TestMethod]
        public void ConvertToHexadecimal_ValidNumber()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.ConvertToHexadecimal(100);

            // Assert
            Assert.AreEqual("64", calculator.Hexadecimal);
        }

        [TestMethod]
        public void ConvertToBinary_NotValidNumberWithoutDecimalPoint()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.ConvertToBinary(256);

            // Assert
            Assert.AreEqual("OUT OF RANGE", calculator.Binary);
        }

        [TestMethod]
        public void ConvertToHexadecimal_NotValidNumberWithoutDecimalPoint()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.ConvertToHexadecimal(4294967296);

            // Assert
            Assert.AreEqual("OUT OF RANGE", calculator.Hexadecimal);
        }

        [TestMethod]
        public void ConvertToBinary_ValidNumberWithDecimalPoint()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.ConvertToBinary(25.6);

            // Assert
            Assert.AreEqual("CANNOT CONVERT WITH DECIMAL POINT", calculator.Binary);
        }

        [TestMethod]
        public void ConvertToHexadecimal_ValidNumberWithDecimalPoint()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            calculator.ConvertToHexadecimal(25.6);

            // Assert
            Assert.AreEqual("CANNOT CONVERT WITH DECIMAL POINT", calculator.Hexadecimal);
        }
    }
}